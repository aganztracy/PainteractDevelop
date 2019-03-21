﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVisualization2Controller : MonoBehaviour {

	AudioSource audio;

	public float[] samples = new float[512]; //存放频谱数据的数组长度

	GameObject CanvasOBJ;

	int pixScale_; //获取粒子原大小数据变量
	int rowNum;
	int cloNum;

	public AudioClip clipsample_1; //显示当前音频片段

	public GameObject pixel_i; //显示当前粒子对象\

	public Vector3 pixelScaleVec;

	// Use this for initialization
	void Start () {

		audio = GetComponent<AudioSource> ();
		//千万别忘记play()！！！！
		audio.Play ();

		clipsample_1 = Resources.Load<AudioClip> ("audioclip_1"); //加载音频资源

		//获取粒子原大小
		CanvasOBJ = GameObject.FindWithTag ("Canvas");
		pixScale_ = CanvasOBJ.GetComponent<ReadPic> ().pixScale;
		rowNum = CanvasOBJ.GetComponent<ReadPic> ().rowNum;
		cloNum = CanvasOBJ.GetComponent<ReadPic> ().cloNum;

		Debug.Log ("bei:pixSale = " + pixScale_);

	}

	// Update is called once per frame
	void Update () {

		Visualization (pixScale_);

	}

	void Visualization (int pixScale) {

		//float[] musicData = audio.GetSpectrumData (128, 0, FFTWindow.Triangle);

		//获取频谱
		audio.GetSpectrumData (samples, 0, FFTWindow.BlackmanHarris);

		for (int i = 0; i < rowNum*cloNum; i++) {
			pixel_i = gameObject.transform.GetChild (i).gameObject;
			//频谱时越向后越小的，为避免后面的数据变化不明显，故在扩大samples[i]时，乘以50+i * i*0.5f
			//Vector3 pixelScaleVec = new Vector3 (pixScale, pixScale, Mathf.Clamp (musicData[i+1] * 10000000000, 0, 300));
			pixelScaleVec = new Vector3 (pixScale_, pixScale_, Mathf.Clamp (samples[i] * (10000 + i * i * 10.5f), 0, 500)+pixScale_);

			pixel_i.transform.localScale = pixelScaleVec;

		}
	}
}
