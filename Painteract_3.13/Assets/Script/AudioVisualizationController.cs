using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizationController : MonoBehaviour {

	int samplesLength = 512; //默认的samples数组大小，若小会在start中扩容

	GameObject CanvasOBJ;	

	int pixScale; //获取粒子原大小数据变量
	int rowNum;
	int cloNum;

	public AudioClip clipsample_1; //显示当前音频片段

	public GameObject pixel_i; //显示当前粒子对象\

	public Vector3 pixelPosVec;

	// Use this for initialization
	void Start () {

		//获取粒子原大小
		CanvasOBJ = GameObject.FindWithTag ("Canvas");
		pixScale = CanvasOBJ.GetComponent<ReadPic> ().pixScale;
		rowNum = CanvasOBJ.GetComponent<ReadPic> ().rowNum;
		cloNum = CanvasOBJ.GetComponent<ReadPic> ().cloNum;

		Debug.Log ("bei:pixSale = " + pixScale + " row:clo=" + rowNum + ":" + cloNum);

		while (samplesLength < rowNum * cloNum) { //判定音频频谱数组长度若小于粒子数目则增大两倍
			samplesLength *= 2;
		}

		AudioPeer._samplesLength = samplesLength;

	}

	// Update is called once per frame
	void Update () {

	}
}