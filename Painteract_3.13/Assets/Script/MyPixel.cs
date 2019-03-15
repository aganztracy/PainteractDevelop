using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPixel : MonoBehaviour {
    int Control = 1; //which case 1-3 by ztq //4-6 by ybg 
    int scl = 10; //pixels' scale

    public int Row;
    public int Clo;
    public Color Col;

    public Vector3 PosXY;

    // public GameObject DotFlowOBJ;

    public GameObject CanvasOBJ; // 获取挂载该脚本的"Canvas"物体，为了获取ReadPic.cs中的对象和方法

    void Start () {

        //获取功能模式控制变量
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        Control = CanvasOBJ.GetComponent<ReadPic> ().Control;

        switch (Control) {
            case 1:
                gameObject.AddComponent<DotFlowController> ();
                break;
            case 2:
                gameObject.AddComponent<AttractorController> ();
                break;
            case 3:

                break;
            case 4:
                //将鼠标拖拽移动粒子相关的脚本添加到粒子上
                gameObject.AddComponent<DragObj> ();
                //将形成弹簧网络的功能脚本添加到粒子上
                gameObject.AddComponent<SpringGrid> ();
                break;
            case 5:
                //将鼠标拖拽移动粒子相关的脚本添加到粒子上
                gameObject.AddComponent<DragObj> ();
                //将形成铰链网络的功能脚本添加到粒子上
                gameObject.AddComponent<HingeGrid> ();
                break;
            case 6:
                // 点击爆炸粒子
                gameObject.AddComponent<ParticlesBoom> ();
                break;
            case 7:
                //点击颜料溅开的测试
                gameObject.AddComponent<PigmentBoom> ();

                break;
            case 8:
                break;

            default:
                break;
        }
    }

    //change the force of the pixels

    //FOR CASE1------------------------------------------------------------------------------------------------------------------------------

    //CASE1 END----------------------------------------------------------------------------------------------------------------------------------

    //FOR CASE2-----------------------------------------------------------------------------------------------------------------------------------

    //CASE2 END----------------------------------------------------------------------------------------------------------------------------------

}