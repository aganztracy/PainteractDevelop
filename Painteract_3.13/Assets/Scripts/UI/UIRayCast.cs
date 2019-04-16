
#if UNITY_ANDROID && !UNITY_EDITOR
#define ANDROID
#endif
 
 
#if UNITY_IPHONE && !UNITY_EDITOR
#define IPHONE
#endif
 
 
 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;



public class UIRayCast : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        // if (IsTouchedUI ()) {
        //     Debug.Log ("当前触摸在UI上");
        // } else {

        // }

        // if (!IsPointerOverGameObject (Input.mousePosition)) {
        //     //没有点击UI
        // }

    }

    public bool IsTouchedUI () {
        bool touchedUI = false;
        if (UnityEngine.Application.isMobilePlatform) {
            if (EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId)) {
                touchedUI = true;
            }
        } else if (EventSystem.current.IsPointerOverGameObject ()) {
            touchedUI = true;
        }
        return touchedUI;
        // Debug.Log(touchedUI);
    }

    //教程说把原函数重载为如下函数后 可以在安卓平台上使用
    public bool IsPointerOverGameObject (Vector2 screenPosition) {
        //实例化点击事件
        PointerEventData eventDataCurrentPosition = new PointerEventData (UnityEngine.EventSystems.EventSystem.current);
        //将点击位置的屏幕坐标赋值给点击事件
        eventDataCurrentPosition.position = new Vector2 (screenPosition.x, screenPosition.y);

        List<RaycastResult> results = new List<RaycastResult> ();
        //向点击处发射射线
        EventSystem.current.RaycastAll (eventDataCurrentPosition, results);

        return results.Count > 0;

        // Debug.Log(results.Count > 0);

    }

//     public void IsOverUI () {  //put the code into UpData()
//         if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
// #if IPHONE || ANDROID
//             if (EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId))

// #else
//                 if (EventSystem.current.IsPointerOverGameObject ())
// #endif
//                     Debug.Log ("当前触摸在UI上");
                    

//                 else
//                     Debug.Log ("当前没有触摸在UI上");
                   
//         }
//     }



    
}

