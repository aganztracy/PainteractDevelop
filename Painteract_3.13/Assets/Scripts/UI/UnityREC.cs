using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityREC : MonoBehaviour {

    bool isRecording = false;

    GameObject CanvasOBJ;
    Transform CanvasTF;

    public GameObject SaveRequireWinPrefab;
    // Start is called before the first frame update
    void Start () {
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        CanvasTF = GameObject.FindWithTag ("Canvas").transform;
        SaveRequireWinPrefab = Resources.Load<GameObject> ("Prefabs/UIPrefabs/SaveRequireWindow");
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
        ShowSaveRequireWin ();
        isRecording = false;
#elif UNITY_IPHONE

#endif

    }

    public void ShowSaveRequireWin () {

        GameObject SaveRequireWindow;
        SaveRequireWindow = Instantiate (SaveRequireWinPrefab, CanvasTF);
        SaveRequireWindow.GetComponent<WarningWinCallBack> ().CurrentPage = "SavePage";
    }

    public void onSaveVideo () {
#if UNITY_ANDROID

        AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject> ("currentActivity");
        jo.Call ("saveRecordVideo");

#elif UNITY_IPHONE

#endif

    }

}