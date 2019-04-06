using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAllController : MonoBehaviour
{
    public void RemoveAllControllerComponent()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {

            transform.GetChild(i).gameObject.GetComponent<MyPixel>().RemoveControllerComponent();
        }
    }
}
