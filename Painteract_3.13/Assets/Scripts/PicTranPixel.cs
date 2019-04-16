using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicTranPixel : MonoBehaviour {
    GameObject CanvasOBJ;
    // GameObject MyPixelsOBJ;

    AttractParticle myPixel;
    List<AttractParticle> Pic1Atoms;
    List<AttractParticle> Pic2AtomsTemp;
    void Start () {
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        // MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
        Pic1Atoms = CanvasOBJ.GetComponent<PicTransController> ().Pic1Atoms;
        Pic2AtomsTemp = CanvasOBJ.GetComponent<PicTransController> ().Pic2AtomsTemp;
        myPixel = new AttractParticle (this.transform.position, this.gameObject.GetComponent<MyPixel> ().Col, this.gameObject, 0); //被吸引方orinIndex默认为0
        Pic1Atoms.Add (myPixel); 
    }
    void Update () {

        if(Pic2AtomsTemp.Count==0) return;//如果图2没导入

        myPixel.update ();
        this.transform.position = myPixel.pos;
        // this.transform.localPosition = myPixel.pos;
        this.gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", myPixel.pixelColor);

        ///
        /// 这个好像没什么用
        /// 
        // if (Input.touchCount == 1) {
        //     myPixel.arrive ();
        // }

        ////可以加一个toggle 让用户自己选择紊乱还是不紊乱
        if (Input.touchCount == 1) {
            foreach (AttractParticle particle in Pic1Atoms) {
                Vector3 force = Pic2AtomsTemp[particle.attractindex].attract (particle); //吸引
                particle.applyForce (force);
                particle.arrive ();
            }
        }
    }
}

public class AttractParticle {
    public Vector3 pos; //位置
    public Vector3 vel; //速度
    public Vector3 acc; //加速度
    public Vector3 target; //目标位置
    public Color pixelColor; //颜色
    public Color targetColor;
    public float maxVel; //最大速度
    public float maxforce;

    public bool chosed = false;
    public int attractindex = 0;
    public int orinIndex = 0;
    public GameObject myOBJ;

    public AttractParticle (Vector3 pixelPos, Color inputColor, GameObject OBJ, int index) {
        this.myOBJ = OBJ;
        pos = pixelPos;
        vel = Vector3.zero;
        acc = Vector3.zero;
        pixelColor = inputColor;
        maxVel = 20f;
        maxforce = 1.5f;
        orinIndex = index;
    }

    public void applyForce (Vector3 force) {
        acc += force;
    }

    public void arrive () { //算出目标点与当前点距离，每次最大100pixels；算出需要的加速度，限定在最大加速度之内，然后apply这个力
        Vector3 desired = target - pos; // A vector pointing from the position to the target
        float d = desired.magnitude;
        // Scale with arbitrary damping within 100 pixels
        if (d < 100) {
            float m = ExtensionMethods.Map (d, 0, 100, 0, maxVel);
            // desired.setMag (maxVel); //怎么改？归一化再乘长度？
            desired = desired.normalized * m;
            // Vector3.ClampMagnitude (desired, m);
        } else {
            desired = desired.normalized * maxVel;
            // Vector3.ClampMagnitude (desired, maxVel);
        }

        // Steering = Desired minus Velocity
        Vector3 steer = desired - vel;
        steer = Vector3.ClampMagnitude (steer, maxforce); // Limit to maximum steering force
        applyForce (steer);
        pixelColor = Color.Lerp (pixelColor, targetColor, .1f); //颜色 差值  
    }

    public void update () {
        vel += acc;
        vel = Vector3.ClampMagnitude (vel, maxVel);
        pos += vel;
        acc = Vector3.zero;
    }


    public Vector3 attract (AttractParticle p) {
        Vector3 force = pos - p.pos; //从粒子到吸引器还有多长距离
        float d = force.magnitude; //向量大小a
        d = Mathf.Clamp (d, 2, 6);
        force.Normalize (); //归一z
        float strength = d; //距离越远 加速度越大 越近 加速度越小
        force *= strength;
        return force;
    }
}