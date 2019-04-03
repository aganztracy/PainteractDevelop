using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    Transform CanvasTF;
    public Canvas HomePageCanvas;
    public Canvas SetupPageCanvas;
    public Canvas ImagePageCanvas;
    public Canvas ChoosePageCanvas;
    public Canvas AdjustPageCanvas;
    public Canvas ScrollViewCanvas;

    public GameObject FunctionScrollView;
    public GameObject ScrollGrid;
    public GameObject FunctionButtonPrefab;
    public bool isScrollOpen;

    // Start is called before the first frame update
    void Start () {

        CanvasTF = GameObject.FindWithTag ("Canvas").transform;
        FunctionButtonPrefab = Resources.Load<GameObject> ("Prefabs/UIPrefabs/FunctionButton");

    }

    // Update is called once per frame
    void Update () {

    }

    public void ShowSetupPage () {

        HomePageCanvas.GetComponent<Canvas> ().enabled = false;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = true;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = false;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = false;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;

    }

    public void ShowHomePage () {

        HomePageCanvas.GetComponent<Canvas> ().enabled = true;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = false;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = false;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
    }

    public void ShowImagePage () {
        Debug.Log ("showimagepage");
        HomePageCanvas.GetComponent<Canvas> ().enabled = false;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = true;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = false;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
    }

    public void ShowChoosePage () {

        HomePageCanvas.GetComponent<Canvas> ().enabled = false;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = false;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = true;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;

    }

    public void ShowAdjustPage () {

        HomePageCanvas.GetComponent<Canvas> ().enabled = false;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = false;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = false;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = true;
        ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;

    }

    public void FlowFunctionScroll () {

        ScrollGrid.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        if (isScrollOpen) {
            ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
            ScrollDown ();
            isScrollOpen = false;
            Debug.Log ("CloseScrollView");
        }else{
            ShowFunctionScrollView ("Flow");
            Debug.Log ("OpenScrollView");
        }

    }

    public void MusicFunctionScroll () {
        ScrollGrid.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        if (isScrollOpen) {
            ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
            ScrollDown ();
            isScrollOpen = false;
        }else{
            ShowFunctionScrollView ("Music");
        }

    }

    public void PhysicFunctionScroll () {
        ScrollGrid.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        if (isScrollOpen) {
            ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
            ScrollDown ();
            isScrollOpen = false;
        }else{
            ShowFunctionScrollView ("Physic");
        }

    }

    public void OtherFunctionScroll () {
        ScrollGrid.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        if (isScrollOpen) {
            ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
            ScrollDown ();
            isScrollOpen = false;
        }else{
            ShowFunctionScrollView ("Other");
        }

    }

    public void ShowFunctionScrollView (string kindIndex) {

        ScrollViewCanvas.GetComponent<Canvas> ().enabled = true;
        isScrollOpen = true;

        ScrollUp ();

        if (kindIndex == "Flow") {  //效果1——8
            for (int i = 1; i < 9; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.AddComponent<ButtonCallBack> ();
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }
        }

        if (kindIndex == "Music") {  //效果9

            for (int i = 9; i < 10; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.AddComponent<ButtonCallBack> ();
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }

        }

        if (kindIndex == "Physic") {  //效果10-13
            for (int i = 10; i < 14; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.AddComponent<ButtonCallBack> ();
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }

        }

        if (kindIndex == "Other") {  //效果14-16
            for (int i = 14; i < 17; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.AddComponent<ButtonCallBack> ();
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }

        }

    }

    public void ScrollUp () {
        GameObject.FindGameObjectWithTag ("FirstButtonPanel").GetComponent<RectTransform> ().sizeDelta = new Vector2 (1000, 380); //属性栏panel拓宽为两层
    }

    public void ScrollDown () {
        GameObject.FindGameObjectWithTag ("FirstButtonPanel").GetComponent<RectTransform> ().sizeDelta = new Vector2 (1000, 200); 
    }

}