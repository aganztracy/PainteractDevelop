using System.Collections;
using System.Collections.Generic;
//using cn.sharerec;
using UnityEngine;

public class RECManager : MonoBehaviour {

    bool isRecording = false;

    GameObject CanvasOBJ;

    // Start is called before the first frame update
    void Start () {
        CanvasOBJ = GameObject.FindWithTag ("Canvas");

    }

    // Update is called once per frame
    void Update () {

    }

    // public void RecordingStart () {

    //     if (ShareREC.IsAvailable ()) {
    //         if (!isRecording) {

    //             OnStartRecord ();
    //         } else {
    //             OnStopRecord ();
    //         }
    //     }else
    //     {
    //         CanvasOBJ.GetComponent<UIManager>().ShowHomePage();
    //     }

    // }

    // public void OnStartRecord () {
    //         ShareREC.StartRecorder();
    //         isRecording = true;

    // }

    // public void OnStopRecord () {
    //         //停止监听事件
    //         ShareREC.OnRecorderStoppedHandler = OnShowShare;

    //         //停止录制
    //         ShareREC.StopRecorder();
    //         isRecording = false;
    // }

    // void OnShowShare(){

    //     ShareREC.AddCustomAttr("name","ShareREC Developer");

    //     ShareREC.AddCustomPlatform("CustomPlatform");
    //     ShareREC.ShowShare();

    // }

}