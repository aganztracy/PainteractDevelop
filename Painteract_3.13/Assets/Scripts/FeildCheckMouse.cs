using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeildCheckMouse : MonoBehaviour {
    Vector3 preMousePos;
    Vector3 curMousePos;
    Vector3 mouseDirec;

    void Update () {
        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint (transform.position); //从相机的视角将物体世界坐标转化为相对屏幕的三维坐标（只有z坐标是空间中的）
        curMousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));
        mouseDirec = curMousePos - preMousePos;
        if ((Input.touchCount == 1)) //if mouseleft down
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.CompareTag ("MyPixel")) {

                preMousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));
                // hit.collider.gameObject.GetComponent<SpriteRenderer> ().color = Color.black;
                // hit.collider.gameObject.GetComponent<VectorField> ().CheckChange (mouseDirec,curMousePos);
                hit.collider.gameObject.GetComponent<DotFlowController> ().ChangeFeild (mouseDirec, preMousePos);
            }
            preMousePos = curMousePos;
        }
    }
}