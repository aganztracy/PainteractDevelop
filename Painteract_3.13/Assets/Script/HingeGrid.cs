using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeGrid : MonoBehaviour {

	//链接关节游戏对象
	GameObject connectedObjRow = null;
	GameObject connectedObjClo = null;
	//当前链接的铰链关节组件
	HingeJoint jointComponentRow = null;
	HingeJoint jointComponentClo = null;

	//定义引用对象
	MyPixel MyPixelOBJ;
	GameObject CanvasOBJ;
	//定义继承的参数
	int Row;
	int Clo;
	Color Col;

	// Use this for initialization
	void Start () {



		// 给每个粒子添加刚体组件
		// 修改刚体组件的属性效果
		gameObject.AddComponent<Rigidbody> ();
		gameObject.GetComponent<Rigidbody> ().useGravity = true;
		gameObject.GetComponent<Rigidbody> ().drag = 0.1f; //    典型的Drag值介于0.001(固体金属)到10(羽毛)之间。
		//gameObject.GetComponent<Rigidbody>().isKinematic = true; // 如果启用该参数，则对象不会被物理所控制
		gameObject.GetComponent<Rigidbody> ().mass = 0.1f; // 重量属性
		// 锁定三个方向的旋转
		gameObject.GetComponent<Rigidbody> ().constraints =
			RigidbodyConstraints.FreezeRotationX; //| RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationZ;

		//物理效果，建立弹性网状结构
		if (Row == 0) { // 最下方一排，不进行连接

		} else if (Clo == 0) { // 最左侧一排，只与其下方一排连接
			Debug.Log ("my row is" + Row + "and my clo is" + Clo);
			jointComponentRow = gameObject.AddComponent<HingeJoint> ();
			//gameObject.GetComponent<HingeJoint>().useSpring = true;
			gameObject.GetComponent<HingeJoint> ().useLimits = true;
			//jointComponentRow2.maxDistance = 3;

			//连接到上方一个连接粒子
			connectedObjRow = CanvasOBJ.GetComponent<ReadPic> ().pixArray[Row - 1, Clo];
			jointComponentRow.connectedBody = connectedObjRow.GetComponent<Rigidbody> ();
		}

		if (Row != 0 && Clo != 0) { // 一般粒子元，和其左方和下方的粒子连接
			Debug.Log ("my row is" + Row + "and my clo is" + Clo);
			jointComponentRow = gameObject.AddComponent<HingeJoint> ();
			//gameObject.GetComponent<HingeJoint>().useSpring = true;
			jointComponentClo = gameObject.AddComponent<HingeJoint> ();
			//jointComponentRow2.maxDistance = 3;
			//jointComponentClo2.maxDistance = 3;

			//连接到上方一个连接粒子
			connectedObjRow = CanvasOBJ.GetComponent<ReadPic> ().pixArray[Row - 1, Clo];
			jointComponentRow.connectedBody = connectedObjRow.GetComponent<Rigidbody> ();

			//连接到左方一个连接粒子
			connectedObjClo = CanvasOBJ.GetComponent<ReadPic> ().pixArray[Row, Clo - 1];
			jointComponentClo.connectedBody = connectedObjClo.GetComponent<Rigidbody> ();
		}

		if (Row == CanvasOBJ.GetComponent<ReadPic> ().rowNum - 1) //最上方一行锁定

		{
			Debug.Log ("my row is" + Row + "and my Clo is" + Clo);
			Debug.Log ("lock" + Row + ":" + Clo);
			gameObject.GetComponent<Rigidbody> ().constraints =
				RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}