using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelItem : PoolItemBase {
    // Start is called before the first frame update
    void Start () {

    }

    public override void Init (){
    //StartCoroutine ();
    }


    public override void Reset (){
    StopAllCoroutines ();
    }
}