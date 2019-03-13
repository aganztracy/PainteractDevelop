using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPixel : MonoBehaviour
{
    int Control =1;//which case 1-3 by ztq //4-6 by ybg 
    int scl = 10;//pixels' scale

    public int Row;
    public int Clo;
    public Color Col;

    Vector2 iniScreenPos;
    Vector2 pixScreenPos;//will update
    Vector2 iniPos;
    Vector2 pos;//will update

    Vector2 vel;
    Vector2 acc;
    float maxspeed;
    

    public GameObject DotFlowOBJ;
    public GameObject MyPixelsOBJ;
    public GameObject CanvasOBJ;// 获取挂载该脚本的"Canvas"物体，为了获取ReadPic.cs中的对象和方法

    void Start()
    {
        //The initial coordinate
        MyPixelsOBJ = GameObject.Find("MyPixels");
        iniPos = this.gameObject.transform.localPosition;
        pos = this.gameObject.transform.localPosition;
        iniScreenPos = new Vector2(this.pos.x + MyPixelsOBJ.transform.position.x, this.pos.y + MyPixelsOBJ.transform.position.y);//粒子生成的位置是相对于父物体的位置生成的
        pixScreenPos = new Vector2(this.pos.x + MyPixelsOBJ.transform.position.x, this.pos.y + MyPixelsOBJ.transform.position.y);
        vel = new Vector3(0, 0, 0);
        acc = new Vector3(0, 0, 0);

        //获取功能模式控制变量
        CanvasOBJ = GameObject.FindWithTag("Canvas");    
        Control = CanvasOBJ.GetComponent<ReadPic>().Control;


        switch (Control)
        {
            case 1:
                DotFlowOBJ = GameObject.Find("DotFlow");
                maxspeed = 1;
                break;
            case 2:
                
                maxspeed = 10;
                break;
            case 3:


                break;
            case 4:
                
                //链接关节游戏对象
                GameObject connectedObjRow = null;
                GameObject connectedObjClo = null;
                //当前链接的弹簧关节组件
                SpringJoint jointComponentRow = null;
                SpringJoint jointComponentClo = null;

                // 给每个粒子添加刚体组件
                gameObject.AddComponent<Rigidbody>();
                // 锁定三个方向的旋转
                gameObject.GetComponent<Rigidbody>().constraints = 
                    RigidbodyConstraints.FreezeRotationZ |RigidbodyConstraints.FreezeRotationX |RigidbodyConstraints.FreezeRotationY;

                //物理效果，建立弹性网状结构
                if (Row == 0)
                {// 最下方一排，不进行连接

                }
                else if (Clo == 0)
                {// 最左侧一排，只与其下方一排连接
                    //Debug.Log("my row is"+Row+"and my clo is"+Clo);
                    jointComponentRow = gameObject.AddComponent<SpringJoint>();
                    jointComponentRow.maxDistance = 3;

                    //连接到下方一个连接粒子
                    connectedObjRow = CanvasOBJ.GetComponent<ReadPic>().pixArray[Row - 1, Clo];
                    jointComponentRow.connectedBody = connectedObjRow.GetComponent<Rigidbody>();
                }

                if (Row != 0 && Clo != 0)
                {// 一般粒子元，和其左方和下方的粒子连接
                    //Debug.Log("my row is" + Row + "and my clo is" + Clo);
                    jointComponentRow = gameObject.AddComponent<SpringJoint>();
                    jointComponentClo = gameObject.AddComponent<SpringJoint>();
                    jointComponentRow.maxDistance = 3;
                    jointComponentClo.maxDistance = 3;

                    //连接到下方一个连接粒子
                    connectedObjRow = CanvasOBJ.GetComponent<ReadPic>().pixArray[Row - 1, Clo];
                    jointComponentRow.connectedBody = connectedObjRow.GetComponent<Rigidbody>();


                    //连接到左方一个连接粒子
                    connectedObjClo = CanvasOBJ.GetComponent<ReadPic>().pixArray[Row, Clo -1];
                    jointComponentClo.connectedBody = connectedObjClo.GetComponent<Rigidbody>();
                }

                if (Row == CanvasOBJ.GetComponent<ReadPic>().rowNum-1)//最上方一行锁定
                                                                            
                {
                    //Debug.Log("my row is" + Row + "and my clo is" + Clo);
                    Debug.Log("lock" + Row + ":" + Clo);
                    gameObject.GetComponent<Rigidbody>().constraints = 
                        RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                }
                break;
            case 5:
                
                Debug.Log(CanvasOBJ.GetComponent<ReadPic>().rowNum);
                //链接关节游戏对象
                GameObject connectedObjRow2 = null;
                GameObject connectedObjClo2 = null;
                //当前链接的弹簧关节组件
                HingeJoint jointComponentRow2 = null;
                HingeJoint jointComponentClo2 = null;

                // 给每个粒子添加刚体组件
                // 测试刚体组件的属性效果
                gameObject.AddComponent<Rigidbody>();
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<Rigidbody>().drag = 0.1f;//    典型的Drag值介于0.001(固体金属)到10(羽毛)之间。
                //gameObject.GetComponent<Rigidbody>().isKinematic = true; // 如果启用该参数，则对象不会被物理所控制
                gameObject.GetComponent<Rigidbody>().mass = 0.1f;// 重量属性
                // 锁定三个方向的旋转
                gameObject.GetComponent<Rigidbody>().constraints =
                    RigidbodyConstraints.FreezeRotationX;//| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationZ;

                //物理效果，建立弹性网状结构
                if (Row == 0)
                {// 最下方一排，不进行连接


                }
                else if (Clo == 0)
                {// 最左侧一排，只与其下方一排连接
                    Debug.Log("my row is" + Row + "and my clo is" + Clo);
                    jointComponentRow2 = gameObject.AddComponent<HingeJoint>();
                    //gameObject.GetComponent<HingeJoint>().useSpring = true;
                    gameObject.GetComponent<HingeJoint>().useLimits = true;
                    //jointComponentRow2.maxDistance = 3;

                    //连接到上方一个连接粒子
                    connectedObjRow2 = CanvasOBJ.GetComponent<ReadPic>().pixArray[Row - 1, Clo];
                    jointComponentRow2.connectedBody = connectedObjRow2.GetComponent<Rigidbody>();
                }

                if (Row != 0 && Clo != 0)
                {// 一般粒子元，和其左方和下方的粒子连接
                    Debug.Log("my row is" + Row + "and my clo is" + Clo);
                    jointComponentRow2 = gameObject.AddComponent<HingeJoint>();
                    //gameObject.GetComponent<HingeJoint>().useSpring = true;
                    jointComponentClo2 = gameObject.AddComponent<HingeJoint>();
                    //jointComponentRow2.maxDistance = 3;
                    //jointComponentClo2.maxDistance = 3;

                    //连接到上方一个连接粒子
                    connectedObjRow2 = CanvasOBJ.GetComponent<ReadPic>().pixArray[Row - 1, Clo];
                    jointComponentRow2.connectedBody = connectedObjRow2.GetComponent<Rigidbody>();


                    //连接到左方一个连接粒子
                    connectedObjClo2 = CanvasOBJ.GetComponent<ReadPic>().pixArray[Row, Clo - 1];
                    jointComponentClo2.connectedBody = connectedObjClo2.GetComponent<Rigidbody>();
                }

                if (Row == CanvasOBJ.GetComponent<ReadPic>().rowNum - 1)//最上方一行锁定

                {
                    Debug.Log("my row is" + Row + "and my Clo is" + Clo);
                    Debug.Log("lock" + Row + ":" + Clo);
                    gameObject.GetComponent<Rigidbody>().constraints =
                        RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                }
                break;
            case 6:
                // 给每个粒子添加light组件
                // 测试组件的属性效果
                gameObject.AddComponent<Light>();
                gameObject.GetComponent<Light>().color = Col ;
                //gameObject.GetComponent<Light>().
                break;
            case 7:
                break;
            case 8:
                break;
                
            default:
                break;
        }
    }

    void Update()
    {
        //update pixScreenPos
        pixScreenPos.x = this.pos.x + MyPixelsOBJ.transform.position.x;
        pixScreenPos.y = this.pos.y + MyPixelsOBJ.transform.position.y;
        switch (Control)
        {
            case 1:
                this.Follow(DotFlowOBJ.GetComponent<DotFlowControl>().FlowField, DotFlowOBJ.GetComponent<DotFlowControl>().Cols);
                this.Edges(0, 0);

                this.vel += this.acc;
                this.vel = Vector2.ClampMagnitude(this.vel, this.maxspeed); //limit velocity because of the ever-exsiting acceleration
                this.pos += this.vel;
                this.acc *= 0;
                this.transform.localPosition = this.pos;
                break;
            case 2:

                if (Input.GetMouseButton(0))
                {
                    this.Attract();
                }
                else
                {
                    this.Arrive();
                }

                this.vel += this.acc;
                this.vel = Vector2.ClampMagnitude(this.vel, this.maxspeed);   
                this.pos += this.vel;
                this.acc *= 0;
                this.transform.localPosition = this.pos;
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
            default:
                break;
        }
    }

    //change the force of the pixels
    public void ApplyForce(Vector2 force)
    {
        this.acc += force;
    } 



    //FOR CASE1------------------------------------------------------------------------------------------------------------------------------
    void Follow(VectorField[] vectors, int Cols)
    {
        int xx = (int)Mathf.Floor((pixScreenPos.x) / scl);
        int yy = (int)Mathf.Floor((pixScreenPos.y) / scl);
        //find colum and row of the pixels
        int index = xx + yy * Cols;
        //find the pixel's index in field 
        Vector2 force = vectors[index].Direction;
        this.ApplyForce(force); //add the vector regarding as force
    }
    void Edges(int picwidth, int picheight)
    {
        int inter = 100;
        if (Vector2.Distance(iniScreenPos, pixScreenPos) > inter)
        {
            this.pos.x = iniScreenPos.x - MyPixelsOBJ.transform.position.x;
            this.pos.y = iniScreenPos.y - MyPixelsOBJ.transform.position.y;
        }
    }
    //CASE1 END----------------------------------------------------------------------------------------------------------------------------------





    //FOR CASE2-----------------------------------------------------------------------------------------------------------------------------------
    void Arrive()
    {
        float maxforce = 1.5f;
        Vector2 target = iniScreenPos;
        Vector2 desired = target - this.pixScreenPos;  // A vector pointing from the position to the target
        desired = Vector2.ClampMagnitude(desired, 10);
        Vector2 steer = desired - vel;
        steer = Vector2.ClampMagnitude(steer, maxforce);
        ApplyForce(steer);
    }
    void Attract()
    {

        float gmass=20;
        float G=1;
        float mass = 1;

        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 force = mousePos - this.pixScreenPos;//distance between the pixels and mousepostion(attractor)
        float d = force.magnitude;
        d = MyConstrain(d, 2, 6);

        force = force.normalized;
        //float strength = (G * mass * p.mass) / (d );//gravity
        //float strength = (G * mass * p.mass) * (d * d);
        float strength = (d * d) / (G * gmass * mass);
        force *= strength;
        ApplyForce(force);
    }
    float MyConstrain(float a, float min, float max)
    {
        if (a <= min) return min;
        else if (min < a && a < max) return a;
        else return max;
    }
    //CASE2 END----------------------------------------------------------------------------------------------------------------------------------



    
}
