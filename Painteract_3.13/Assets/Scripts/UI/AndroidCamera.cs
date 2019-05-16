using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
// using System.Drawing;
// using System.Drawing.Imaging;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class AndroidCamera : MonoBehaviour {

    GameObject CanvasOBJ;

    public Texture2D Img;
    public Texture2D Img2;

    public Vector2 ImgSize = new Vector2 (0, 0);

    private int myScreemWidth = UnityEngine.Screen.width;
    private int myScreemHeight = UnityEngine.Screen.height;

    public bool isGetTextureDone = false; //是否获取图片成功变量

    public int TargetImg;

    /// <summary>
    /// socket数据传输部分

    IPAddress ip = IPAddress.Parse ("172.20.10.2"); //换了wifi  记得修改172.20.10.2（stone）
    byte[] data;
    bool receivedPic = false;
    Texture2D m_Texure;


    TcpClient client;
    public void Client () {
        client = new TcpClient ();
        data = new byte[client.ReceiveBufferSize];

        try {
            client.Connect (ip, 10001); //同步方法，连接成功、抛出异常、服务器不存在等之前程序会被阻塞
        } catch (Exception ex) {
            Debug.Log ("客户端连接异常：" + ex.Message);
        }
        // client.GetStream ().BeginRead (data, 0, System.Convert.ToInt32 (client.ReceiveBufferSize), ReceiveMessage, null); //客户端接收消息
        Debug.Log ("LocalEndPoint = " + client.Client.LocalEndPoint + ". RemoteEndPoint = " + client.Client.RemoteEndPoint);

    }
    public void SendMsgToServer (Byte[] buffer) { //客户端发送数据部分
        //为了防止socket连接出现异常,所以使用try...catch语句 
        try {
            NetworkStream streamToServer = client.GetStream (); //获得客户端的流
            // byte[] buffer = Encoding.Unicode.GetBytes (msg); //将字符串转化为二进制
            streamToServer.Write (buffer, 0, buffer.Length); //将转换好的二进制数据写入流中并发送
            Debug.Log ("buffer.Length:" + buffer.Length); //0
            Debug.Log ("发送了图像的二进制数据流");
        } catch (SocketException e) {
            Debug.Log (e.Data.ToString ());
            Debug.Log ("服务端产生异常：" + e.Message);
        }
    }
    //// Convert Image to Byte[]
    // private byte[] ImageToByte (Image image) {
    //     ImageFormat format = image.RawFormat;
    //     using (MemoryStream ms = new MemoryStream ()) {
    //         if (format.Equals (ImageFormat.Jpeg)) {
    //             image.Save (ms, ImageFormat.Jpeg);
    //         } else if (format.Equals (ImageFormat.Png)) {
    //             image.Save (ms, ImageFormat.Png);
    //         } else if (format.Equals (ImageFormat.Bmp)) {
    //             image.Save (ms, ImageFormat.Bmp);
    //         } else if (format.Equals (ImageFormat.Gif)) {
    //             image.Save (ms, ImageFormat.Gif);
    //         } else if (format.Equals (ImageFormat.Icon)) {
    //             image.Save (ms, ImageFormat.Icon);
    //         }
    //         byte[] buffer = new byte[ms.Length];
    //         //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
    //         ms.Seek (0, SeekOrigin.Begin);
    //         ms.Read (buffer, 0, buffer.Length);
    //         return buffer;
    //     }
    // }
    /// </summary>

    private void Awake () {

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

                Client (); //新建一个客户端，客户端连接服务器，确保服务器是打开的。
                //发送数据给客户端
               
                Texture2D testTex;
                Texture2D decopmpresseTex;
                testTex=(Texture2D)CanvasOBJ.GetComponent<ReadPic> ().Img ;////用ResizePic后的图数据量更小，更快传输
                testTex.Compress(false);
                // byte[] imgdData=testTex.EncodeToJPG ();
                decopmpresseTex= testTex.DeCompress();
                byte[] imgdData = decopmpresseTex.EncodeToJPG ();
                SendMsgToServer (imgdData);

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

public static class ExtensionMethod {
    public static Texture2D DeCompress (this Texture2D source) {
        RenderTexture renderTex = RenderTexture.GetTemporary (
            source.width,
            source.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear);

        UnityEngine.Graphics.Blit (source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D (source.width, source.height);
        readableText.ReadPixels (new Rect (0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply ();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary (renderTex);
        return readableText;
    }
}