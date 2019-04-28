using System.Collections;
using System.Collections.Generic;
using cn.sharerec;
using UnityEngine;

public class RECManager : MonoBehaviour {

    bool isRecording = false;

    GameObject CanvasOBJ;

    // Start is called before the first frame update
    void Start () {
        CanvasOBJ = GameObject.FindWithTag ("Canvas");

        //如果ShareREC.cs并非挂载在Main Camera对象或者Main Camera对象修改为其他名字时，
        //需要调用ShareREC. setCallbackObjectName设置回调对象名称。否则会导致需要回调的接口无法正常回调
        //ShareREC.setCallbackObjectName (“CallbackObjectName”);
        

    }

    // Update is called once per frame
    void Update () {

    }

    public void RecordingStart () {

        if (ShareREC.IsAvailable ()) {
            if (!isRecording) {

                OnStartRecord ();
            } else {
                OnStopRecord ();
            }
        } else {
            CanvasOBJ.GetComponent<UIManager> ().ShowHomePage ();
        }

    }

    public void OnStartRecord () {
        ShareREC.StartRecorder ();
        isRecording = true;

    }

    public void OnStopRecord () {
        //停止监听事件
        ShareREC.OnRecorderStoppedHandler = OnShowShare;

        //停止录制
        ShareREC.StopRecorder ();
        isRecording = false;
    }

    void OnShowShare () {

        ShareREC.AddCustomAttr ("name", "ShareREC Developer");

        ShareREC.AddCustomPlatform ("CustomPlatform");
        ShareREC.ShowShare ();

    }

}