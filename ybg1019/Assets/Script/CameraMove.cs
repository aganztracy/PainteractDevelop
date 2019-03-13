using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {


    GameObject MainCamera;



    // Use this for initialization
    void Start()
    {
   
        MainCamera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //沿着Y轴旋转，也就是左右旋转
            MainCamera.transform.Rotate(0, -25 * Time.deltaTime, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            MainCamera.transform.Rotate(0, 25 * Time.deltaTime, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //沿着X轴旋转
            MainCamera.transform.Rotate(-25 * Time.deltaTime, 0, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MainCamera.transform.Rotate(25 * Time.deltaTime, 0, 0, Space.Self);
        }

    }



}
