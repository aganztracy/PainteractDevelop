using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour {
    Gyroscope myGyro;
    bool gyroEnabled;
    float sensitivity = 80.0f;

    void Start () {
        gyroEnabled = EnableGyro ();
        Debug.Log ("gyroEnabled:" + gyroEnabled);
        // gyInfo = SystemInfo.supportsGyroscope;

    }
    private bool EnableGyro () {
        if (SystemInfo.supportsGyroscope) {
            myGyro = Input.gyro;
            myGyro.enabled = true;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update () {
        if (gyroEnabled) { //如果设备支持陀螺仪
            //晃动速率 或许有用
            // this.gameObject.GetComponent<ParticleSystemForceField> ().directionX = myGyro.rotationRate.x * 50f;
            // this.gameObject.GetComponent<ParticleSystemForceField> ().directionY = myGyro.rotationRate.y * 50f;
            // this.gameObject.GetComponent<ParticleSystemForceField> ().directionZ = myGyro.rotationRate.z * 50f;

            //要限制影响值
            Vector3 dir = Vector3.zero;

            dir.x = Mathf.Clamp (Input.acceleration.x * sensitivity, -80, 80);
            dir.y = Mathf.Clamp (Input.acceleration.y * sensitivity, -80, 80);
            dir.z = Mathf.Clamp (Input.acceleration.z * sensitivity, -10, 10);

            // if (dir.sqrMagnitude > 1) {
            //     dir.Normalize (); // clamp acceleration vector to unit sphere
            // }

            this.gameObject.GetComponent<ParticleSystemForceField> ().directionX = dir.x;
            this.gameObject.GetComponent<ParticleSystemForceField> ().directionY = dir.y;
            this.gameObject.GetComponent<ParticleSystemForceField> ().directionZ = dir.z;

            //得到的是手机在空间中旋转的欧拉角 这里没什么用的
            // this.gameObject.GetComponent<ParticleSystemForceField>().directionX=myGyro.attitude.eulerAngles.x* 0.05f;
            // this.gameObject.GetComponent<ParticleSystemForceField>().directionY=myGyro.attitude.eulerAngles.y* 0.05f;
            // this.gameObject.GetComponent<ParticleSystemForceField>().directionZ=myGyro.attitude.eulerAngles.z* 0.05f;

        }
    }
    // //for debug
    // void OnGUI () {
    //     if (gyroEnabled) {
    //         GUI.skin.label.fontSize = Screen.width / 20;
    //         //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
    //         GUI.Label (new Rect (200, 300, 900, 90), "Gyro rotation rate " + myGyro.rotationRate);
    //         GUI.Label (new Rect (200, 450, 900, 90), "Gyro attitude" + myGyro.attitude);
    //         GUI.Label (new Rect (50, 550, 900, 200), "Gyro attitude eulerAngles" + myGyro.attitude.eulerAngles);
    //         GUI.Label (new Rect (200, 800, 900, 90), "Input.acceleration: " + Input.acceleration);
    //     }
    // }
}