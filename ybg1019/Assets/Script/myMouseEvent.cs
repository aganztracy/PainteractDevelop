using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myMouseEvent : MonoBehaviour
{
    public Camera painterCamera;
    public GameObject dotFlow;
    public GameObject PicProcessed;// 获取挂载该脚本的"Canvas"物体，为了获取ReadPic.cs中的对象和方法

    int Control = 1;// For convenient debugging to switch function

    Vector2 prePOS = new Vector2(0, 0);
    Vector2 POS = new Vector2(0, 0);

    void Start()
    {
        //获取功能模式控制变量
        PicProcessed = GameObject.FindWithTag("Canvas");
        Control = PicProcessed.GetComponent<ReadPic>().Control;
    }

    void Update()
    {
        POS = Input.mousePosition;
        Vector3 mouseDirec = POS - prePOS;
        if (Input.GetMouseButton(0))//if mouseleft down
        {
            Ray ray = painterCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                prePOS = Input.mousePosition;
                // hit.collider.gameObject.GetComponent<SpriteRenderer>().color=Color.black;
                
                switch (Control)
                {
                    case 1:
                        dotFlow.GetComponent<dotFlowControl>().changeFeild(mouseDirec);//change vectorfeild
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
            prePOS = POS;
        }
    }


}
