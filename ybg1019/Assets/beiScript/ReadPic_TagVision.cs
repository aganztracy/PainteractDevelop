using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ReadPic_TagVision : MonoBehaviour {

    public static Texture2D img = null;
    public static Color[,] imageColor2d;
    public RawImage rawImageObj;
    public Transform myPixels;
    //parameters
    private int pixScale;//diameter of pixel
    private int linePixNum = 40;//number of pixels in a line
    private int myScreemWidth = UnityEngine.Screen.width;

    //bei  10/22
    //the row and clo number
    public int rowPixelNum;
    public int cloPixelNum;
    //the pixel number
    public int pixelNum = 0;
    //链接关节游戏对象
    GameObject connectedObjRow = null;
    GameObject connectedObjClo = null;
    //当前链接的弹簧关节组件
    SpringJoint jointComponentRow = null;
    SpringJoint jointComponentClo = null;

    public void addHead()
    {
        OpenFileDialog od = new OpenFileDialog();
        od.Title = "请选择头像图片";
        od.Multiselect = false;
        od.Filter = "图片文件(*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp";

        if (od.ShowDialog() == DialogResult.OK)
        {
            StartCoroutine(GetTexture("file://" + od.FileName));
        }

    }
    public void picProcess()
    {
        if (img == null) return;
        int width = Mathf.FloorToInt(img.width);
        int height = Mathf.FloorToInt(img.height);
        imageColor2d = new Color[height, width];//initialize
        Color[] pix = img.GetPixels(0, 0, width, height);
        pixScale = (int)Mathf.Floor(myScreemWidth / linePixNum);//像素直径是屏幕宽度除以列像素数

        //covert 1d color[] to 2d color[,]
        for (int row = 0; row < height; row = row + pixScale)
        {
            rowPixelNum++;//获取行数
            for (int clo = 0; clo < width; clo = clo + pixScale)
            {
                imageColor2d[row, clo] = pix[width * row + clo];//pay attention to the writing style of 2d array in c#
                createSprite(row, clo, imageColor2d[row, clo]);
                if (row == 0)
                {
                    cloPixelNum++;//获取列数
                }

            }

        }
        rawImageObj.gameObject.SetActive(false);

        Debug.Log(rowPixelNum + ":" + cloPixelNum);//显示最终行列数

        //bei  18/10/19
        //以某种方式打开图片后，就不显示打开图片按钮
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(false);
        //并且显示处理图片和取消按钮
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);
        //显示变换按钮和首页按钮
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.GetChild(5).gameObject.SetActive(true);
        //显示主页名称logo
        GameObject.Find("Canvas").transform.GetChild(8).gameObject.SetActive(false);



    }


    public void createSprite(float y, float x, Color col)
    {
        GameObject pixShape = new GameObject();
        pixShape.AddComponent<SpriteRenderer>();
        pixShape.AddComponent<TrailRenderer>();
        pixShape.AddComponent<BoxCollider>();


        //bei 10/22
        //pixShape.AddComponent<myPixel>();//给每个粒子添加脚本组件
        pixShape.AddComponent<Rigidbody>();
        pixShape.GetComponent<Rigidbody>().useGravity = false;
        //给粒子添加tag编号，便于寻找
        String pixelTag = pixelNum.ToString();
        AddTag(pixelTag, pixShape);//应用函数



        //锁定位置，但可以旋转，有弹性反应
        pixShape.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        pixShape.AddComponent<SpringJoint>();

        //物理效果，弹性网状结构
        if (y == 0)//最下方一排，不进行连接
        {

        }
        else if (x == 0)//最左侧一排，只与其下方一排连接
        {
            pixShape.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
            jointComponentRow = pixShape.AddComponent<SpringJoint>();

            //寻找下方一个连接粒子的tag
            String pixelUnder = (pixelNum - cloPixelNum).ToString();
            connectedObjRow = GameObject.FindWithTag(pixelUnder);
            jointComponentRow.connectedBody = connectedObjRow.GetComponent<Rigidbody>();

        }

        if (y != 0 && x != 0)
        {

            jointComponentRow = pixShape.AddComponent<SpringJoint>();
            jointComponentClo = pixShape.AddComponent<SpringJoint>();

            //寻找下方一个连接粒子的tag
            String pixelUnder = (pixelNum - cloPixelNum).ToString();
            connectedObjRow = GameObject.FindWithTag(pixelUnder);
            jointComponentRow.connectedBody = connectedObjRow.GetComponent<Rigidbody>();

            //寻找左边一个连接粒子的tag
            String pixelLeft = (pixelNum - 1).ToString();
            connectedObjClo = GameObject.FindWithTag(pixelLeft);
            jointComponentClo.connectedBody = connectedObjClo.GetComponent<Rigidbody>();

            pixelNum++;

        }


        SpriteRenderer spr = pixShape.GetComponent<SpriteRenderer>();
        Sprite sp = (Sprite)Resources.Load("sprites/circle", typeof(Sprite)) as Sprite;

        TrailRenderer trialr = pixShape.GetComponent<TrailRenderer>();
        trialr.material = Resources.Load("Material/1") as Material;
        //Debug.Log(Resources.Load("Material/1")as Material);

        trialr.material.color = col;
        trialr.startWidth = pixScale;

        spr.color = col;
        spr.sprite = sp;

        pixShape.transform.SetParent(myPixels);
        pixShape.transform.localScale = new Vector3(pixScale, pixScale, 1);
        pixShape.transform.localPosition = new Vector3(x, y, 10f);//Sets the coordinates relative to the parent object 


    }
    Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);
        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);

        for (int i = 0; i < result.height; ++i)
        {
            for (int j = 0; j < result.width; ++j)
            {
                Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                result.SetPixel(j, i, newColor);
            }
        }
        result.Apply();
        return result;
    }

    public Texture2D resizePic(Texture2D pic)
    {
        int picW = pic.width;
        int picH = pic.height;
        if (Mathf.Max(picW, picH) == picW)
        {
            pic = ScaleTexture(pic, myScreemWidth, (int)(myScreemWidth * picH / picW));
        }
        else
        {
            pic = ScaleTexture(pic, (int)(picW * myScreemWidth / picH), myScreemWidth);
        }
        return pic;
    }

    IEnumerator GetTexture(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.isDone && www.error == null)
        {
            img = resizePic(www.texture);

            //bei  18/10/19
            GameObject.Find("Canvas").transform.GetChild(6).gameObject.SetActive(true);
            //让显示图片的UI控件一开始先隐藏，当打开图片之后再激活
            //因为控件背景为白色才能正常显示图片，但背景为黑

            rawImageObj.texture = img;

            //bei  18/10/19
            //以某种方式打开图片后，就不显示打开图片按钮
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(false);
            //并且显示处理图片和取消按钮
            GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
            //不显示主页名称logo
            GameObject.Find("Canvas").transform.GetChild(8).gameObject.SetActive(false);


        }


    }


    //bei  18/10/19
    public void Cancel()
    {
        if (rawImageObj)
        {
            rawImageObj.texture = null;
        }


        //不显示处理图片和取消按钮
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
        //并且显示打开图片按钮
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);
        //不显示缺省rawimage
        GameObject.Find("Canvas").transform.GetChild(6).gameObject.SetActive(false);
        //不显示变换按钮和首页按钮
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(5).gameObject.SetActive(false);
        //不显示主页名称logo
        GameObject.Find("Canvas").transform.GetChild(8).gameObject.SetActive(true);

    }

    public void AddTag(string tag, GameObject obj)
    {
        if (!isHasTag(tag))
        {
            SerializedObject tagManager = new SerializedObject(obj);//序列化物体
            SerializedProperty it = tagManager.GetIterator();//序列化属性
            while (it.NextVisible(true))//下一属性的可见性
            {
                if (it.name == "m_TagString")
                {
                    Debug.Log(it.stringValue);
                    it.stringValue = tag;
                    tagManager.ApplyModifiedProperties();
                }
            }
        }
    }

    public bool isHasTag(string tag)
    {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.tags[i].Equals(tag))
                return true;
        }
        return false;
    }
}
