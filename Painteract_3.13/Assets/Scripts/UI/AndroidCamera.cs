using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class AndroidCamera : MonoBehaviour {

    GameObject CanvasOBJ;

    public Texture2D Img;
    public Texture2D Img2;

    public Vector2 ImgSize = new Vector2 (0, 0);

    private int myScreemWidth = UnityEngine.Screen.width;
    private int myScreemHeight = UnityEngine.Screen.height;

    public bool isGetTextureDone = false; //是否获取图片成功变量

    public int TargetImg;

    private void Awake ()

    {

        //将挂载此脚本的物体的名字改为和java脚本中的名字一致

        this.gameObject.name = "BGPlane";

        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        Img = CanvasOBJ.GetComponent<ReadPic> ().Img;
        Img2 = CanvasOBJ.GetComponent<PicTransController> ().Img2;

    }

    public void SetTargetImg (int targetImg) {

        TargetImg = targetImg;

    }

    //打开相册    

    public void OpenPhoto (int targetImg)

    {

        SetTargetImg (targetImg);

        AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");

        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject> ("currentActivity");

        jo.Call ("OpenGallery");

    }

    //打开相机  

    public void OpenCamera ()

    {

        AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");

        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject> ("currentActivity");

        jo.Call ("takephoto");

    }

    //获取图片的调用

    public void GetImagePath (string imagePath)

    {

        if (imagePath == null)

            return;

        StartCoroutine ("LoadImage", imagePath);

    }

    public void GetTakeImagePath (string imagePath)

    {

        if (imagePath == null)

            return;

        StartCoroutine ("LoadImage", imagePath);

    }

    private IEnumerator LoadImage (string imagePath)

    {

        WWW www = new WWW ("file://" + imagePath);
        string filepath = "file://" + imagePath;

        yield return www;

        if (www.isDone && www.error == null)

        {
            //成功读取图片，写自己的逻辑  

            if (TargetImg == 1) {
                CanvasOBJ.GetComponent<UIManager> ().ShowImagePage ();
                //GameObject.FindWithTag ("Player").GetComponent<MeshRenderer> ().material.color = Color.green;
                CanvasOBJ.GetComponent<ReadPic> ().Img = ResizePic (www.texture);
                CanvasOBJ.GetComponent<ReadPic> ().ShowPic (); //让图片显示到屏幕上

            }

            if (TargetImg == 2) {
                //GameObject.FindWithTag ("Player").GetComponent<MeshRenderer> ().material.color = Color.yellow;
                CanvasOBJ.GetComponent<PicTransController> ().Img2 = ResizePic (www.texture);
                CanvasOBJ.GetComponent<PicTransController> ().AfterOpenPic2 (); //执行打开第二张照片后的操作
            }

        } else

        {
            //GameObject.FindWithTag ("Player").GetComponent<MeshRenderer> ().material.color = Color.red;
            Debug.LogError ("LoadImage>>>www.error:" + www.error);
        }

    }

    Texture2D ResizePic (Texture2D pic) {
        int picW = pic.width;
        int picH = pic.height;

        if (Mathf.Max (picW, picH) == picW) {
            pic = ScaleTexture (pic, myScreemWidth, (int) (myScreemWidth * picH / picW));
        } else {
            pic = ScaleTexture (pic, (int) (picW * myScreemWidth / picH), myScreemWidth);
        }

        return pic;

    }

    Texture2D ScaleTexture (Texture2D source, int targetWidth, int targetHeight) {
        Texture2D result = new Texture2D (targetWidth, targetHeight, source.format, false);
        float incX = (1.0f / (float) targetWidth);
        float incY = (1.0f / (float) targetHeight);

        ImgSize = new Vector2 (targetWidth, targetHeight);
        SetOrinImageBgScale ();

        for (int i = 0; i < result.height; ++i) {
            for (int j = 0; j < result.width; ++j) {
                Color newColor = source.GetPixelBilinear ((float) j / (float) result.width, (float) i / (float) result.height);
                result.SetPixel (j, i, newColor);
            }
        }
        result.Apply ();
        return result;
    }

    public void SetOrinImageBgScale () {

        CanvasOBJ.GetComponent<ReadPic> ().OrinImageBg.GetComponent<RectTransform> ().sizeDelta = ImgSize;
    }

    //电脑端打开图片函数
    public void AddHead () {
        OpenFileDialog od = new OpenFileDialog ();
        od.Title = "请选择头像图片";
        od.Multiselect = false;
        od.Filter = "图片文件(*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp";

        if (od.ShowDialog () == DialogResult.OK) {
            StartCoroutine (GetTexture ("file://" + od.FileName));
        }

    }

    IEnumerator GetTexture (string url) {
        WWW www = new WWW (url);
        yield return www;
        if (www.isDone && www.error == null) {
            CanvasOBJ.GetComponent<UIManager> ().ShowImagePage ();

            CanvasOBJ.GetComponent<ReadPic> ().Img = ResizePic (www.texture);
            CanvasOBJ.GetComponent<ReadPic> ().ShowPic (); //让图片显示到屏幕上

        }
    }

}