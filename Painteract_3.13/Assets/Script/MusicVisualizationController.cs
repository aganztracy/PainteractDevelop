using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (AudioSource))]
public class MusicVisualizationController : MonoBehaviour {

	AudioSource audio;

	public float[] samples; //存放频谱数据的数组长度
	int samplesLength = 512;//默认的samples数组大小，若小会在start中扩容

	GameObject CanvasOBJ;

	int pixScale; //获取粒子原大小数据变量
	int rowNum;
	int cloNum;

	public AudioClip clipsample_1; //显示当前音频片段

	public GameObject pixel_i; //显示当前粒子对象\

	public Vector3 pixelPosVec;

	// Use this for initialization
	void Start () {

		audio = GetComponent<AudioSource> ();
		//千万别忘记play()！！！！
		audio.Play ();

		clipsample_1 = Resources.Load<AudioClip> ("audio/audioclip_1"); //加载音频资源

		//获取粒子原大小
		CanvasOBJ = GameObject.FindWithTag ("Canvas");
		pixScale = CanvasOBJ.GetComponent<ReadPic> ().pixScale;
		rowNum = CanvasOBJ.GetComponent<ReadPic> ().rowNum;
		cloNum = CanvasOBJ.GetComponent<ReadPic> ().cloNum;

		Debug.Log ("bei:pixSale = " + pixScale+" row:clo="+rowNum+":"+cloNum);

		while(samplesLength<rowNum*cloNum){//判定音频频谱数组长度若小于粒子数目则增大两倍
			samplesLength *=2;
		}

		samples = new float[samplesLength];//定义音频频谱数组长度

	}

	// Update is called once per frame
	void Update () {

		Visualization (pixScale);

	}

	void Visualization (int pixScale) {

		//float[] musicData = audio.GetSpectrumData (128, 0, FFTWindow.Triangle);

		//获取频谱
		audio.GetSpectrumData (samples, 0, FFTWindow.BlackmanHarris);

		for (int i = 0; i < rowNum*cloNum; i++) {
			pixel_i = gameObject.transform.GetChild (i).gameObject;
			//频谱时越向后越小的，为避免后面的数据变化不明显，故在扩大samples[i]时，乘以50+i * i*0.5f
			//Vector3 pixelScaleVec = new Vector3 (pixScale, pixScale, Mathf.Clamp (musicData[i+1] * 10000000000, 0, 300));
			pixelPosVec = new Vector3 (pixel_i.transform.localPosition.x, pixel_i.transform.localPosition.y, Mathf.Clamp (samples[i] * (10000 + i * i * 0.5f), 0, 100));

			pixel_i.transform.localPosition = pixelPosVec;

		}
	}

}