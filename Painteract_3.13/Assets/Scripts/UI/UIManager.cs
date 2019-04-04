using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    Transform CanvasTF;

    //所有界面  目前需要在
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

    public GameObject WarningWinPrefab;

    // Start is called before the first frame update
    void Start () {

        CanvasTF = GameObject.FindWithTag ("Canvas").transform;
        FunctionButtonPrefab = Resources.Load<GameObject> ("Prefabs/UIPrefabs/FunctionButton");
        WarningWinPrefab = Resources.Load<GameObject> ("Prefabs/UIPrefabs/WarningWindow");
    }

    // Update is called once per frame
    void Update () {

    }

    /// <summary>
    /// Show***Page () 函数为界面显示和其他界面影藏函数
    /// 界面跳转（显隐）通过禁用界面上的画布组件实现，避免了画布的重载
    /// </summary>

    public void ShowSetupPage () {

        HomePageCanvas.GetComponent<Canvas> ().enabled = false;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = true;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = false;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = false;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollDown ();

    }

    public void ShowHomePage () {

        HomePageCanvas.GetComponent<Canvas> ().enabled = true;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = false;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = false;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollDown ();
    }

    public void ShowImagePage () {
        Debug.Log ("showimagepage");
        HomePageCanvas.GetComponent<Canvas> ().enabled = false;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = true;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = false;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollDown ();
    }

    public void ShowChoosePage () {

        HomePageCanvas.GetComponent<Canvas> ().enabled = false;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = false;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = true;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollDown ();

    }

    public void ShowAdjustPage () {

        HomePageCanvas.GetComponent<Canvas> ().enabled = false;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = false;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = false;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = true;
        ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
        ScrollDown ();

    }

    /// <summary>
    /// ***FunctionScroll () 函数为某一类别（第一层级效果选择）按钮触发后子效果选择ScrollView弹出控制函数
    /// 功能包括：
    /// 生成和销毁对应的子按钮组
    /// 按下按钮弹出，再次按下隐藏
    /// 关联脚本  ButtonCallBack.cs  实现子效果按钮的响应（实现何种功能、按钮贴图）
    /// </summary>

    public void FlowFunctionScroll () {

        ScrollGrid.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        if (isScrollOpen) {
            ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
            ScrollDown ();
            isScrollOpen = false;
            //Debug.Log ("CloseScrollView");
        } else {
            ShowFunctionScrollView ("Flow");
            //Debug.Log ("OpenScrollView");
        }

    }

    public void MusicFunctionScroll () {
        ScrollGrid.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        if (isScrollOpen) {
            ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
            ScrollDown ();
            isScrollOpen = false;
        } else {
            ShowFunctionScrollView ("Music");
        }

    }

    public void PhysicFunctionScroll () {
        ScrollGrid.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        if (isScrollOpen) {
            ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
            ScrollDown ();
            isScrollOpen = false;
        } else {
            ShowFunctionScrollView ("Physic");
        }

    }

    public void OtherFunctionScroll () {
        ScrollGrid.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        if (isScrollOpen) {
            ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
            ScrollDown ();
            isScrollOpen = false;
        } else {
            ShowFunctionScrollView ("Other");
        }

    }

    private void ShowFunctionScrollView (string kindIndex) {

        ScrollViewCanvas.GetComponent<Canvas> ().enabled = true;
        isScrollOpen = true;

        ScrollUp ();

        if (kindIndex == "Flow") { //效果1——8
            for (int i = 1; i < 9; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.AddComponent<ButtonCallBack> ();
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }
        }

        if (kindIndex == "Music") { //效果9

            for (int i = 9; i < 10; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.AddComponent<ButtonCallBack> ();
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }

        }

        if (kindIndex == "Physic") { //效果10-13
            for (int i = 10; i < 14; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.AddComponent<ButtonCallBack> ();
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }

        }

        if (kindIndex == "Other") { //效果14-16
            for (int i = 14; i < 17; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.AddComponent<ButtonCallBack> ();
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }

        }

    }

    private void ScrollUp () {
        GameObject.FindGameObjectWithTag ("FirstButtonPanel").GetComponent<RectTransform> ().sizeDelta = new Vector2 (1000, 380); //属性栏panel拓宽为两层
    }

    private void ScrollDown () {
        GameObject.FindGameObjectWithTag ("FirstButtonPanel").GetComponent<RectTransform> ().sizeDelta = new Vector2 (1000, 200);
    }

    /// <summary>
    /// ***PageBackWarning () 函数为返回时警告弹窗弹出控制函数，以及当前界面返回逻辑实现（返回到哪一界面，场景发生什么样的重置）
    /// 关联脚本  WarningWinCallBack.cs  实现按钮响应
    /// </summary>

    public void ImagePageBackWarning () {
        WarningPopUp ("ImagePage");
    }

    public void ChoosePageBackWarning () {
        WarningPopUp ("ChoosePage");

    }
    public void AdjustPageBackWarning () {
        WarningPopUp ("AdjustPage");

    }

    private void WarningPopUp (string pageName) {

        GameObject tempWarningWindow;
        tempWarningWindow = Instantiate (WarningWinPrefab, CanvasTF);
        tempWarningWindow.AddComponent<WarningWinCallBack> ();
        tempWarningWindow.GetComponent<WarningWinCallBack> ().CurrentPage = pageName;
    }



}