using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigmentBoomController : MonoBehaviour {

///
/// 	/// 颜料喷溅效果by北
	/// 点击粒子后在粒子位置生成粒子颜色的模拟颜料喷溅效果的sprite（多个随机或按顺序出现）
	/// 然后原粒子会被销毁
	/// 模拟粒子被戳破的效果
	/// 

	// Use this for initialization
	Sprite sp1, sp2;
	int spChoice;
	Material spMaterial;

	//定义引用对象
	MyPixel MyPixelOBJ;
	GameObject CanvasOBJ;
	void Start () {
		sp1 = (Sprite) Resources.Load ("Sprites/whitepigment2", typeof (Sprite)) as Sprite;
		sp2 = (Sprite) Resources.Load ("Sprites/whitepigment3", typeof (Sprite)) as Sprite;
		spMaterial = Resources.Load ("Materials/AtomMaterial") as Material;
		MyPixelOBJ = gameObject.GetComponent<MyPixel> ();
		CanvasOBJ = GameObject.FindWithTag ("Canvas");
		spChoice = 1;
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator OnMouseDown () {
		//将物体由世界坐标系转化为屏幕坐标系 ，由vector3 结构体变量ScreenSpace存储，以用来明确屏幕坐标系Z轴的位置  
		Vector3 ScreenSpace = Camera.main.WorldToScreenPoint (transform.position);
		//完成了两个步骤，1由于鼠标的坐标系是2维的，需要转化成3维的世界坐标系，2只有三维的情况下才能来计算鼠标位置与物体的距离，offset即是距离  
		Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

		Debug.Log ("boom");

		//当鼠标左键按下时  
		while (Input.GetMouseButton (0)) {
			//得到现在鼠标的2维坐标系位置  
			Vector3 curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);
			//将当前鼠标的2维位置转化成三维的位置  
			Vector3 CurPosition = Camera.main.ScreenToWorldPoint (curScreenSpace);
			//在当前粒子位置上贴上贴图
			GameObject pigmentSpriteOBJ = new GameObject ();
			pigmentSpriteOBJ.AddComponent<SpriteRenderer> ();
			SpriteRenderer pigmentSpr = pigmentSpriteOBJ.GetComponent<SpriteRenderer> ();
			pigmentSpr.color = MyPixelOBJ.Col;

			if (spChoice == 1) {
				Debug.Log ("spChoice is" + spChoice);
				pigmentSpr.sprite = sp1;
				spChoice = 2;
			} else {
				Debug.Log ("spChoice is" + spChoice);
				pigmentSpr.sprite = sp2;
				spChoice = 1;
			}

			float r = Random.Range (30, 120);
			pigmentSpriteOBJ.transform.SetParent (CanvasOBJ.GetComponent<ReadPic> ().MyPixelsTF);
			pigmentSpriteOBJ.transform.localScale = new Vector3 (r, r, 1);
			pigmentSpriteOBJ.transform.localPosition = new Vector3 (MyPixelOBJ.PosXY.x, MyPixelOBJ.PosXY.y, 10f); //Sets the coordinates relative to the parent object 
			pigmentSpriteOBJ.transform.rotation = new Quaternion (r, 0, 0, 0);
			//销毁原粒子
			Destroy (gameObject);

			//这个很主要  
			yield return new WaitForFixedUpdate ();
		}

	}
}
