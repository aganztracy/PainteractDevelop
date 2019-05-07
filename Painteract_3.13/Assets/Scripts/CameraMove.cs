using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    // 主角  相机围绕其旋转 拉近  
    public Transform RotateTarget;

    // 缩放系数  
    private float distance = 0.0f;
    private float scaleV = 140.0f;

    // 左右滑动移动速度  
    private float xSpeed = 20.0f;
    private float ySpeed = 20.0f;

    // 旋转限制系数  y轴上可旋转的最小值和最大值 
    private float yMinLimit = -90;
    private float yMaxLimit = 90;

    // 缩放限制系数 是根据相机的视野来调整的 可缩放倍数?不明白,不太像倍数 
    private float distanceMax = 9000f;
    private float distanceMin = 200f;

    // 摄像头的位置  
    private float x = 0.0f;
    private float y = 0.0f;

    // 记录上一次手机触摸位置判断用户是在做放大还是缩小手势  
    private Vector2 oldPosition1 = new Vector2 (0, 0);
    private Vector2 oldPosition2 = new Vector2 (0, 0);

    //for PC debug
    private float rotateSpeed = 10.0f;
    private float zoomFactor = 150.0f;

    //记录默认初始位置，为了重置
    public Vector3 defaultPosition ;
    public Quaternion defaultRotation = Quaternion.Euler (0, 0, 0);

    //初始化游戏信息设置  
    void Start () {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        distance = -transform.position.z;
        //  GetComponent<Rigidbody>().freezeRotation = true;

        defaultPosition = new Vector3(angles.y,angles.x,distance);
        defaultRotation = this.transform.rotation;

        //for PC debug
        camerarotate ();
        camerazoom ();
    }

    void Update () {
        // 判断触摸数量为单点触摸  
        if (Input.touchCount == 2) {
            float myDot = Vector3.Dot (Input.GetTouch (0).deltaPosition, Input.GetTouch (1).deltaPosition);
            if (myDot > 0) { //两手指同向旋转
                if (Input.GetTouch (0).phase == TouchPhase.Moved) {
                    //根据触摸点计算X与Y位置  
                    x += Input.GetTouch (0).deltaPosition.x * xSpeed * 0.02f;
                    y -= Input.GetTouch (0).deltaPosition.y * ySpeed * 0.02f;
                    // Debug.Log (Input.GetTouch (0).deltaPosition.x);//手指水平移动的距离
                }
            } else { //两手指异向缩放
                if (Input.GetTouch (0).phase == TouchPhase.Moved || Input.GetTouch (1).phase == TouchPhase.Moved) {
                    // 计算出当前两点触摸点的位置  
                    var tempPosition1 = Input.GetTouch (0).position;
                    var tempPosition2 = Input.GetTouch (1).position;
                    // 函数返回真为放大，返回假为缩小  
                    if (isEnlarge (oldPosition1, oldPosition2, tempPosition1, tempPosition2)) {
                        // 放大系数超过distanceMin以后不允许继续放大  
                        if (distance > distanceMin) {
                            distance -= scaleV;
                        }
                    } else {
                        // 缩小系数返回distanceMax后不允许继续缩小  
                        if (distance < distanceMax) {
                            distance += scaleV;
                        }
                    }
                    // 备份上一次触摸点的位置，用于对比  
                    oldPosition1 = tempPosition1;
                    oldPosition2 = tempPosition2;

                }
            }
            // 触摸类型为移动触摸  

        }
    }

    // 函数返回真为放大，返回假为缩小  
    bool isEnlarge (Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2) {
        // 函数传入上一次触摸两点的位置与本次触摸两点的位置计算出用户的手势  
        float leng1 = Mathf.Sqrt ((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        float leng2 = Mathf.Sqrt ((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));

        if (leng1 < leng2) {
            // 放大手势  
            return true;
        } else {
            // 缩小手势  
            return false;
        }
    }
    //。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。  
    // Update方法一旦调用结束以后进入这里算出重置摄像机的位置  
    void LateUpdate () {
        // RotateTarget为主角，缩放旋转的参照物  
        if (RotateTarget) {
            // 重置摄像机的位置  
            y = ClampAngle (y, yMinLimit, yMaxLimit);
            Quaternion rotation = Quaternion.Euler (y, x, 0);
            Vector3 position = rotation * new Vector3 (0.0f, 0.0f, -distance) + RotateTarget.position;

            transform.rotation = rotation;
            transform.position = position;
            // Debug.Log (position);
        }
    }

    static float ClampAngle (float angle, float min, float max) {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp (angle, min, max);

    }

    // public Transform RotateTarget; //获取旋转目标
    // // private float rotateSpeed = 10.0f;
    // private float zoomFactor=150.0f;
    // void Update () {
    //     camerarotate ();
    //     camerazoom ();
    // }
    private void camerarotate () //摄像机围绕目标旋转操作
    {
        // transform.RotateAround (RotateTarget.position, Vector3.up, rotateSpeed * Time.deltaTime); //摄像机围绕目标按一定速度旋转
        var mouse_x = Input.GetAxis ("Mouse X"); //获取鼠标X轴移动
        var mouse_y = -Input.GetAxis ("Mouse Y"); //获取鼠标Y轴移动
        //鼠标左键平移，有冲突，关闭掉
        // if (Input.GetKey (KeyCode.Mouse0)) {
        //     transform.Translate (Vector3.left * (mouse_x * 15f*zoomFactor) * Time.deltaTime);
        //     transform.Translate (Vector3.up * (mouse_y * 15f*zoomFactor) * Time.deltaTime);
        // }
        if (Input.GetKey (KeyCode.Mouse1)) {
            transform.RotateAround (RotateTarget.transform.position, Vector3.up, mouse_x * 5);
            transform.RotateAround (RotateTarget.transform.position, transform.right, mouse_y * 5);
        }
    }

    private void camerazoom () //摄像机滚轮缩放
    {
        if (Input.GetAxis ("Mouse ScrollWheel") > 0)
            transform.Translate (Vector3.forward * 0.5f * zoomFactor);

        if (Input.GetAxis ("Mouse ScrollWheel") < 0)
            transform.Translate (Vector3.forward * -0.5f * zoomFactor);
    }

    public void cameraReset () {

        // this.transform.rotation = defaultRotation;
        // this.transform.position = defaultPosition;

        // transform.Translate(defaultPosition);
        //transform.SetPositionAndRotation(defaultPosition,defaultRotation);//无法修改transform的信息
        x = defaultPosition.x;
        y = defaultPosition.y;
        distance = defaultPosition.z;
        


    }

}