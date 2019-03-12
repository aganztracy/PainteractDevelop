using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addSpring : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.AddComponent<SpringJoint>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
