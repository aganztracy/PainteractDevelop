using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class ReadPic : MonoBehaviour {

    public Texture2D Img = null; //static
    public static Color[, ] ImageColor2d;
    public RawImage OrinImageBg;
    public Transform MyPixelsTF;
    public GameObject MyPixelsOBJ;
    //parameters
    public int pixScale; //diameter of pixel

    private int linePixNum = 10; //number of pixels in a line
    private int myScreemWidth = UnityEngine.Screen.width;
    private int myScreemHeight = UnityEngine.Screen.height;
    GameObject CanvasOBJ;

    //the row and clo number
    public int rowNum;
    public int cloNum;

    public GameObject[, ] pixArray;
    public Color[, ] pixColors; //
    // Sprite sp;
    // Material spMaterial;

    //--------------------------功能模式控制变量--------------------------------
    public int Control = 1;

    public GameObject PixelPrefab;
    //test bei
    //public int pixScaleNum = pixScale;

    void Start () {
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
        // sp = (Sprite)Resources.Load("Sprites/circle", typeof(Sprite)) as Sprite;
        // spMaterial = Resources.Load ("Materials/AtomMaterial") as Material;

        PixelPrefab = (GameObject) Resources.Load ("Prefabs/PixelPrefab"); //球形prefab
        // PixelPrefab = (GameObject) Resources.Load ("Prefabs/CubePixelPrefab"); //立方体形prefab
    }


    
    public void ShowPic(){
                    //bei  18/10/19

            OrinImageBg.gameObject.SetActive (true);
            //让显示图片的UI控件一开始先隐藏，当打开图片之后再激活
            //因为控件背景为白色才能正常显示图片，但背景为黑
            OrinImageBg.texture = Img;
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

        OrinImageBg.gameObject.SetActive (false);

        InstantiateController (); //效果脚本初始化

    }

    public GameObject CreateSprite (float y, float x, int row, int clo, Color col) {
        GameObject pixShape = Instantiate (PixelPrefab);
        float hue, saturate, brightness; //brightness 的值是0-1
        Color.RGBToHSV (col, out hue, out saturate, out brightness);

        //float brightScaleFactor = brightness * 5.0f;  //长短不一
        float brightScaleFactor = 1.0f; //长短一样

        // pixShape.GetComponent<Renderer> ().material = spMaterial;

        pixShape.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", col);

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

        ///粒子的拖尾 渐变
        TrailRenderer pixelTR = pixShape.GetComponent<TrailRenderer> ();
        Gradient gradient = new Gradient ();
        float alpha = 1.0f;
        gradient.SetKeys (
            new GradientColorKey[] { new GradientColorKey (col, 0.0f), new GradientColorKey (Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey (alpha, 0.0f), new GradientAlphaKey (alpha, 1.0f) }
        );
        pixelTR.colorGradient = gradient;

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

    // Texture2D ScaleTexture (Texture2D source, int targetWidth, int targetHeight) {
    //     Texture2D result = new Texture2D (targetWidth, targetHeight, source.format, false);
    //     float incX = (1.0f / (float) targetWidth);
    //     float incY = (1.0f / (float) targetHeight);

    //     for (int i = 0; i < result.height; ++i) {
    //         for (int j = 0; j < result.width; ++j) {
    //             Color newColor = source.GetPixelBilinear ((float) j / (float) result.width, (float) i / (float) result.height);
    //             result.SetPixel (j, i, newColor);
    //         }
    //     }
    //     result.Apply ();
    //     return result;
    // }

    // public Texture2D ResizePic (Texture2D pic) {
    //     int picW = pic.width;
    //     int picH = pic.height;
    //     if (Mathf.Max (picW, picH) == picW) {
    //         pic = ScaleTexture (pic, myScreemWidth, (int) (myScreemWidth * picH / picW));
    //     } else {
    //         pic = ScaleTexture (pic, (int) (picW * myScreemWidth / picH), myScreemWidth);
    //     }
    //     return pic;
    // }

    // IEnumerator GetTexture (string url) {
    //     WWW www = new WWW (url);
    //     yield return www;
    //     if (www.isDone && www.error == null) {
    //         Img = ResizePic (www.texture);

    //         //bei  18/10/19
    //         OrinImageBg.gameObject.SetActive (true);
    //         //让显示图片的UI控件一开始先隐藏，当打开图片之后再激活
    //         //因为控件背景为白色才能正常显示图片，但背景为黑

    //         OrinImageBg.texture = Img;

    //     }
    // }

    //bei  18/10/19
    public void Cancel () {
        if (OrinImageBg) {
            OrinImageBg.texture = null;
        }

        DestroyController (); //去除效果脚本

    }

    public void RefreshProcess_Destroy () { //去除前一效果产生的所有对象，进行新处理
        MyPixelsOBJ.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        PicProcess ();

    }

    public void RefreshProcess_Remove () {

        ResetPixelPosition ();
        MyPixelsOBJ.GetComponent<RemoveAllController> ().RemoveAllControllerComponent ();
        for (int i = MyPixelsOBJ.transform.childCount - 1; i >= 0; i--) { //重新进行MyPixel脚本的添加
            // Destroy (MyPixelsOBJ.transform.GetChild (i).gameObject.GetComponent<MyPixel> ());
            // MyPixelsOBJ.transform.GetChild (i).gameObject.AddComponent<MyPixel> ();
            MyPixelsOBJ.transform.GetChild (i).gameObject.GetComponent<MyPixel> ().Restart ();
        }
        InstantiateController ();

    }

    public void InstantiateController () { //效果添加在MyPixelsOBJ上的功能脚本
        //向量场流动
        if (Control == 1) {
            MyPixelsOBJ.AddComponent<DotFlowController> ();
            MyPixelsOBJ.AddComponent<FeildCheckMouse> ();
            ///
            /// test by z 4.7 10:51 will be delete if test done
            /// 
            /// 
            // GameObject ztqTempPS = GameObject.Find ("Particle_test2");
            // ztqTempPS.AddComponent<MyParticleController> ();
        }
        //暂时的粒子系统
        if (Control == 3) {
            GameObject BeverageBoxsOBJ = GameObject.FindWithTag ("BeverageBoxs");
            BeverageBoxsOBJ.AddComponent<BeveragesController> ();

            GameObject ztqTempPS = GameObject.Find ("Particle_test2");
            ztqTempPS.AddComponent<MyParticleController> ();
            ztqTempPS.GetComponent<ParticleSystem> ().Play ();

            MyPixelsOBJ.SetActive (false);

        }
        if (Control == 4) {

        }
        //如果是音乐可视化功能，在粒子产生后添加音乐可视化脚本
        if (Control == 9) {
            MyPixelsOBJ.AddComponent<AudioPeer> ();
            MyPixelsOBJ.AddComponent<AudioVisualizationController> ();
            AudioPeer APComponent = MyPixelsOBJ.GetComponent<AudioPeer> ();
            AudioVisualizationController AVCComponent = MyPixelsOBJ.GetComponent<AudioVisualizationController> ();
            AVCComponent.changePixelScale = true;
        }

        // Debug.Log ("Music Visualization setup");
        if (Control == 12) {
            GameObject BeverageBoxsOBJ = GameObject.FindWithTag ("BeverageBoxs");
            BeverageBoxsOBJ.AddComponent<BeveragesController> ();
        }

        if (Control == 14) {
            MyPixelsOBJ.AddComponent<NoiseFlowFieldController> ();
            NoiseFlowFieldController NFComponent = MyPixelsOBJ.GetComponent<NoiseFlowFieldController> ();
            Debug.Log ("add noise.cs");

        }

        // Debug.Log ("Music Visualization2 setup");
        if (Control == 13) {
            MyPixelsOBJ.AddComponent<WobblyGridController> ();
        }
    }

    public void DestroyController () { //在切换效果或退出效果时，消除效果添加在MyPixelsOBJ上的功能脚本
        if (Control == 1) {
            DotFlowController DFComponent = MyPixelsOBJ.GetComponent<DotFlowController> ();
            FeildCheckMouse FCComponent = MyPixelsOBJ.GetComponent<FeildCheckMouse> ();
            Destroy (DFComponent);
            Destroy (FCComponent);
        }
        if (Control == 3) {
            GameObject BeverageBoxsOBJ = GameObject.FindWithTag ("BeverageBoxs");
            BeveragesController BBoxComponent = BeverageBoxsOBJ.GetComponent<BeveragesController> ();
            BBoxComponent.DestroyChildren ();
            Destroy (BBoxComponent);

            GameObject ztqTempPS = GameObject.Find ("Particle_test2");
            MyParticleController MPCComponent = ztqTempPS.GetComponent<MyParticleController> ();

            Destroy (MPCComponent);

            ztqTempPS.GetComponent<ParticleSystem> ().Stop (); //为什么不执行？？？
            ztqTempPS.GetComponent<ParticleSystem> ().enableEmission = false;
            Debug.Log ("stop particles");

            MyPixelsOBJ.SetActive (true);

        }
        if (Control == 4) {
            // PicTranPixel PTCComponent=MyPixelsOBJ.GetComponent<PicTranPixel>();
            // Destroy (PTCComponent);
        }
        //如果是音乐可视化功能，在返回首页时需要暂停音乐的播放并去除音乐可视化的脚本
        if (Control == 12) {
            GameObject BeverageBoxsOBJ = GameObject.FindWithTag ("BeverageBoxs");
            BeveragesController BBoxComponent = BeverageBoxsOBJ.GetComponent<BeveragesController> ();
            BBoxComponent.DestroyChildren ();
            Destroy (BBoxComponent);
        }

        if (Control == 14) {
            NoiseFlowFieldController NFComponent = MyPixelsOBJ.GetComponent<NoiseFlowFieldController> ();
            Destroy (NFComponent);

        }

        if (Control == 9) {
            AudioVisualizationController AVComponent = MyPixelsOBJ.GetComponent<AudioVisualizationController> ();
            AudioPeer APComponent = MyPixelsOBJ.GetComponent<AudioPeer> ();
            AVComponent.StopMusic ();
            Destroy (AVComponent);
            Destroy (APComponent);
        }

        if (Control == 13) {
            WobblyGridController WGComponent = MyPixelsOBJ.GetComponent<WobblyGridController> ();
            Destroy (WGComponent);
        }

    }

    public void SetControlTo (int targetControl) { //每个效果按钮按下时的响应函数

        DestroyController ();

        Control = targetControl;

        RefreshProcess_Destroy ();

    }

}

public static class ExtensionMethods { //一个扩展的静态类，可以直接用类名调用方法，这里的方法是自定义的。方法不多，暂时存放在这。
    public static float Map (this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}