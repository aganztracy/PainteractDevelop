using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public int Control = 1;
    Transform CanvasTF;
    public Canvas HomePageCanvas;
    public Canvas SetupPageCanvas;
    public Canvas ImagePageCanvas;
    public Canvas ChoosePageCanvas;
    public Canvas AdjustPageCanvas;

    // Start is called before the first frame update
    void Start () {

        CanvasTF = GameObject.FindWithTag ("Canvas").transform;

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

    }

    public void ShowHomePage () {

        HomePageCanvas.GetComponent<Canvas> ().enabled = true;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = false;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = false;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = false;
    }

    public void ShowImagePage () {
Debug.Log("showimagepage");
        HomePageCanvas.GetComponent<Canvas> ().enabled = false;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = true;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = false;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = false;
    }

    public void ShowChoosePage () {

        
        HomePageCanvas.GetComponent<Canvas> ().enabled = false;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = false;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = true;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = false;

    }

    public void ShowAdjustPage(){

        
        HomePageCanvas.GetComponent<Canvas> ().enabled = false;
        SetupPageCanvas.GetComponent<Canvas> ().enabled = false;
        ImagePageCanvas.GetComponent<Canvas> ().enabled = false;
        ChoosePageCanvas.GetComponent<Canvas> ().enabled = false;
        AdjustPageCanvas.GetComponent<Canvas> ().enabled = true;


    }

}