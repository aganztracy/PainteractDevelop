using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMouseEvent : MonoBehaviour {
    public Camera PainterCamera;
    public GameObject DotFlowOBJ;
    public GameObject CanvasOBJ; // 获取挂载该脚本的"Canvas"物体，为了获取ReadPic.cs中的对象和方法

    UIRayCast uiRayCast;

    int Control = 1; // For convenient debugging to switch function

    Vector2 prePos = new Vector2 (0, 0);
    Vector2 pos = new Vector2 (0, 0);

    void Start () {
        //获取功能模式控制变量
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        Control = CanvasOBJ.GetComponent<ReadPic> ().Control;
        
    }

    void Update () {
        pos = Input.mousePosition;
        Vector3 mouseDirec = pos - prePos;

            if (Input.GetMouseButton (0)) //if mouseleft down
            {
                Ray ray = PainterCamera.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)) {
                    prePos = Input.mousePosition;
                    // hit.collider.gameObject.GetComponent<SpriteRenderer>().color=Color.black;

                    switch (Control) {
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
                        default:
                            break;
                    }

                }
                prePos = pos;

        }
    }




}