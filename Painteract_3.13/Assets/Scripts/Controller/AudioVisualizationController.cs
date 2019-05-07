using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizationController : MonoBehaviour {
	public AudioSource audio;
	AudioPeer audioPeerOBJ;
	public AudioClip clipsample_1; //显示当前音频片段

	int samplesLength = 512; //默认的samples数组大小，若小会在start中扩容

	GameObject CanvasOBJ;

	public float pixScale; //获取粒子原大小数据变量
	int rowNum;
	int cloNum;

	public GameObject pixel_i; //显示当前粒子对象
	public Vector3 pixelPosVec; //粒子位置变量
	public Vector3 pixelScaleVec; //粒子大小变量

	[Range (1, 500)]
	public float SamplesScale; //控制将获得频谱数组值放大的倍数

	//choice
	public bool changePixelScale;
	public bool changePixelPostion;

	public bool useMicrophone;

	public bool isAudioPlay;

	// Use this for initialization
	void Start () {

		audioPeerOBJ = gameObject.GetComponent<AudioPeer> ();
		isAudioPlay = true;

		if (useMicrophone) {

			useMicrophoneInput ();
		}

		audio = GetComponent<AudioSource> ();

		//获取粒子原大小
		CanvasOBJ = GameObject.FindWithTag ("Canvas");
		pixScale = CanvasOBJ.GetComponent<ReadPic> ().pixScale;
		rowNum = CanvasOBJ.GetComponent<ReadPic> ().rowNum;
		cloNum = CanvasOBJ.GetComponent<ReadPic> ().cloNum;

		// Debug.Log ("bei:pixSale = " + pixScale + " row:clo=" + rowNum + ":" + cloNum);

		while (samplesLength < rowNum * cloNum) { //判定音频频谱数组长度若小于粒子数目则增大两倍
			samplesLength *= 2;
		}

		AudioPeer._samplesLength = samplesLength;

		SamplesScale = 1;

	}

	public void useMicrophoneInput () {

		useMicrophone = true;
		audioPeerOBJ._useMicrophone = true;
		StopMusic();
	}

	public void offMicrophoneInput () {
		useMicrophone = false;
		audioPeerOBJ._useMicrophone = false;
		StartMusic();
	}

	public void setChangePixelPostion () {
		changePixelPostion = true;

	}
	public void offChangePixelPostion () {
		changePixelPostion = false;

	}

	public void setChangePixelScale () {
		changePixelScale = true;

	}
		public void offChangePixelScale () {
		changePixelScale = false;

	}

	public void setPixelScale (float pixscale) {
		pixScale = pixscale;
	}

	// Update is called once per frame
	void Update () {

		int bandscount = 0;

		for (int i = 0; i < rowNum * cloNum; i++) {
			pixel_i = gameObject.transform.GetChild (i).gameObject;
			//频谱时越向后越小的，为避免后面的数据变化不明显，故在扩大samples[i]时，乘以50+i * i*0.5f
			//Vector3 pixelScaleVec = new Vector3 (pixScale, pixScale, Mathf.Clamp (musicData[i+1] * 10000000000, 0, 300));

			if (changePixelPostion) {
				pixelPosVec = new Vector3 (pixel_i.transform.localPosition.x, pixel_i.transform.localPosition.y, AudioPeer._audioBandBuffer64[bandscount] * SamplesScale);
				pixel_i.transform.localPosition = pixelPosVec;
			}

			if (changePixelScale) {
				pixelScaleVec = new Vector3 (pixScale, pixScale, Mathf.Clamp (AudioPeer._samplesStereo[i] * (100 + i * i * 0.5f) * SamplesScale, 0, 500) + pixScale);
				pixel_i.transform.localScale = pixelScaleVec;
			}

			bandscount++;

			if (bandscount == 63) { //如果数据超过了64频带，重新循环一遍赋值
				bandscount = 0;
			}
		}

	}

	public void StopMusic () {
		audio.Stop ();
		isAudioPlay = false;
	}

	public void StartMusic(){
		audio.Play ();
		isAudioPlay = true;
	}
}