using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring3DController : MonoBehaviour {

	//链接关节游戏对象
	GameObject connectedObjRow = null;
	GameObject connectedObjClo = null;
	//当前链接的弹簧关节组件
	SpringJoint jointComponentRow = null;
	SpringJoint jointComponentClo = null;

	//定义引用对象
	MyPixel MyPixelOBJ;
	GameObject CanvasOBJ;
	//定义继承的参数
	int Row;
	int Clo;
	Color Col;

	int RowNum;
	int CloNum;

	// Use this for initialization
	void Start () {

		MyPixelOBJ = gameObject.GetComponent<MyPixel> ();
		CanvasOBJ = GameObject.FindWithTag ("Canvas");
		Row = MyPixelOBJ.Row;
		Clo = MyPixelOBJ.Clo;
		Col = MyPixelOBJ.Col;
		RowNum = CanvasOBJ.GetComponent<ReadPic> ().rowNum;
		CloNum = CanvasOBJ.GetComponent<ReadPic> ().cloNum;

		// 给每个粒子添加刚体组件
		gameObject.AddComponent<Rigidbody> ();
		// 锁定三个方向的旋转
		gameObject.GetComponent<Rigidbody> ().constraints =
			RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

		//物理效果，建立弹性网状结构
		if (Row == 0) { // 最下方一排，不进行连接

		} else if (Clo == 0) { // 最左侧一排，只与其下方一排连接
			//Debug.Log("my row is"+Row+"and my clo is"+Clo);
			jointComponentRow = gameObject.AddComponent<SpringJoint> ();
			jointComponentRow.maxDistance = 3;

			//连接到下方一个连接粒子
			connectedObjRow = CanvasOBJ.GetComponent<ReadPic> ().pixArray[Row - 1, Clo];
			jointComponentRow.connectedBody = connectedObjRow.GetComponent<Rigidbody> ();

		}

		if (Row != 0 && Clo != 0) { // 一般粒子元，和其左方和下方的粒子连接
			//Debug.Log("my row is" + Row + "and my clo is" + Clo);
			jointComponentRow = gameObject.AddComponent<SpringJoint> ();
			jointComponentClo = gameObject.AddComponent<SpringJoint> ();
			jointComponentRow.maxDistance = 3;
			jointComponentClo.maxDistance = 3;

			//连接到下方一个连接粒子
			connectedObjRow = CanvasOBJ.GetComponent<ReadPic> ().pixArray[Row - 1, Clo];
			jointComponentRow.connectedBody = connectedObjRow.GetComponent<Rigidbody> ();

			//连接到左方一个连接粒子
			connectedObjClo = CanvasOBJ.GetComponent<ReadPic> ().pixArray[Row, Clo - 1];
			jointComponentClo.connectedBody = connectedObjClo.GetComponent<Rigidbody> ();
		}

		//四个角的粒子锁定

		if (Row == RowNum - 1||Row == 0) {
			if (Clo == 0 || Clo == CloNum - 1) {
				gameObject.GetComponent<Rigidbody> ().constraints =
					RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			}
		}

	}

}