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

                break;
            case 5:
                break;
            case 6:

                break;
            case 7:

                break;
            case 8:

                break;
            case 9: //=========================================================可拖拽弹簧网格
                //将鼠标拖拽移动粒子相关的脚本添加到粒子上
                gameObject.AddComponent<DragObj> ();
                //将形成弹簧网络的功能脚本添加到粒子上
                gameObject.AddComponent<SpringGridController> ();
                break;
            case 10: //=========================================================3D Noise Flow Field
                gameObject.AddComponent<FlowFieldParticle> ();

                break;
            case 11: //=========================================================音乐可视化

                break;
            case 12: //=========================================================爆炸粒子系统效果
                // 点击爆炸粒子
                gameObject.AddComponent<ParticleBoomController> ();
                break;
            case 13: //=========================================================颜料溅开效果
                //点击颜料溅开的测试
                gameObject.AddComponent<PigmentBoomController> ();
                break;
            case 14: //=========================================================三维粒子效果
                //弹簧三维连接测试
                //将鼠标拖拽移动粒子相关的脚本添加到粒子上
                gameObject.AddComponent<DragObj> ();
                //将形成弹簧网络的功能脚本添加到粒子上
                gameObject.AddComponent<Spring3DController> ();

                break;
            case 15: //=========================================================粒子饮料效果
                // 给每个粒子添加刚体组件
                gameObject.AddComponent<Rigidbody> ();
                gameObject.AddComponent<Rigidbody> ().mass = 10;
                gameObject.AddComponent<Rigidbody> ().drag = 0.01f;
                break;
            case 16: //=========================================================

                //gameObject.AddComponent<WobblyGridController> ();            

                break;

            default:
                break;
        }
    }
}