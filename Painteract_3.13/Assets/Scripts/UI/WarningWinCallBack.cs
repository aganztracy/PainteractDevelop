using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningWinCallBack : MonoBehaviour {

    public GameObject CanvasOBJ;
    public GameObject MyPixelsOBJ;

    public string CurrentPage;//区分当前请求返回的界面，以确定返回效果

    // Start is called before the first frame update
    void Start () {
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");

        this.gameObject.transform.Find ("YesButton").GetComponent<Button> ().onClick.AddListener (onClickYes);
        this.gameObject.transform.Find ("NoButton").GetComponent<Button> ().onClick.AddListener (onClickNo);
    }

    void onClickYes () {

        if (CurrentPage == "ImagePage") {

            CanvasOBJ.GetComponent<ReadPic> ().Cancel ();
            CanvasOBJ.GetComponent<UIManager> ().ShowHomePage ();

        }

        if (CurrentPage == "ChoosePage") {

            CanvasOBJ.GetComponent<ReadPic> ().Cancel ();
            CanvasOBJ.GetComponent<UIManager> ().ShowHomePage ();
            MyPixelsOBJ.GetComponent<DestroyAllChildren>().DestroyChildren();


        }
        if (CurrentPage == "AdjustPage") {

            CanvasOBJ.GetComponent<ReadPic> ().Cancel ();
            CanvasOBJ.GetComponent<UIManager> ().ShowChoosePage ();


        }

        if(CurrentPage == "SavePage"){
             CanvasOBJ.GetComponent<UnityREC> ().onSaveVideo ();
        }
        
        Destroy (gameObject);

    }

    void onClickNo () {

        Destroy (gameObject);
    }

}