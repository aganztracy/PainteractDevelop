using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (AudioSource))]
public class MusicVisualizationController : MonoBehaviour {

	public AudioClip[] clips = new AudioClip[1]; //导入的音频切片数组

	public float colorMultiplyer = 1;
	//[Range(0,1)]
	public float s = 1;
	public float v = 1;

	private int index = 0; //当前音频片段索引

	private AudioSource audio;

	GameObject CanvasOBJ;

	int pixScale_; //获取粒子原大小数据变量

	public AudioClip clipsample_1; //显示当前音频片段

	public GameObject pixel_i; //显示当前粒子对象

	// Use this for initialization
	void Start () {

		audio = GetComponent<AudioSource> ();
		clipsample_1 = Resources.Load<AudioClip> ("audioclip_1"); //加载音频资源
		clips[0] = clipsample_1; //音频片段赋值

		//获取粒子原大小
		CanvasOBJ = GameObject.FindWithTag ("Canvas");
		pixScale_ = CanvasOBJ.GetComponent<ReadPic> ().pixScale;

		Debug.Log ("bei:pixSale = " + pixScale_);

	}

	// Update is called once per frame
	void Update () {

		Visualization (pixScale_);
		// if (Input.GetMouseButtonDown (1)) {
		// 	//当鼠标右键按下，切换到下一首音频
		// 	ChangeSound ();
		// }

	}

	void Visualization (int pixScale) {

		//float[] musicData = audio.GetSpectrumData (128, 0, FFTWindow.Triangle);
		float[] musicData = GetComponent<AudioSource> ().GetSpectrumData (512, 0, FFTWindow.Triangle);

		int i = 0;
		while (i < 360) {
			pixel_i = gameObject.transform.GetChild (i).gameObject;
			//频谱时越向后越小的，为避免后面的数据变化不明显，故在扩大samples[i]时，乘以50+i * i*0.5f
			Vector3 pixelScaleVec = new Vector3 (pixScale, pixScale, Mathf.Clamp (musicData[i+1] * 10000000000, 0, 300));
			
			

            if (pixelScaleVec.z > pixel_i.transform.localScale.z)
            {
                pixel_i.transform.localScale = pixelScaleVec;
                
            }
            else if (pixelScaleVec.z < pixel_i.transform.localScale.z)
            {
                pixel_i.transform.localScale -= new Vector3(0, 0, 1);
            }

			//barsSprites[i].color = HSVtoRGB(musicData[i]*colorMultiplyer,s,v,1);
			// Debug.Log("pixel_"+i+"  done");
			i++;
		}
	}

	void ChangeSound () {

		index++;
		if (index > clips.Length - 1) {
			index = 0;
		}
		print (index);
		audio.clip = clips[index];

		audio.Play ();
	}

	#region Static
	public static Color HSVtoRGB (float hue, float saturation, float value, float alpha) {
		while (hue > 1f) {
			hue -= 1f;
		}
		while (hue < 0f) {
			hue += 1f;
		}
		while (saturation > 1f) {
			saturation -= 1f;
		}
		while (saturation < 0f) {
			saturation += 1f;
		}
		while (value > 1f) {
			value -= 1f;
		}
		while (value < 0f) {
			value += 1f;
		}
		if (hue > 0.999f) {
			hue = 0.999f;
		}
		if (hue < 0.001f) {
			hue = 0.001f;
		}
		if (saturation > 0.999f) {
			saturation = 0.999f;
		}
		if (saturation < 0.001f) {
			return new Color (value * 255f, value * 255f, value * 255f);

		}
		if (value > 0.999f) {
			value = 0.999f;
		}
		if (value < 0.001f) {
			value = 0.001f;
		}

		float h6 = hue * 6f;
		if (h6 == 6f) {
			h6 = 0f;
		}
		int ihue = (int) (h6);
		float p = value * (1f - saturation);
		float q = value * (1f - (saturation * (h6 - (float) ihue)));
		float t = value * (1f - (saturation * (1f - (h6 - (float) ihue))));
		switch (ihue) {
			case 0:
				return new Color (value, t, p, alpha);
			case 1:
				return new Color (q, value, p, alpha);
			case 2:
				return new Color (p, value, t, alpha);
			case 3:
				return new Color (p, q, value, alpha);
			case 4:
				return new Color (t, p, value, alpha);
			default:
				return new Color (value, p, q, alpha);
		}
	}
	#endregion
}