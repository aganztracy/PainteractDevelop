using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class AndroidCamera : MonoBehaviour {

    GameObject CanvasOBJ;

    Texture2D Img;
    Texture2D Img2;

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
        SetTargetImg(targetImg);

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

        if (www.error == null)

        {
            //成功读取图片，写自己的逻辑  
            //CanvasOBJ.GetComponent<ReadPic>().AddPic("file://" + imagePath);
            if (TargetImg == 1) {
                //GetTexture1 ("file://" + imagePath);
                CanvasOBJ.GetComponent<ReadPic> ().AddPic(filepath);
            }

            if (TargetImg == 2) {
                //GetTexture2 ("file://" + imagePath);
                CanvasOBJ.GetComponent<PicTransController> ().AddPic2 (filepath);
            }

        } else

        {
            Debug.LogError ("LoadImage>>>www.error:" + www.error);
        }

    }

    IEnumerator GetTexture (string url, Texture2D Img) {
        WWW www = new WWW (url);
        yield return www;
        if (www.isDone && www.error == null) {
            Img = ResizePic (www.texture);
            isGetTextureDone = true;
        }
    }

    IEnumerator GetTexture1 (string url) {
        WWW www = new WWW (url);
        yield return www;
        if (www.isDone && www.error == null) {
            CanvasOBJ.GetComponent<ReadPic> ().Img = ResizePic (www.texture);
            isGetTextureDone = true;
        }
    }

        IEnumerator GetTexture2 (string url) {
        WWW www = new WWW (url);
        yield return www;
        if (www.isDone && www.error == null) {
            CanvasOBJ.GetComponent<PicTransController> ().Img2 = ResizePic (www.texture);
            isGetTextureDone = true;
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

        for (int i = 0; i < result.height; ++i) {
            for (int j = 0; j < result.width; ++j) {
                Color newColor = source.GetPixelBilinear ((float) j / (float) result.width, (float) i / (float) result.height);
                result.SetPixel (j, i, newColor);
            }
        }
        result.Apply ();
        return result;
    }

}