using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class ReadPic : MonoBehaviour {

    public static Texture2D Img = null;
    public static Color[, ] ImageColor2d;
    public RawImage OrinImageBg;
    public Transform MyPixelsTF;
    //parameters
    private int pixScale; //diameter of pixel
    private int linePixNum = 25; //number of pixels in a line
    private int myScreemWidth = UnityEngine.Screen.width;
    private int myScreemHeight = UnityEngine.Screen.height;
    Transform CanvasTF;

    //the row and clo number
    public int rowNum;
    public int cloNum;

    public GameObject[, ] pixArray;
    public Color[, ] pixColors;
    // Sprite sp;
    // Material spMaterial;

    //--------------------------功能模式控制变量--------------------------------
    public int Control = 1;

    //test bei
    //public int pixScaleNum = pixScale;

    void Start () {
        CanvasTF = GameObject.FindWithTag ("Canvas").transform;
        // sp = (Sprite)Resources.Load("sprites/circle", typeof(Sprite)) as Sprite;
        // spMaterial = Resources.Load("Material/1") as Material;
    }
    public void AddHead () {
        OpenFileDialog od = new OpenFileDialog ();
        od.Title = "请选择头像图片";
        od.Multiselect = false;
        od.Filter = "图片文件(*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp";

        if (od.ShowDialog () == DialogResult.OK) {
            StartCoroutine (GetTexture ("file://" + od.FileName));
        }

    }
    public void PicProcess () {
        if (Img == null) return;
        int width = Mathf.FloorToInt (Img.width);
        int height = Mathf.FloorToInt (Img.height);
        ImageColor2d = new Color[height, width]; //initialize
        Color[] pix = Img.GetPixels (0, 0, width, height);
        pixScale = (int) Mathf.Floor (myScreemWidth / linePixNum); //像素直径是屏幕宽度除以列像素数
        //align to center
        MyPixelsTF.transform.localPosition = new Vector3 ((myScreemWidth - width) * 0.5f, (myScreemHeight - height) * 0.5f, 1);

        rowNum = (int) Mathf.Floor (height / pixScale);
        cloNum = (int) Mathf.Floor (width / pixScale);

        Debug.Log (rowNum + "DDS" + cloNum);

        pixArray = new GameObject[rowNum, cloNum];
        pixColors = new Color[rowNum, cloNum];

        //covert 1d color[] to 2d color[,]
        for (int y = 0, row = 0; row < rowNum; y = y + pixScale, row++) {
            for (int x = 0, clo = 0; clo < cloNum; x = x + pixScale, clo++) {
                ImageColor2d[y, x] = pix[width * y + x]; //pay attention to the writing style of 2d array in c#
                pixArray[row, clo] = CreateSprite (y, x, row, clo, ImageColor2d[y, x]);
                pixColors[row, clo] = ImageColor2d[y, x];
            }
        }
        OrinImageBg.gameObject.SetActive (false);
        //---------------------------------------------------------------------temp-----------
        //ca.SetActive(true);

        // Debug.Log(pixArray);

        //bei  18/10/19
        //以某种方式打开图片后，就不显示打开图片按钮
        CanvasTF.GetChild (0).gameObject.SetActive (false);
        CanvasTF.GetChild (1).gameObject.SetActive (false);
        //并且显示处理图片和取消按钮
        CanvasTF.GetChild (2).gameObject.SetActive (false);
        CanvasTF.GetChild (3).gameObject.SetActive (false);
        //显示变换按钮和首页按钮
        CanvasTF.GetChild (4).gameObject.SetActive (true);
        CanvasTF.GetChild (5).gameObject.SetActive (true);
        //显示主页名称logo
        CanvasTF.GetChild (8).gameObject.SetActive (false);
        //不显示功能选择按钮组
        CanvasTF.GetChild (9).gameObject.SetActive (false);

    }

    public GameObject CreateSprite (int y, int x, int row, int clo, Color col) {
        //将粒子对象改成球体
        GameObject pixShape = GameObject.CreatePrimitive (PrimitiveType.Sphere);
        pixShape.GetComponent<Renderer> ().material.color = col;

        //GameObject pixShape = new GameObject();
        //pixShape.AddComponent<SpriteRenderer>();
        //发光效果尝试
        //pixShape.GetComponent<Renderer>().material = Resources.Load <Material>("Assets/Resources/Materials/emission");
        //pixShape.AddComponent<TrailRenderer>();
        pixShape.AddComponent<BoxCollider> ();
        pixShape.AddComponent<MyPixel> ();

        pixShape.GetComponent<MyPixel> ().Row = row;
        pixShape.GetComponent<MyPixel> ().Clo = clo;
        pixShape.GetComponent<MyPixel> ().Col = col;
        pixShape.GetComponent<MyPixel> ().PosXY = new Vector2 (x, y);
        pixShape.name = row + "," + clo;

        //SpriteRenderer spr = pixShape.GetComponent<SpriteRenderer>();

        //TrailRenderer trialr = pixShape.GetComponent<TrailRenderer>();
        //trialr.material = spMaterial;

        //trialr.material.color = col;
        //trialr.startWidth = pixScale;

        //spr.color = col;
        //spr.sprite = sp;

        pixShape.transform.SetParent (MyPixelsTF);
        pixShape.transform.localScale = new Vector3 (pixScale, pixScale, pixScale);
        pixShape.transform.localPosition = new Vector3 (x, y, 10f); //Sets the coordinates relative to the parent object 

        return pixShape;
    }
    Texture2D ScaleTexture (Texture2D source, int targetWidth, int targetHeight) {
        Texture2D result = new Texture2D (targetWidth, targetHeight, source.format, false);
        float incX = (1.0f / (float) targetWidth);
        float incY = (1.0f / (float) targetHeight);

        for (int i = 0; i < result.height; ++i) {
            for (int j = 0; j < result.width; ++j) {
                Color newColor = source.GetPixelBilinear ((float) j / (float) result.width, (float) i / (float) result.height);
                result.SetPixel (j, i, newColor);
            }
        }
        result.Apply ();
        return result;
    }

    public Texture2D ResizePic (Texture2D pic) {
        int picW = pic.width;
        int picH = pic.height;
        if (Mathf.Max (picW, picH) == picW) {
            pic = ScaleTexture (pic, myScreemWidth, (int) (myScreemWidth * picH / picW));
        } else {
            pic = ScaleTexture (pic, (int) (picW * myScreemWidth / picH), myScreemWidth);
        }
        return pic;
    }

    IEnumerator GetTexture (string url) {
        WWW www = new WWW (url);
        yield return www;
        if (www.isDone && www.error == null) {
            Img = ResizePic (www.texture);

            //bei  18/10/19
            CanvasTF.GetChild (6).gameObject.SetActive (true);
            //让显示图片的UI控件一开始先隐藏，当打开图片之后再激活
            //因为控件背景为白色才能正常显示图片，但背景为黑

            OrinImageBg.texture = Img;

            //bei  18/10/19
            //以某种方式打开图片后，就不显示打开图片按钮
            CanvasTF.GetChild (0).gameObject.SetActive (false);
            CanvasTF.GetChild (1).gameObject.SetActive (false);
            //并且显示处理图片和取消按钮
            CanvasTF.GetChild (2).gameObject.SetActive (true);
            CanvasTF.GetChild (3).gameObject.SetActive (true);
            //不显示主页名称logo
            CanvasTF.GetChild (8).gameObject.SetActive (false);
            //不显示功能选择按钮组
            CanvasTF.GetChild (9).gameObject.SetActive (false);

        }

    }

    //bei  18/10/19
    public void Cancel () {
        if (OrinImageBg) {
            OrinImageBg.texture = null;
        }

        //不显示处理图片和取消按钮
        CanvasTF.GetChild (0).gameObject.SetActive (true);
        CanvasTF.GetChild (1).gameObject.SetActive (true);
        //并且显示打开图片按钮
        CanvasTF.GetChild (2).gameObject.SetActive (false);
        CanvasTF.GetChild (3).gameObject.SetActive (false);
        //不显示缺省rawimage
        CanvasTF.GetChild (6).gameObject.SetActive (false);
        //不显示变换按钮和首页按钮
        CanvasTF.GetChild (4).gameObject.SetActive (false);
        CanvasTF.GetChild (5).gameObject.SetActive (false);
        //不显示主页名称logo
        CanvasTF.GetChild (8).gameObject.SetActive (true);
        //显示功能选择按钮组
        CanvasTF.GetChild (9).gameObject.SetActive (true);
    }

    public void SetControlto1 () {
        Control = 1;
    }

    public void SetControlto2 () {
        Control = 2;
    }
    public void SetControlto3 () {
        Control = 3;
    }
    public void SetControlto4 () {
        Control = 4;
    }
    public void SetControlto5 () {
        Control = 5;
    }
    public void SetControlto6 () {
        Control = 6;
    }
    public void SetControlto7 () {
        Control = 7;
    }
    public void SetControlto8 () {
        Control = 8;
    }
}