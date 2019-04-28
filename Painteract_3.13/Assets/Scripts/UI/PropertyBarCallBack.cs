using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyBarCallBack : MonoBehaviour {

    public int Control;
    public GameObject CanvasOBJ;

    // Start is called before the first frame update
    void Start () {
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        Control = CanvasOBJ.transform.GetComponent<ReadPic> ().Control;
        PropertyBarInstantiate (Control);

    }

    // Update is called once per frame
    void Update () {

    }

    void PropertyBarInstantiate (int control) {

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
                //要给里面的按钮设置回调函数，因为预置体不保留面板设置的回调
                Button openImg2Btn = this.transform.Find ("TempBtnPic2").GetComponent<Button> ();
                GameObject AndroidCamOBJ = GameObject.FindGameObjectWithTag ("AndroidCameraPlane");

                openImg2Btn.onClick.AddListener (delegate () {
                    AndroidCamOBJ.GetComponent<AndroidCamera> ().OpenPhoto (2);
                    
                });

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

                Slider slider1 = this.transform.Find ("Slider1").GetComponent<Slider> ();
                Slider slider2 = this.transform.Find ("Slider2").GetComponent<Slider> ();
                Slider slider3 = this.transform.Find ("Slider3").GetComponent<Slider> ();
                Slider slider4 = this.transform.Find ("Slider4").GetComponent<Slider> ();

                slider1.onValueChanged.AddListener (delegate { PropertyAdjust_13 (1, slider1); });
                slider2.onValueChanged.AddListener (delegate { PropertyAdjust_13 (2, slider2); });
                slider3.onValueChanged.AddListener (delegate { PropertyAdjust_13 (3, slider3); });
                slider4.onValueChanged.AddListener (delegate { PropertyAdjust_13 (4, slider4); });

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

    void PropertyAdjust_13 (int property, Slider slider) {

        GameObject MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
        WobblyGridController WGComponent = MyPixelsOBJ.GetComponent<WobblyGridController> ();

        switch (property) {
            case 1:
                WGComponent.SpacingBetween = slider.value;

                break;
            case 2:
                WGComponent.WaveScale = slider.value;
                break;
            case 3:
                WGComponent.Amplitude = slider.value;
                break;
            case 4:
                WGComponent.Frequency = slider.value;
                break;
        }

    }
}