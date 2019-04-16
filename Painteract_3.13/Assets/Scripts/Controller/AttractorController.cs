using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class AttractorController : MonoBehaviour {
    float maxspeed = 10;
    Vector3 iniPos;
    Vector3 pos; //will update
    Vector3 vel;
    Vector3 acc;

    public Vector3 MyPixelsOBJPos;

    void Start () {
        MyPixelsOBJPos = GameObject.Find ("MyPixels").transform.position;
        iniPos = this.gameObject.transform.position;
        pos = this.gameObject.transform.position;
        vel = new Vector3 (0, 0, 0);
        acc = new Vector3 (0, 0, 0);
    }
    void Update () {

        this.vel += this.acc;
        this.vel = Vector3.ClampMagnitude (this.vel, this.maxspeed);
        this.pos += this.vel;
        this.acc *= 0;
        this.transform.position = this.pos;

        if (!IsTouchedUI ()) {
            // if (Input.GetMouseButton (0)) {
            if (Input.touchCount == 1) {
                this.Attract ();
            } else {
                this.Arrive ();
            }
        }

    }
    void ApplyForce (Vector3 force) {
        this.acc += force;
    }
    void Arrive () {
        float maxforce = 1.5f;
        Vector3 target = iniPos;
        Vector3 desired = target - this.pos; // A vector pointing from the position to the target
        desired = Vector3.ClampMagnitude (desired, 10);
        Vector3 steer = desired - vel;
        steer = Vector3.ClampMagnitude (steer, maxforce);
        // Debug.Log (steer);
        ApplyForce (steer);
    }

    void Attract () {

        float gmass = 20;
        float G = 1;
        float mass = 1;

        //可以计算空间中一个物体的位置和鼠标之间的向量差
        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint (transform.position); //从相机的视角将物体世界坐标转化为相对屏幕的三维坐标（只有z坐标是空间中的）
        Vector3 mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

        Vector3 force = mousePos - this.pos; //distance between the pixels and mousepostion(attractor)
        float d = force.magnitude;
        d = MyConstrain (d, 2, 6);

        force = force.normalized;
        //float strength = (G * mass * p.mass) / (d );//gravity
        //float strength = (G * mass * p.mass) * (d * d);
        float strength = (d * d) / (G * gmass * mass);
        force *= strength;
        ApplyForce (force);
    }
    float MyConstrain (float a, float min, float max) {
        if (a <= min) return min;
        else if (min < a && a < max) return a;
        else return max;
    }

    public bool IsTouchedUI () {
        bool touchedUI = false;
        if (UnityEngine.Application.isMobilePlatform) {
            if (EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId)) {
                touchedUI = true;
            }
        } else if (EventSystem.current.IsPointerOverGameObject ()) {
            touchedUI = true;
        }
        return touchedUI;
        // Debug.Log (touchedUI);
    }
}