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

                Toggle toggle9_1 = this.transform.Find ("UseMicrophoneToggle").GetComponent<Toggle> ();
                Toggle toggle9_2 = this.transform.Find ("ChangeScaleToggle").GetComponent<Toggle> ();
                Toggle toggle9_3 = this.transform.Find ("ChangePositionToggle").GetComponent<Toggle> ();
                Slider slider9_1 = this.transform.Find ("Slider1").GetComponent<Slider> ();
                Slider slider9_2 = this.transform.Find ("Slider2").GetComponent<Slider> ();

                toggle9_1.onValueChanged.AddListener (delegate { PropertyAdjust_9 (1); });
                toggle9_2.onValueChanged.AddListener (delegate { PropertyAdjust_9 (2); });
                toggle9_3.onValueChanged.AddListener (delegate { PropertyAdjust_9 (3); });
                slider9_1.onValueChanged.AddListener (delegate { PropertyAdjust_9 (4); });
                slider9_2.onValueChanged.AddListener (delegate { PropertyAdjust_9 (5); });

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

                Slider slider13_1 = this.transform.Find ("Slider1").GetComponent<Slider> ();
                Slider slider13_2 = this.transform.Find ("Slider2").GetComponent<Slider> ();
                Slider slider13_3 = this.transform.Find ("Slider3").GetComponent<Slider> ();
                Slider slider13_4 = this.transform.Find ("Slider4").GetComponent<Slider> ();

                slider13_1.onValueChanged.AddListener (delegate { PropertyAdjust_13 (1, slider13_1); });
                slider13_2.onValueChanged.AddListener (delegate { PropertyAdjust_13 (2, slider13_2); });
                slider13_3.onValueChanged.AddListener (delegate { PropertyAdjust_13 (3, slider13_3); });
                slider13_4.onValueChanged.AddListener (delegate { PropertyAdjust_13 (4, slider13_4); });

                break;

                /// <summary>
                /// Other 类别效果
                /// 14-16
                /// </summary>
            case 14: //=========================================================3D Noise Flow Field

                Slider slider14_1 = this.transform.Find ("Slider1").GetComponent<Slider> ();
                Slider slider14_2 = this.transform.Find ("Slider2").GetComponent<Slider> ();
                Slider slider14_3 = this.transform.Find ("Slider3").GetComponent<Slider> ();
                Slider slider14_4 = this.transform.Find ("Slider4").GetComponent<Slider> ();

                slider14_1.onValueChanged.AddListener (delegate { PropertyAdjust_14 (1, slider14_1); });
                slider14_2.onValueChanged.AddListener (delegate { PropertyAdjust_14 (2, slider14_2); });
                slider14_3.onValueChanged.AddListener (delegate { PropertyAdjust_14 (3, slider14_3); });
                slider14_4.onValueChanged.AddListener (delegate { PropertyAdjust_14 (4, slider14_4); });

                break;

            case 15: //=========================================================爆炸粒子系统效果
                break;
            case 16: //=========================================================颜料溅开效果
                break;

            default:
                break;

        }

    }

    void PropertyAdjust_9 (int property) {
        GameObject MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
        AudioVisualizationController AVComponent = MyPixelsOBJ.GetComponent<AudioVisualizationController> ();

        Toggle toggle9_1 = this.transform.Find ("UseMicrophoneToggle").GetComponent<Toggle> ();
        Toggle toggle9_2 = this.transform.Find ("ChangeScaleToggle").GetComponent<Toggle> ();
        Toggle toggle9_3 = this.transform.Find ("ChangePositionToggle").GetComponent<Toggle> ();
        Slider slider9_1 = this.transform.Find ("Slider1").GetComponent<Slider> ();
        Slider slider9_2 = this.transform.Find ("Slider2").GetComponent<Slider> ();

        switch (property) {
            case 1:
                if (toggle9_1.isOn) {
                    AVComponent.useMicrophoneInput ();
                } else {
                    AVComponent.offMicrophoneInput ();
                }
                break;
            case 2:
                if (toggle9_2.isOn) {
                    AVComponent.setChangePixelScale ();
                } else {
                    AVComponent.offChangePixelScale ();
                }
                break;
            case 3:
                if (toggle9_3.isOn) {
                    AVComponent.setChangePixelPostion ();
                } else {
                    AVComponent.offChangePixelPostion ();
                }
                break;
            case 4:
                AVComponent.pixScale = slider9_1.value;

                break;
            case 5:
                AVComponent.SamplesScale = slider9_2.value;
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

    void PropertyAdjust_14 (int property, Slider slider) {

        GameObject MyPixelsOBJ = GameObject.FindWithTag ("MyPixels");
        NoiseFlowFieldController NFComponent = MyPixelsOBJ.GetComponent<NoiseFlowFieldController> ();

        switch (property) {
            case 1:
                NFComponent._increment = slider.value;

                break;
            case 2:
                NFComponent._particleScale = slider.value;
                NFComponent.setParticleScale ();
                break;
            case 3:
                NFComponent._particleMoveSpeed = slider.value;
                break;
            case 4:
                NFComponent._particleRotateSpeed = slider.value;
                break;
        }

    }

}