using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {


    GameObject _mainCamera;



    // Use this for initialization
    void Start()
    {
   
        _mainCamera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //沿着Y轴旋转，也就是左右旋转
            _mainCamera.transform.Rotate(0, -25 * Time.deltaTime, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _mainCamera.transform.Rotate(0, 25 * Time.deltaTime, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //沿着X轴旋转
            _mainCamera.transform.Rotate(-25 * Time.deltaTime, 0, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _mainCamera.transform.Rotate(25 * Time.deltaTime, 0, 0, Space.Self);
        }

    }



}
