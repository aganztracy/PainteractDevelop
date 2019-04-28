using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityREC : MonoBehaviour {

    bool isRecording = false;

    GameObject CanvasOBJ;
    // Start is called before the first frame update
    void Start () {
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
    }

     public void RecordingStart () {

      
            if (!isRecording) {

                onStartRecording ();
            } else {
                onStopRecording ();
            }


    }


    public void onStartRecording () {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject> ("currentActivity");
        jo.Call ("startRecording");
        isRecording = true;
#elif UNITY_IPHONE

#endif

    }

    public void onStopRecording () {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject> ("currentActivity");
        jo.Call ("stopRecordin");
        isRecording = false;
#elif UNITY_IPHONE

#endif

    }

}