using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;

public class BeveragesController : MonoBehaviour {

	public int myScreemWidth = UnityEngine.Screen.width;
	public int myScreemHeight = UnityEngine.Screen.height;

	public GameObject BeverageBox; //BeverageBox预制体
	//public Transform BeverageBoxTF; //BeverageBox预制体的位置数组

	public Vector3[] Pos = new Vector3[6];

	public Vector3[] Scale= new Vector3[6];

	int BoxScaleX, BoxScaleY, BoxScaleZ;

	int WallScale = 300;

	// Use this for initialization
	void Start () {

        BeverageBox = Resources.Load<GameObject>("Prefab/BeverageBox");
		//BeverageBoxTF = GameObject.FindWithTag("BeverageBoxs").transform;
		
		BoxScaleX = myScreemWidth;
		BoxScaleY = myScreemHeight;
		BoxScaleZ = 800;

		Pos[0] = new Vector3 (BoxScaleX * 0.5f, BoxScaleY * 0.5f, -BoxScaleZ * 0.5f);
		Pos[1] = new Vector3 (BoxScaleX * 0.5f, BoxScaleY * 0.5f, BoxScaleZ * 0.5f);
		Pos[2] = new Vector3 (-WallScale * 0.5f, BoxScaleY * 0.5f, 0);
		Pos[3] = new Vector3 (BoxScaleX + WallScale * 0.5f, BoxScaleY * 0.5f, 0);
		Pos[4] = new Vector3 (BoxScaleX * 0.5f, BoxScaleY + WallScale * 0.5f, 0);
		Pos[5] = new Vector3 (BoxScaleX * 0.5f, -WallScale * 0.5f, 0);

		Scale[0] = new Vector3 (BoxScaleX, BoxScaleY, WallScale);
		Scale[1] = new Vector3 (BoxScaleX, BoxScaleY, WallScale);
		Scale[2] = new Vector3 (WallScale, BoxScaleY, BoxScaleZ);
		Scale[3] = new Vector3 (WallScale, BoxScaleY, BoxScaleZ);
		Scale[4] = new Vector3 (BoxScaleX + WallScale * 2, WallScale, BoxScaleZ);
		Scale[5] = new Vector3 (BoxScaleX + WallScale * 2, WallScale, BoxScaleZ);

		//gameObject.transform.position = new Vector3 (0, 0, 1);
		//gameObject.GetComponent<BoxCollider> ().center = Pos;
		//gameObject.GetComponent<BoxCollider> ().size = Scale;
		for (int i = 0; i < 6; i++) {
			GameObject tempBeverageBox;
			tempBeverageBox = Instantiate (BeverageBox, Pos[i], Quaternion.identity);
			tempBeverageBox.GetComponent<BoxCollider> ().size = Scale[i];
			tempBeverageBox.transform.SetParent(this.transform);
		}
		//tempBeverageBox = Instantiate (BeverageBox, Pos[i], Quaternion.identity);

	}

	// Update is called once per frame
	void Update () {

	}
}