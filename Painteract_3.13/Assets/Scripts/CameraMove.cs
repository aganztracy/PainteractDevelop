using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public Transform RotateTarget; //获取旋转目标
    // private float rotateSpeed = 10.0f;
    private float zoomFactor=150.0f;
    void Update () {
        camerarotate ();
        camerazoom ();
    }

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
            transform.Translate (Vector3.forward * 0.5f*zoomFactor);

        if (Input.GetAxis ("Mouse ScrollWheel") < 0)
            transform.Translate (Vector3.forward * -0.5f*zoomFactor);
    }

}