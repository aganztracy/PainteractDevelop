using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllChildren : MonoBehaviour {


//修改完成



    public void DestroyChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
