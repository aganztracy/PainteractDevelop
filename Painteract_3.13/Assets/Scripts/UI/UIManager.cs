using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    Transform CanvasTF;
    int Control;

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
    public string CurrentScrollKind;

    public GameObject WarningWinPrefab;

    public GameObject PorpertyBarPrefab;
    GameObject tempPorpertyBar;
    public bool isPropertyOpen = false;

    // Start is called before the first frame update
    void Start () {

        CanvasTF = GameObject.FindWithTag ("Canvas").transform;
        Control = CanvasTF.GetComponent<ReadPic> ().Control;
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
        PropertyBarClose ();

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
    /// 
    /// 功能选择第一级栏目的显示和隐藏
    /// </summary>
    /// 

    public void ShowClassifyBar () {

        GameObject.FindGameObjectWithTag ("ChoosePageCanvas").transform.GetChild (1).gameObject.SetActive (true);
        GameObject.FindGameObjectWithTag ("ChoosePageCanvas").transform.GetChild (0).gameObject.SetActive (false);

    }

    public void CloseClassifyBar () {
        ScrollDown ();
        GameObject.FindGameObjectWithTag ("ChoosePageCanvas").transform.GetChild (1).gameObject.SetActive (false);
        GameObject.FindGameObjectWithTag ("ChoosePageCanvas").transform.GetChild (0).gameObject.SetActive (true);

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
        if (isScrollOpen && CurrentScrollKind == "Flow") {
            ScrollDown ();
            //Debug.Log ("CloseScrollView");
        } else {
            ShowFunctionScrollView ("Flow");
            //Debug.Log ("OpenScrollView");
        }

    }

    public void MusicFunctionScroll () {
        ScrollGrid.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        if (isScrollOpen && CurrentScrollKind == "Music") {
            ScrollDown ();
        } else {
            ShowFunctionScrollView ("Music");
        }

    }

    public void PhysicFunctionScroll () {
        ScrollGrid.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        if (isScrollOpen && CurrentScrollKind == "Physic") {
            ScrollDown ();
        } else {
            ShowFunctionScrollView ("Physic");
        }

    }

    public void OtherFunctionScroll () {
        ScrollGrid.GetComponent<DestroyAllChildren> ().DestroyChildren ();
        if (isScrollOpen && CurrentScrollKind == "Other") {
            ScrollDown ();
        } else {
            ShowFunctionScrollView ("Other");
        }

    }

    private void ShowFunctionScrollView (string kindIndex) {

        ScrollViewCanvas.GetComponent<Canvas> ().enabled = true;
        CurrentScrollKind = kindIndex;

        ScrollUp ();

        if (kindIndex == "Flow") { //效果1——8
            for (int i = 1; i < 9; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }
        }

        if (kindIndex == "Music") { //效果9

            for (int i = 9; i < 10; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }

        }

        if (kindIndex == "Physic") { //效果10-13
            for (int i = 10; i < 14; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }

        }

        if (kindIndex == "Other") { //效果14-16
            for (int i = 14; i < 17; i++) {

                GameObject tempButton;
                tempButton = Instantiate (FunctionButtonPrefab, ScrollGrid.transform);
                tempButton.GetComponent<ButtonCallBack> ().Control = i;
            }

        }

    }

    private void ScrollUp () {

        if (GameObject.FindGameObjectWithTag ("FirstButtonPanel")) {
            var FirstButtonPanel = GameObject.FindGameObjectWithTag ("FirstButtonPanel");
            FirstButtonPanel.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1000, 380); //属性栏panel拓宽为两层
        }
        ResetScrollGrid ();//切换功能时对滚动栏目位置进行重置
        isScrollOpen = true;

    }

    private void ScrollDown () {
        ScrollViewCanvas.GetComponent<Canvas> ().enabled = false;
        if (GameObject.FindGameObjectWithTag ("FirstButtonPanel")) {
            var FirstButtonPanel = GameObject.FindGameObjectWithTag ("FirstButtonPanel");
            FirstButtonPanel.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1000, 200);
        }
        isScrollOpen = false;
    }

    public void ResetScrollGrid () {

        if (GameObject.FindGameObjectWithTag ("ScrollGrid")) {
            var ScrollGrid = GameObject.FindGameObjectWithTag ("ScrollGrid");
            ScrollGrid.GetComponent<RectTransform> ().localPosition = new Vector3 (400, 0,0);

        }
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
        tempWarningWindow.GetComponent<WarningWinCallBack> ().CurrentPage = pageName;
    }

    public void PropertyBarOpen () {

        var PropertyBarUpButton = GameObject.FindGameObjectWithTag ("AdjustPageCanvas").transform.GetChild (1);
        PropertyBarUpButton.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("UI/PropertyDown");

        Control = CanvasTF.GetComponent<ReadPic> ().Control;

        if (isPropertyOpen) {

            PropertyBarClose ();

        } else {
            switch (Control) {

                /// <summary>
                /// Flow 类别效果
                /// 1-8
                /// </summary>
                case 1:
                    break;
                case 2:
                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:
                    break;
                case 6:

                    break;
                case 7:

                    break;
                case 8:

                    break;

                    /// <summary>
                    /// Music 类别效果
                    /// 9
                    /// </summary>

                case 9: //=========================================================音乐可视化

                    break;

                    /// <summary>
                    /// Physic 类别效果
                    /// 10-13
                    /// </summary>
                case 10: //=========================================================可拖拽弹簧网格

                    break;
                case 11: //=========================================================三维粒子效果

                    break;
                case 12: //=========================================================粒子饮料效果

                    break;
                case 13: //=========================================================WobblyGrid 

                    PorpertyBarPrefab = Resources.Load<GameObject> ("Prefabs/UIPrefabs/PropertyBars/PorpertyBar_1");
                    tempPorpertyBar = Instantiate (PorpertyBarPrefab, CanvasTF);
                    isPropertyOpen = true;

                    // Debug.Log("PorpertyBarPrefab instantiate");

                    break;

                    /// <summary>
                    /// Other 类别效果
                    /// 14-16
                    /// </summary>
                case 14: //=========================================================3D Noise Flow Field

                    break;

                case 15: //=========================================================爆炸粒子系统效果
                    break;
                case 16: //=========================================================颜料溅开效果
                    break;

                default:
                    break;

            }

        }

    }

    public void PropertyBarClose () {

        if (tempPorpertyBar) {
            Destroy (tempPorpertyBar);
        }
        isPropertyOpen = false;

        var PropertyBarUpButton = GameObject.FindGameObjectWithTag ("AdjustPageCanvas").transform.GetChild (1);
        PropertyBarUpButton.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("UI/PropertyUp");

    }

}