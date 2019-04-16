using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class DotFlowController : MonoBehaviour {
    public float scl; //scale of vector feild, the smaller scl,the densitier feild

    //for test
    int pixScale; //scale of vector feild, the smaller scl,the densitier feild
    public GameObject VectorFieldPrefab;
    public static VectorField[] FlowFields; //ector feild
    // public static GameObject[] FlowFields; //ector feild
    public int Cols, Rows;

    Vector3 iniPos;
    Vector3 pos; //will update

    Vector3 vel;
    Vector3 acc;
    float maxspeed = 1;

    public GameObject MyPixelsOBJ;
    public GameObject CanvasOBJ;
    void Start () {
        MyPixelsOBJ = GameObject.Find ("MyPixels");
        CanvasOBJ = GameObject.FindWithTag ("Canvas");

        VectorFieldPrefab = (GameObject) Resources.Load ("Prefabs/VectorField");

        //for test
        pixScale = CanvasOBJ.GetComponent<ReadPic> ().pixScale;
        scl = pixScale * 0.8f; //向量场的间隔比粒子直径大才更容易吗？不可以小嘛？
        //

        iniPos = this.gameObject.transform.position;
        pos = this.gameObject.transform.position;

        vel = new Vector3 (0, 0, 0);
        acc = new Vector3 (0, 0, 0);

        Cols = (int) Mathf.Floor (UnityEngine.Screen.width / scl); //1080
        Rows = (int) Mathf.Floor (UnityEngine.Screen.height / scl); //1920
        if (this.transform.gameObject.CompareTag ("MyPixels")) {
            FlowFields = new VectorField[Cols * Rows];
            for (int y = 0; y < Rows; y++) {
                for (int x = 0; x < Cols; x++) {
                    int index1 = x + y * Cols;
                    Vector2 v = new Vector2 (1, 1);
                    v = v * 0.008f; //set the Magnitude of vector
                    FlowFields[index1] = new VectorField (x, y, scl);
                    // GameObject vecField = Instantiate (VectorFieldPrefab);
                    // var flowFieldsCompo=FlowFields[index1].GetComponent<VectorField>();//目前测试，之后可行的话要优化，引用
                    // vecField.GetComponent<VectorField> ().xNum = x;
                    // vecField.GetComponent<VectorField> ().yNum = y;
                    // vecField.GetComponent<VectorField> ().scl = scl;
                    // vecField.transform.position = new Vector3 (x * scl, y * scl, -500);
                    // FlowFields[index1] = vecField;
                    // new VectorField (x, y, scl);
                }
            }
        }
    }

    void Update () {
        if (this.transform.gameObject.CompareTag ("MyPixels")) {
            return;
        }
        //update pixScreenPos
        // pixScreenPos.x = this.pos.x + MyPixelsOBJ.transform.position.x;
        // pixScreenPos.y = this.pos.y + MyPixelsOBJ.transform.position.y;

        this.Follow (FlowFields, Cols); //为什么没加static 前数组会被自动清空？？
        this.Edges (0, 0);

        this.vel += this.acc;
        this.vel = Vector3.ClampMagnitude (this.vel, this.maxspeed); //limit velocity because of the ever-exsiting acceleration
        this.pos += this.vel;
        this.acc *= 0;
        this.transform.position = this.pos;

    }
    void Follow (VectorField[] vectors, int Cols) {
        int xx = (int) Mathf.Floor ((pos.x) / scl);
        int yy = (int) Mathf.Floor ((pos.y) / scl);
        //find colum and row of the pixels
        int index = xx + yy * Cols;
        //find the pixel's index in field 
        if (index > 0 && index < vectors.Length) { //to avoid out of index
            Vector3 force = vectors[index].Direction;
            if (force == Vector3.zero) return;
            this.ApplyForce (force); //add the vector regarding as force
        }

        // Debug.Log(force);

    }
    void Edges (int picwidth, int picheight) {
        int inter = 100;
        if (Vector3.Distance (iniPos, pos) > inter) {
            this.pos.x = iniPos.x;
            this.pos.y = iniPos.y;
        }
    }
    void ApplyForce (Vector3 force) {
        this.acc += force;
    }

    public void ChangeFeild (Vector3 mouseDirec, Vector3 MousePos) {
        for (int y = 0; y < Rows; y++) {
            for (int x = 0; x < Cols; x++) {
                int index1 = x + y * Cols;
                FlowFields[index1].CheckChange (mouseDirec, MousePos);
            }
            //gameobject needs add boxcollider component
        }
    }

    public class VectorField {
        public float x; //positionx 
        public float y; //positiony  
        public float xNum; //positionx num
        public float yNum; //positiony  num
        string num;
        public Vector3 Direction;
        public float scl;

        // public VectorField (int X, int Y, float Scl) {
        public VectorField (int xNum, int yNum, float Scl) {
            scl = Scl;
            x = xNum * scl;
            y = yNum * scl;

            Direction = new Vector3 (0, 0, 0);
            num = xNum + "," + yNum;
        }
        public void CheckChange (Vector3 mouseDirec, Vector3 mousePos) {

            // Vector3 ScreenSpace = Camera.main.WorldToScreenPoint (this.transform.position); //从相机的视角将物体世界坐标转化为相对屏幕的三维坐标（只有z坐标是空间中的）
            // Vector3 curMousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));
            //为什么是一起移动的呢？
            if (Mathf.Abs (mousePos.x - x) < scl && Mathf.Abs (mousePos.y - (y )) < scl) { //420是父亲的位置，
                Direction = mouseDirec;
                // Debug.Log (Mathf.Abs (Input.mousePosition.x - x) + "_____" + Mathf.Abs (Input.mousePosition.y - (y + 420)) + "___" + scl);
                // Debug.Log (num);

            }

        }
        // public void ChangeVector (Vector3 mouseDirec) {
        //     Direction = mouseDirec;
        //     this.gameObject.transform.Rotate (mouseDirec);
        //     // Debug.Log ("rotate");
        // }

    }

}