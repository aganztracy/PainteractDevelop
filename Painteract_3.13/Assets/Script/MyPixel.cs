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
                //将形成弹簧网络的功能脚本添加到粒子上
                gameObject.AddComponent<SpringGridController> ();
                break;
            case 4:
                //将鼠标拖拽移动粒子相关的脚本添加到粒子上
                gameObject.AddComponent<DragObj> ();
                //将形成弹簧网络的功能脚本添加到粒子上
                gameObject.AddComponent<SpringGridController> ();
                break;
            case 5:
                // //将麦克风声音可视化相关的脚本添加到粒子上
                // if (Row == 0 && Clo == 0) {
                //     gameObject.AddComponent<MicrophoneVisualizationController> ();
                // }

                // //将形成弹簧网络的功能脚本添加到粒子上
                // gameObject.AddComponent<SpringGridController> ();

                break;
            case 6:
                // 点击爆炸粒子
                gameObject.AddComponent<ParticleBoomController> ();
                break;
            case 7:
                //点击颜料溅开的测试
                gameObject.AddComponent<PigmentBoomController> ();

                break;
            case 8:
                // //弹簧三维连接测试
                // //将鼠标拖拽移动粒子相关的脚本添加到粒子上
                // gameObject.AddComponent<DragObj> ();
                // //将形成弹簧网络的功能脚本添加到粒子上
                // gameObject.AddComponent<Spring3DController> ();
                // gameObject.AddComponent<ParticleBoomController> ();

                // 给每个粒子添加刚体组件
		        gameObject.AddComponent<Rigidbody> ();
                gameObject.AddComponent<Rigidbody> ().mass = 10;
                gameObject.AddComponent<Rigidbody> ().drag = 0.01f;
                break;

            default:
                break;
        }
    }
}