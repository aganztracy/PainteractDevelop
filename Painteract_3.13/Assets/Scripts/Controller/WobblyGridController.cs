using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblyGridController : MonoBehaviour {
	public GameObject[, ] pixArray;
	public int rowNum;
	public int cloNum;

	public int pixScale;

	GameObject CanvasOBJ;
	GameObject MyPixelOBJ;

	public float SpacingBetween; //粒子之间的空间

	float defaultSpacing = 1;

	public int GirdDivision;

	public float WaveScale,Amplitude, Frequency;

	public bool isWave;

	// Use this for initialization
	void Start () {
		CanvasOBJ = GameObject.FindWithTag ("Canvas");
		MyPixelOBJ = GameObject.FindWithTag ("MyPixels");
		pixArray = CanvasOBJ.GetComponent<ReadPic> ().pixArray;
		rowNum = CanvasOBJ.GetComponent<ReadPic> ().rowNum;
		cloNum = CanvasOBJ.GetComponent<ReadPic> ().cloNum;
		pixScale = CanvasOBJ.GetComponent<ReadPic> ().pixScale;

		SpacingBetween = defaultSpacing;
		GirdDivision = 2;
		isWave = true;

		WaveScale = 0.5f;
		Amplitude = 1;
		Frequency = 1;

		ChangeGrid (SpacingBetween);

	}

	// Update is called once per frame
	void Update () {

		//ChangeGrid (SpacingBetween);
		if (isWave) {

			for (int i = 0; i < rowNum * cloNum; i++) {
				//more details in the video "gripple water shader"
				float wave = WaveScale*Mathf.Sin (Time.fixedTime * Amplitude + (i * Frequency));
				float wave2 = WaveScale*Mathf.PerlinNoise (Time.fixedTime * Amplitude + (i * Frequency), (Time.fixedTime * Amplitude + (i * Frequency)));
				GameObject pixel_i = MyPixelOBJ.transform.GetChild (i).gameObject;
				pixel_i.transform.localPosition =
					new Vector3 (pixel_i.transform.localPosition.x, pixel_i.transform.localPosition.y, pixel_i.transform.localPosition.z + (wave * wave2) * Random.Range (1, 3));

			}
		}

	}

	void ChangeGrid (float spacing) {
		for (int y = 0, row = 0; row < rowNum; y = y + pixScale, row++) {
			for (int x = 0, clo = 0; clo < cloNum; x = x + pixScale, clo++) {

				float Rtemp = 1;
				float Ctemp = 1;
				if (row > rowNum / GirdDivision) {
					Rtemp = (rowNum / GirdDivision) - (row - (rowNum / GirdDivision));
				} else {
					Rtemp = row;
				}

				if (clo > cloNum / GirdDivision) {
					Ctemp = (cloNum / GirdDivision) - (clo - (cloNum / GirdDivision));
				} else {
					Ctemp = clo;
				}

				float oldz = pixArray[row, clo].transform.localPosition.z;
				pixArray[row, clo].transform.localPosition = new Vector3 (x * spacing, y * spacing, oldz + (Ctemp + 1) * (Rtemp + 1));

			}
		}
	}
}