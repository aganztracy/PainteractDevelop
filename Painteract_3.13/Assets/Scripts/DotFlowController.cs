using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class DotFlowController : MonoBehaviour {
    public int scl = 10; //scale of vector feild, the smaller scl,the densitier feild
    public VectorField[] FlowField; //ector feild
    public int Cols, Rows;

    Vector3 iniPos;
    Vector3 pos; //will update

    Vector3 vel;
    Vector3 acc;
    float maxspeed = 1;

    public GameObject MyPixelsOBJ;
    void Start () {
        MyPixelsOBJ = GameObject.Find ("MyPixels");
        iniPos = this.gameObject.transform.position;
        pos = this.gameObject.transform.position;

        vel = new Vector3 (0, 0, 0);
        acc = new Vector3 (0, 0, 0);

        Cols = (int) Mathf.Floor (UnityEngine.Screen.width / scl);
        Rows = (int) Mathf.Floor (UnityEngine.Screen.height / scl);
        FlowField = new VectorField[Cols * Rows];
        for (int y = 0; y < Rows; y++) {
            for (int x = 0; x < Cols; x++) {
                int index1 = x + y * Cols;
                Vector2 v = new Vector2 (1, 1);
                v = v * (float) 0.004; //set the Magnitude of vector
                FlowField[index1] = new VectorField (x, y, scl);
                //FlowField[index1].draw();
            }
        }

        // // show vecterfeilds
        // for (int y = 0; y < Rows; y++)
        // {
        //     for (int x = 0; x < Cols; x++)
        //     {
        //         int index1 = x + y * Cols;
        //         FlowField[index1].draw();
        //     }

        //     //gameobject needs add boxcollider component
        // }
    }

    void Update () {
        //update pixScreenPos
        // pixScreenPos.x = this.pos.x + MyPixelsOBJ.transform.position.x;
        // pixScreenPos.y = this.pos.y + MyPixelsOBJ.transform.position.y;

        this.Follow (FlowField, Cols);
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
        Vector3 force = vectors[index].Direction;
        this.ApplyForce (force); //add the vector regarding as force
    }
    void Edges (int picwidth, int picheight) {
        int inter = 100;
        if (Vector3.Distance (iniPos, pos) > inter) {
            this.pos.x = iniPos.x;
            this.pos.y = iniPos.y;
        }
    }
    public void ApplyForce (Vector3 force) {
        this.acc += force;
    }

    public void ChangeFeild (Vector3 mouseDirec) {
        for (int y = 0; y < Rows; y++) {
            for (int x = 0; x < Cols; x++) {
                int index1 = x + y * Cols;
                FlowField[index1].CheckChange (mouseDirec);
            }
            //gameobject needs add boxcollider component
        }
    }

    //计算mouseDirec
    IEnumerator OnMouseDown () { //鼠标落在gui元素或者碰撞体上才会调用
        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint (transform.position); //从相机的视角将物体世界坐标转化为相对屏幕的三维坐标（只有z坐标是空间中的）
        Vector3 preMousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

        //当鼠标左键按下时  
        while (Input.GetMouseButton (0)) {
            //得到现在鼠标的2维坐标系位置  
            Vector3 curMousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));
            Vector3 mouseDirec=curMousePos-preMousePos;
            ChangeFeild (mouseDirec);

            //这个很主要  
            yield return new WaitForFixedUpdate ();
        }

    }
}

public class VectorField {
    int x; //col number
    int y; //row number
    public Vector2 Direction;
    int scl;

    public SpriteRenderer spr;
    public VectorField (int X, int Y, int Scl) {
        x = X;
        y = Y;
        Direction = new Vector2 (0, 0);
        scl = Scl;

        // GameObject pixShape = new GameObject();
        // pixShape.AddComponent<SpriteRenderer>();
        // spr = pixShape.GetComponent<SpriteRenderer>();
        // spr.color = Color.green;
        // Sprite sp = (Sprite)Resources.Load("Sprites/circle", typeof(Sprite)) as Sprite;
        // spr.sprite = sp;
        // pixShape.transform.localPosition = new Vector3(x, y, 10f);//Sets the coordinates relative to the parent object 
    }
    public void CheckChange (Vector2 mouseDirec) {
        if (Mathf.Abs (Input.mousePosition.x - x * scl) < scl && Mathf.Abs (Input.mousePosition.y - y * scl) < scl) {
            Direction = mouseDirec;
            Debug.Log(mouseDirec);
        }
    }

}