using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidCamera : MonoBehaviour
{

    GameObject CanvasOBJ;


     private void Awake()

    {

        //将挂载此脚本的物体的名字改为和java脚本中的名字一致

        this.gameObject.name = "BGPlane";

        CanvasOBJ = GameObject.FindWithTag ("Canvas");

    }

 

    //打开相册    

    public void OpenPhoto()

    {

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

        jo.Call("OpenGallery");

    }

 

    //打开相机  

    public void OpenCamera()

    {

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

        jo.Call("Takephoto");

    }

 

    //获取图片的调用

    public void GetImagePath(string imagePath)

    {

        if (imagePath == null)

            return;

        StartCoroutine("LoadImage", imagePath);

    }

    

    private IEnumerator LoadImage(string imagePath)

    {

        WWW www = new WWW("file://" + imagePath);

        yield return www;

        if (www.error == null)

        {

            //成功读取图片，写自己的逻辑  

            //this.GetComponent<MeshRenderer>().material.mainTexture = www.texture;

            CanvasOBJ.GetComponent<ReadPic>().Img = www.texture;

        }

        else

        {

            Debug.LogError("LoadImage>>>www.error:" + www.error);

        }

}
}
