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
    public GameObject MyPixelsOBJ;

    void Start () {

        //获取功能模式控制变量
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        MyPixelsOBJ = GameObject.FindWithTag("MyPixels");
        Control = CanvasOBJ.GetComponent<ReadPic> ().Control;

        switch (Control) {

            /// <summary>
            /// Flow 类别效果
            /// 1-8
            /// </summary>
            case 1:
                gameObject.AddComponent<DotFlowController> ();
                break;
            case 2:
                gameObject.AddComponent<AttractorController> ();
                break;
            case 3:

                break;
            case 4:
                gameObject.AddComponent<PicTranPixel> ();

                break;
            case 5:
                break;
            case 6:

                break;
            case 7:

                break;
            case 8:

                break;

                /// <summary>
                /// Music 类别效果
                /// 9
                /// </summary>

            case 9: //=========================================================音乐可视化

                break;

                /// <summary>
                /// Physic 类别效果
                /// 10-13
                /// </summary>
            case 10: //=========================================================可拖拽弹簧网格
                //将鼠标拖拽移动粒子相关的脚本添加到粒子上
                gameObject.AddComponent<DragObj> ();
                //将形成弹簧网络的功能脚本添加到粒子上
                gameObject.AddComponent<SpringGridController> ();

                break;
            case 11: //=========================================================三维粒子效果
                //弹簧三维连接测试
                //将鼠标拖拽移动粒子相关的脚本添加到粒子上
                gameObject.AddComponent<Rigidbody> ();
                gameObject.AddComponent<DragObj> ();
                //将形成弹簧网络的功能脚本添加到粒子上
                gameObject.AddComponent<Spring3DController> ();

                break;
            case 12: //=========================================================粒子饮料效果
                // 给每个粒子添加刚体组件
                //gameObject.AddComponent<Rigidbody> ();
                gameObject.AddComponent<Rigidbody> ();
                gameObject.GetComponent<Rigidbody> ().mass = 10;
                gameObject.GetComponent<Rigidbody> ().drag = 0.01f;
                break;
            case 13: //=========================================================WobblyGrid

                //gameObject.AddComponent<WobblyGridController> ();            

                break;

                /// <summary>
                /// Other 类别效果
                /// 14-16
                /// </summary>
            case 14: //=========================================================3D Noise Flow Field
                gameObject.AddComponent<FlowFieldParticle> ();
                
                MyPixelsOBJ.GetComponent<NoiseFlowFieldController>()._particles.Add (this.GetComponent<FlowFieldParticle> ());
                MyPixelsOBJ.GetComponent<NoiseFlowFieldController>()._particleMeshRenderer.Add (this.GetComponent<MeshRenderer> ());
                MyPixelsOBJ.GetComponent<NoiseFlowFieldController>().setRandomPosition(this.gameObject);
                //Debug.Log ("added____________________");
                break;

            case 15: //=========================================================爆炸粒子系统效果
                // 点击爆炸粒子
                gameObject.AddComponent<ParticleBoomController> ();
                break;
            case 16: //=========================================================颜料溅开效果
                //点击颜料溅开的测试
                gameObject.AddComponent<PigmentBoomController> ();
                break;

            default:
                break;
        }
    }

    public void Restart () {

        switch (Control) {

            /// <summary>
            /// Flow 类别效果
            /// 1-8
            /// </summary>
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

                /// <summary>
                /// Music 类别效果
                /// 9
                /// </summary>

            case 9: //=========================================================音乐可视化

                break;

                /// <summary>
                /// Physic 类别效果
                /// 10-13
                /// </summary>
            case 10: //=========================================================可拖拽弹簧网格
                //将鼠标拖拽移动粒子相关的脚本添加到粒子上
                gameObject.AddComponent<DragObj> ();
                //将形成弹簧网络的功能脚本添加到粒子上
                gameObject.AddComponent<SpringGridController> ();

                break;
            case 11: //=========================================================三维粒子效果
                //弹簧三维连接测试
                //将鼠标拖拽移动粒子相关的脚本添加到粒子上
                gameObject.AddComponent<Rigidbody> ();
                gameObject.AddComponent<DragObj> ();
                //将形成弹簧网络的功能脚本添加到粒子上
                gameObject.AddComponent<Spring3DController> ();

                break;
            case 12: //=========================================================粒子饮料效果
                // 给每个粒子添加刚体组件
                //gameObject.AddComponent<Rigidbody> ();
                gameObject.AddComponent<Rigidbody> ();
                gameObject.AddComponent<Rigidbody> ().mass = 10;
                gameObject.AddComponent<Rigidbody> ().drag = 0.01f;
                break;
            case 13: //=========================================================WobblyGrid

                //gameObject.AddComponent<WobblyGridController> ();            

                break;

                /// <summary>
                /// Other 类别效果
                /// 14-16
                /// </summary>
            case 14: //=========================================================3D Noise Flow Field
                gameObject.AddComponent<FlowFieldParticle> ();
                gameObject.GetComponent<TrailRenderer>().startWidth = Random.Range(1,50);
                //gameObject.GetComponent<TrailRenderer>().endWidth = gameObject.GetComponent<TrailRenderer>().startWidth;
                // Debug.Log ("added____________________");
                break;

            case 15: //=========================================================爆炸粒子系统效果
                // 点击爆炸粒子
                gameObject.AddComponent<ParticleBoomController> ();
                break;
            case 16: //=========================================================颜料溅开效果
                //点击颜料溅开的测试
                gameObject.AddComponent<PigmentBoomController> ();
                break;

            default:
                break;
        }
    }

    public void RemoveControllerComponent () {

        switch (Control) {

            /// <summary>
            /// Flow 类别效果
            /// 1-8
            /// </summary>
            case 1:
                Destroy (gameObject.GetComponent<DotFlowController> ());
                break;
            case 2:
                Destroy (gameObject.GetComponent<AttractorController> ());
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

                /// <summary>
                /// Music 类别效果
                /// 9
                /// </summary>

            case 9: //=========================================================音乐可视化

                break;

                /// <summary>
                /// Physic 类别效果
                /// 10-13
                /// </summary>
            case 10: //=========================================================可拖拽弹簧网格
                Destroy (gameObject.GetComponent<Rigidbody> ());
                Destroy (gameObject.GetComponent<DragObj> ());
                Destroy (gameObject.GetComponent<SpringGridController> ());

                break;
            case 11: //=========================================================三维粒子效果
                //弹簧三维连接测试
                //将鼠标拖拽移动粒子相关的脚本添加到粒子上
                Destroy (gameObject.GetComponent<Rigidbody> ());
                Destroy (gameObject.GetComponent<DragObj> ());
                //将形成弹簧网络的功能脚本添加到粒子上
                Destroy (gameObject.GetComponent<Spring3DController> ());

                break;
            case 12: //=========================================================粒子饮料效果
                break;
            case 13: //=========================================================WobblyGrid

                //gameObject.AddComponent<WobblyGridController> ();            

                break;

                /// <summary>
                /// Other 类别效果
                /// 14-16
                /// </summary>
            case 14: //=========================================================3D Noise Flow Field
                Destroy (gameObject.GetComponent<FlowFieldParticle> ());
                break;

            case 15: //=========================================================爆炸粒子系统效果
                // 点击爆炸粒子
                Destroy (gameObject.GetComponent<ParticleBoomController> ());
                break;
            case 16: //=========================================================颜料溅开效果
                //点击颜料溅开的测试
                Destroy (gameObject.GetComponent<PigmentBoomController> ());
                break;

            default:
                break;
        }

    }
}