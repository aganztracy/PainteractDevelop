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
    public GameObject MyPixelsOBJ;
    //parameters
    public int pixScale; //diameter of pixel

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
    Material spMaterial;

    //--------------------------功能模式控制变量--------------------------------
    public int Control = 1;

    //test bei
    //public int pixScaleNum = pixScale;

    void Start () {
        CanvasTF = GameObject.FindWithTag ("Canvas").transform;
        MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
        // sp = (Sprite)Resources.Load("Sprites/circle", typeof(Sprite)) as Sprite;
        spMaterial = Resources.Load ("Materials/AtomMaterial") as Material;
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
        MyPixelsTF.transform.position = new Vector3 ((myScreemWidth - width) * 0.5f, (myScreemHeight - height) * 0.5f, 1);

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

        Debug.Log ("CreateSprite finished");
        OrinImageBg.gameObject.SetActive (false);

        InstantiateController (); //效果脚本初始化

    }

    public GameObject CreateSprite (float y, float x, int row, int clo, Color col) {
        //将粒子对象改成球体
        GameObject pixShape = GameObject.CreatePrimitive (PrimitiveType.Sphere);

        //将粒子对象改为cube by z
        //GameObject pixShape = GameObject.CreatePrimitive (PrimitiveType.Cube);
        float hue, saturate, brightness; //brightness 的值是0-1
        Color.RGBToHSV (col, out hue, out saturate, out brightness);

        //float brightScaleFactor = brightness * 5.0f;  //长短不一
        float brightScaleFactor = 1.0f; //长短一样

        pixShape.GetComponent<Renderer> ().material = spMaterial;

        pixShape.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", col);
        //GameObject pixShape = new GameObject();
        //pixShape.AddComponent<SpriteRenderer>();

        //发光效果尝试
        //pixShape.AddComponent<TrailRenderer>();
        pixShape.AddComponent<BoxCollider> ();
        pixShape.AddComponent<MyPixel> ();

        pixShape.GetComponent<MyPixel> ().Row = row;
        pixShape.GetComponent<MyPixel> ().Clo = clo;
        pixShape.GetComponent<MyPixel> ().Col = col;

        //test 3d position by z

        ///-无效果0----------------------------
        float z = 10.0f;
        ///-效果1可----------------------------
        // float z = Mathf.Sin(Mathf.Sqrt(x*x+y*y))*100; //比较混乱的城市效果
        ///-效果2可---------------------------
        // float z = Mathf.Sin (Mathf.Sqrt (clo * clo + row * row)) * 100; //有规律的波纹效果1，用其他函数也可以变成其他类型的波纹
        ///--效果3可---------------------------
        // float u = ExtensionMethods.Map (row, 0, rowNum,-10, 10);
        // float v = ExtensionMethods.Map (clo, 0, cloNum, -10, 10);
        // float z = Mathf.Cos (Mathf.Sqrt (u * u + v * v)) * 100; //有规律的波纹效果2

        ///-卷曲效果零食可-------------------------------
        // float u = ExtensionMethods.Map (row, 0, rowNum, -0, 5);//后面这两个参数对形状影响很大
        // float v = ExtensionMethods.Map (clo, 0, cloNum, -1, 1);
        // x = v*100*5;
        // y = Mathf.Sin (u) * Mathf.Cos (v)*100*5;
        // float z = Mathf.Cos (u) * Mathf.Cos (v)*100*5;
        ///---效果4---？---------------------------
        // float u = ExtensionMethods.Map (row, 0, rowNum, 10, 52);
        // float v = ExtensionMethods.Map (clo, 0, cloNum, -10, 10);
        // x = 0.75f * v * 100;
        // y = Mathf.Sin (u) * v * 100;
        // float z = Mathf.Cos (u) * Mathf.Cos (v) * 500;
        //--------------------------------------

        pixShape.GetComponent<MyPixel> ().PosXY = new Vector3 (x, y, z);
        pixShape.name = row + "," + clo;

        ///粒子的拖尾
        //SpriteRenderer spr = pixShape.GetComponent<SpriteRenderer>();

        //TrailRenderer trialr = pixShape.GetComponent<TrailRenderer>();
        //trialr.material = spMaterial;

        //trialr.material.color = col;
        //trialr.startWidth = pixScale;

        //spr.color = col;
        //spr.sprite = sp;

        pixShape.transform.SetParent (MyPixelsTF);
        pixShape.transform.localScale = new Vector3 (pixScale, pixScale, pixScale * brightScaleFactor);
        ///1.Sets the coordinates relative to the parent object 2.用长方体表示时，平移半个高度保持一面是平整的
        pixShape.transform.localPosition = new Vector3 (x, y, z - pixScale * brightScaleFactor * 0.5f);
        return pixShape;
    }

    public void ResetPixelPosition () {

        for (int y = 0, row = 0; row < rowNum; y = y + pixScale, row++) {
            for (int x = 0, clo = 0; clo < cloNum; x = x + pixScale, clo++) {
                pixArray[row, clo].transform.localPosition = pixArray[row, clo].GetComponent<MyPixel> ().PosXY;
            }
        }

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
            OrinImageBg.gameObject.SetActive (true);
            //让显示图片的UI控件一开始先隐藏，当打开图片之后再激活
            //因为控件背景为白色才能正常显示图片，但背景为黑

            OrinImageBg.texture = Img;

        }
    }

    //bei  18/10/19
    public void Cancel () {
        if (OrinImageBg) {
            OrinImageBg.texture = null;
        }

        DestroyController (); //去除效果脚本

    }

    public void RefreshProcess () { //去除前一效果产生的所有对象，进行新处理

        ResetPixelPosition ();
        MyPixelsOBJ.GetComponent<RemoveAllController> ().RemoveAllControllerComponent ();
        for (int i = MyPixelsOBJ.transform.childCount - 1; i >= 0; i--) { //重新进行MyPixel脚本的添加
            // Destroy (MyPixelsOBJ.transform.GetChild (i).gameObject.GetComponent<MyPixel> ());
            // MyPixelsOBJ.transform.GetChild (i).gameObject.AddComponent<MyPixel> ();
            MyPixelsOBJ.transform.GetChild (i).gameObject.GetComponent<MyPixel> ().Restart();
        }

        InstantiateController ();
    }

    public void InstantiateController () { //效果添加在MyPixelsOBJ上的功能脚本

        //如果是音乐可视化功能，在粒子产生后添加音乐可视化脚本
        if (Control == 9) {
            GameObject MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
            MyPixelsOBJ.AddComponent<AudioPeer> ();
            MyPixelsOBJ.AddComponent<AudioVisualizationController> ();
            AudioPeer APComponent = MyPixelsOBJ.GetComponent<AudioPeer> ();
            AudioVisualizationController AVCComponent = MyPixelsOBJ.GetComponent<AudioVisualizationController> ();
            AVCComponent.changePixelScale = true;
        }

        // Debug.Log ("Music Visualization setup");

        if (Control == 14) {
            GameObject MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
            MyPixelsOBJ.AddComponent<NoiseFlowFieldController> ();
            NoiseFlowFieldController NFComponent = MyPixelsOBJ.GetComponent<NoiseFlowFieldController> ();
            Debug.Log ("add noise.cs");

        }

        // Debug.Log ("Music Visualization2 setup");
        if (Control == 13) {
            GameObject MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
            MyPixelsOBJ.AddComponent<WobblyGridController> ();
        }
    }

    public void DestroyController () { //在切换效果或退出效果时，消除效果添加在MyPixelsOBJ上的功能脚本

        //如果是音乐可视化功能，在返回首页时需要暂停音乐的播放并去除音乐可视化的脚本

        if (Control == 14) {
            GameObject MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
            NoiseFlowFieldController NFComponent = MyPixelsOBJ.GetComponent<NoiseFlowFieldController> ();
            Destroy (NFComponent);

        }

        if (Control == 9) {
            GameObject MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
            AudioVisualizationController AVComponent = MyPixelsOBJ.GetComponent<AudioVisualizationController> ();
            AudioPeer APComponent = MyPixelsOBJ.GetComponent<AudioPeer> ();
            AVComponent.StopMusic ();
            Destroy (AVComponent);
            Destroy (APComponent);
        }

        if (Control == 13) {
            GameObject MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
            WobblyGridController WGComponent = MyPixelsOBJ.GetComponent<WobblyGridController> ();
            Destroy (WGComponent);
        }

    }

    public void SetControlTo (int targetControl) { //每个效果按钮按下时的响应函数

        DestroyController ();

        Control = targetControl;

        if (targetControl == 12) {
            //MyPixelsOBJ.GetComponent<DestroyAllChildren> ().DestroyChildren ();
            MyPixelsOBJ.GetComponent<RemoveAllController> ().RemoveAllControllerComponent ();
            GameObject BeverageBoxsOBJ = GameObject.FindWithTag ("BeverageBoxs");
            BeverageBoxsOBJ.AddComponent<BeveragesController> ();
            for (int i = MyPixelsOBJ.transform.childCount - 1; i >= 0; i--) { //重新进行MyPixel脚本的添加
                Destroy (MyPixelsOBJ.transform.GetChild (i).gameObject.GetComponent<MyPixel> ());
                MyPixelsOBJ.transform.GetChild (i).gameObject.AddComponent<MyPixel> ();
            }

        } else {
            RefreshProcess ();

        }

    }

}

public static class ExtensionMethods { //一个扩展的静态类，可以直接用类名调用方法，这里的方法是自定义的。方法不多，暂时存放在这。
    public static float Map (this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}