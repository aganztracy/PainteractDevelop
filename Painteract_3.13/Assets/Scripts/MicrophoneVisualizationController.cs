using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneVisualizationController : MonoBehaviour {

	public float volume;
	private AudioClip micRecord;
	string device; //记录设备麦克风名称

	/// <summary>
	/// 拖尾的移动速度   要和摄像机的移动速度一致
	/// </summary>
	private int speed;
	private float x;
	private float y;

	float PosX;
	float PosY;
	MyPixel MyPixelOBJ;

	// Use this for initialization
	void Start () {
		//初始化速度的值
		speed = 10;
		device = Microphone.devices[0]; //获取设备麦克风
		//开启设备
		micRecord = Microphone.Start (device, true, 999, 44100); //44100音频采样率   固定格式

		MyPixelOBJ = gameObject.GetComponent<MyPixel> ();
		x = gameObject.transform.position.x;
		y = gameObject.transform.position.y;
		PosX = MyPixelOBJ.PosXY.x;
		PosY = MyPixelOBJ.PosXY.y;
	}

	// Update is called once per frame
	void Update () {
		volume = GetMaxVolume ();
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}

		//要加粒子特效  产生拖尾
		transform.Translate (Vector3.right * speed * Time.deltaTime);

		//print(volume);
		//处理峰值
		if (volume > 0.9f) {
			volume = volume * speed * Time.deltaTime;
			gameObject.transform.localPosition = new Vector3 (PosX, PosY, volume * 10);
		} else {
			gameObject.transform.localPosition = new Vector3 (PosX, PosY, volume * 10);
		}

		Debug.Log (volume);

	}

	//每一振处理那一帧接收的音频文件
	float GetMaxVolume () {
		float maxVolume = 0f;
		//剪切音频
		float[] volumeData = new float[128];
		int offset = Microphone.GetPosition (device) - 128 + 1;
		if (offset < 0) {
			return 0;
		}
		micRecord.GetData (volumeData, offset);

		for (int i = 0; i < 128; i++) {
			float tempMax = volumeData[i]; //修改音量的敏感值
			if (maxVolume < tempMax) {
				maxVolume = tempMax;
			}
		}
		return maxVolume;
	}

}