using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mySpringCloth : MonoBehaviour {
    //应该是补充pixels的操作，不是调用
    //利用函数参数传递的方式
    //实在不行可以放弃在每个粒子生成时候进行连接
    //而是在所有粒子生成完成后连接

    public GameObject myPixels;
    public GameObject processButton;

    public int rowPixelNum;
    public int cloPixelNum;


    // Use this for initialization
    void Start () {

       // The initial coordinate
        myPixels = GameObject.Find("myPixels");
        processButton = GameObject.Find("processButton");
        

        ////bei 10/22



        ////将粒子添加到数组中
        //pixelsArray.Add(pixShape);

        ////弹簧连接辅助线
        ////LineRenderer line = pixShape.gameObject.AddComponent<LineRenderer>();


        ////锁定位置，但可以旋转，有弹性反应
        ////pixShape.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ| RigidbodyConstraints.FreezePositionX| RigidbodyConstraints.FreezePositionY;
        ////pixShape.AddComponent<SpringJoint>();

        ////物理效果，弹性网状结构
        //if (y == 0)//最下方一排，不进行连接
        //{
        //    // pixShape.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        //}
        //else if (x == 0)//最左侧一排，只与其下方一排连接
        //{
        //    //pixShape.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        //    jointComponentRow = pixShape.AddComponent<SpringJoint>();
        //    jointComponentRow.maxDistance = 3;

        //    //连接到下方一个连接粒子
        //    connectedObjRow = (GameObject)pixelsArray[pixelNum - cloPixelNum];
        //    jointComponentRow.connectedBody = connectedObjRow.GetComponent<Rigidbody>();

        //    //给连接的两个粒子之间画线
        //    //line.positionCount = 2;
        //    //line.SetPosition(0, pixShape.transform.position);
        //    //line.SetPosition(1, connectedObjRow.transform.position);



        //}

        //if (y != 0 && x != 0)
        //{

        //    jointComponentRow = pixShape.AddComponent<SpringJoint>();
        //    jointComponentClo = pixShape.AddComponent<SpringJoint>();
        //    jointComponentRow.maxDistance = 3;
        //    jointComponentClo.maxDistance = 3;

        //    //连接到下方一个连接粒子
        //    connectedObjRow = (GameObject)pixelsArray[pixelNum - cloPixelNum];
        //    jointComponentRow.connectedBody = connectedObjRow.GetComponent<Rigidbody>();


        //    //连接到左方一个连接粒子
        //    connectedObjClo = (GameObject)pixelsArray[pixelNum - 1];
        //    jointComponentClo.connectedBody = connectedObjClo.GetComponent<Rigidbody>();


        //    //给连接的三个粒子之间画线
        //    //line.positionCount = 3;

        //    //line.SetPosition(1, pixShape.transform.position);
        //    //line.SetPosition(0, connectedObjRow.transform.position);
        //    //line.SetPosition(2, connectedObjClo.transform.position);
        //}

        ////if (y == 39 * pixScale || x == 36 * pixScale)
        //if (y == 39 * pixScale)//最上方一行锁定
        //                       //-----------------------------------------------------------数字会随着图像而变，要修改
        //{
        //    Debug.Log("lock" + y + ":" + x);
        //    pixShape.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        //}



    }

    // Update is called once per frame
    void Update () {

    }

    public void addSpring()
    {

    }


}
