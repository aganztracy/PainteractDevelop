using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAllChildren : MonoBehaviour {


//修改完成


    public void destroyChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
