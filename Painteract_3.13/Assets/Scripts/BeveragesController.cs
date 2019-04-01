using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;

public class BeveragesController : MonoBehaviour {
/// <summary>
/// 粒子饮料 粒子为无任何连接的受重力作用的刚体
/// 脚本功能为在场景中生成六面预制墙体（挂有BoxCollider的空物体）
/// 使形成一个长高为屏幕大小，宽为自定义大小的碰撞体盒子
/// 限制粒子在盒子中运动（受盒子壁、其他粒子的碰撞）
/// </summary>
	public int myScreemWidth = UnityEngine.Screen.width;
	public int myScreemHeight = UnityEngine.Screen.height;

	public GameObject BeverageBox; //BeverageBox预制体
	//public Transform BeverageBoxTF; //BeverageBox预制体的位置数组

	public Vector3[] Pos = new Vector3[6];//六面墙体的位置信息数组

	public Vector3[] Scale= new Vector3[6];//六面墙体的大小信息数组

	int BoxScaleX, BoxScaleY, BoxScaleZ;//装粒子的盒子的大小数据

	int WallScale = 300;//六面墙体的厚度：厚度太小粒子会穿过碰撞体掉出，太厚会导致碰撞体重叠后粒子的运动混乱

	// Use this for initialization
	void Start () {
		//获取预置体
        BeverageBox = Resources.Load<GameObject>("Prefabs/BeverageBox");
		//BeverageBoxTF = GameObject.FindWithTag("BeverageBoxs").transform;
		
		//设定装粒子的盒子的大小
		BoxScaleX = myScreemWidth;
		BoxScaleY = myScreemHeight;
		BoxScaleZ = 800;

		//设定六面墙的位置信息（前，后，左，右，上，下）
		Pos[0] = new Vector3 (BoxScaleX * 0.5f, BoxScaleY * 0.5f, -BoxScaleZ * 0.5f);
		Pos[1] = new Vector3 (BoxScaleX * 0.5f, BoxScaleY * 0.5f, BoxScaleZ * 0.5f);
		Pos[2] = new Vector3 (-WallScale * 0.5f, BoxScaleY * 0.5f, 0);
		Pos[3] = new Vector3 (BoxScaleX + WallScale * 0.5f, BoxScaleY * 0.5f, 0);
		Pos[4] = new Vector3 (BoxScaleX * 0.5f, BoxScaleY + WallScale * 0.5f, 0);
		Pos[5] = new Vector3 (BoxScaleX * 0.5f, -WallScale * 0.5f, 0);

		//设定六面墙的大小信息（前，后，左，右，上，下）
		Scale[0] = new Vector3 (BoxScaleX, BoxScaleY, WallScale);
		Scale[1] = new Vector3 (BoxScaleX, BoxScaleY, WallScale);
		Scale[2] = new Vector3 (WallScale, BoxScaleY, BoxScaleZ);
		Scale[3] = new Vector3 (WallScale, BoxScaleY, BoxScaleZ);
		Scale[4] = new Vector3 (BoxScaleX + WallScale * 2, WallScale, BoxScaleZ);
		Scale[5] = new Vector3 (BoxScaleX + WallScale * 2, WallScale, BoxScaleZ);

		//依次生成六面墙
		for (int i = 0; i < 6; i++) {
			GameObject tempBeverageBox;
			tempBeverageBox = Instantiate (BeverageBox, Pos[i], Quaternion.identity);
			tempBeverageBox.GetComponent<BoxCollider> ().size = Scale[i];
			tempBeverageBox.transform.SetParent(this.transform);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}