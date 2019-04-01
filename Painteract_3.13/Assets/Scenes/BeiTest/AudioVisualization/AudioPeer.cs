using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour {
	AudioSource _audioSource;

	//Microphone input
	public AudioClip _audioclip;
	public bool _useMicrophone;
	public string _selectedDevice;
	//different input use different audio mixer group (master/microphone)
	public AudioMixerGroup _mixerGroupMicrophone, _mixerGroupMaster;

	// float[] _samples = new float[512];
	private float[] _samplesLeft;
	private float[] _samplesRight;
	public static float[] _samplesStereo;
	public static int _samplesLength = 512;

	//audio 8
	private float[] _freqBand = new float[8];
	private float[] _bandBuffer = new float[8];
	private float[] _bufferDecrease = new float[8];
	//把data的值转换到0-1区间内
	private float[] _freqBandHighest = new float[8];

	//audio 64
	private float[] _freqBand64 = new float[64];
	private float[] _bandBuffer64 = new float[64];
	private float[] _bufferDecrease64 = new float[64];
	//把data的值转换到0-1区间内
	private float[] _freqBandHighest64 = new float[64];

	[HideInInspector]
	public static float[] _audioBand, _audioBandBuffer;

	//audio 64
	[HideInInspector]
	public static float[] _audioBand64, _audioBandBuffer64;

	[HideInInspector]
	public static float _Amplitude, _AmplitudeBuffer;
	private float _AmplitudeHighest;
	public float _audioProfile;

	public enum _channel { Stereo, Left, Right }; //在inspector中会有一个下拉菜单可选择采样类型：立体声，左声道，右声道

 public _channel channel = new _channel ();

 // Use this for initialization
 void Start () {

 _samplesLeft = new float[_samplesLength];
 _samplesRight = new float[_samplesLength];
 _samplesStereo = new float [_samplesLength];

 _audioBand = new float[8];
 _audioBandBuffer = new float[8];
 _audioBand64 = new float[64];
 _audioBandBuffer64 = new float[64];

_mixerGroupMicrophone = Resources.Load<AudioMixerGroup>("Audio/AudioMixer/Microphone");


 _audioSource = GetComponent<AudioSource> ();
 AudioProfile (_audioProfile);

 _audioclip = Resources.Load<AudioClip> ("audio/audioclip_1");

 //Microphone input

 if (_useMicrophone) {

 if (Microphone.devices.Length > 0) {

 _selectedDevice = Microphone.devices[0].ToString ();
 _audioSource.outputAudioMixerGroup = _mixerGroupMicrophone;
 _audioSource.clip = Microphone.Start (_selectedDevice, true, 10, AudioSettings.outputSampleRate);

 } else {
 _useMicrophone = false;
			}

		}

		if (!_useMicrophone) {
			_audioSource.outputAudioMixerGroup = _mixerGroupMaster;
			_audioSource.clip = _audioclip;
		}

		_audioSource.Play ();

	}

	// Update is called once per frame
	void Update () {
		GetSpectrumAudioSource ();
		GetStereoSpectrumAudioSource();
		MakeFrequenctBands ();
		MakeFrequenctBands64 ();
		BandBuffer ();
		BandBuffer64 ();
		CreateAudioBands ();
		CreateAudioBands64 ();
		GetAmplitude (); //Get Average Amplitude

	}

	void AudioProfile (float audioProfile) {
		for (int i = 0; i < 8; i++) {
			_freqBandHighest[i] = audioProfile;
		}
	}

	void GetAmplitude () {

		float _CurrentAmplitude = 0;
		float _CurrentAmplitudeBuffer = 0;
		for (int i = 0; i < 8; i++) {
			_CurrentAmplitude += _audioBand[i];
			_CurrentAmplitudeBuffer += _audioBandBuffer[i];
		}

		if (_CurrentAmplitude > _AmplitudeHighest) {

			_AmplitudeHighest = _CurrentAmplitude;
		}

		_Amplitude = _CurrentAmplitude / _AmplitudeHighest;
		_AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
	}

	void CreateAudioBands () {
		for (int i = 0; i < 8; i++) {
			if (_freqBand[i] > _freqBandHighest[i]) {
				_freqBandHighest[i] = _freqBand[i];
			}

			_audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
			_audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
		}
	}

	void CreateAudioBands64 () {
		for (int i = 0; i < 64; i++) {
			if (_freqBand64[i] > _freqBandHighest64[i]) {
				_freqBandHighest64[i] = _freqBand64[i];
			}

			_audioBand64[i] = (_freqBand64[i] / _freqBandHighest64[i]);
			_audioBandBuffer64[i] = (_bandBuffer64[i] / _freqBandHighest64[i]);
		}
	}

	void GetSpectrumAudioSource () {

		//第二个参数0代表的是左声道，1是右声道
		_audioSource.GetSpectrumData (_samplesLeft, 0, FFTWindow.Blackman);
		_audioSource.GetSpectrumData (_samplesRight, 1, FFTWindow.Blackman);
	}

	void GetStereoSpectrumAudioSource(){

		for(int i = 0;i< _samplesLength;i++){
			_samplesStereo[i] = _samplesLeft[i] + _samplesRight[i];
		}
	}

	void BandBuffer () {
		for (int g = 0; g < 8; g++) {
			if (_freqBand[g] > _bandBuffer[g]) {
				_bandBuffer[g] = _freqBand[g];
				_bufferDecrease[g] = 0.005f;
			}

			if (_freqBand[g] < _bandBuffer[g]) {
				_bandBuffer[g] -= _bufferDecrease[g];
				_bufferDecrease[g] *= 1.2f;
			}

		}
	}

	void BandBuffer64 () {
		for (int g = 0; g < 64; g++) {
			if (_freqBand64[g] > _bandBuffer64[g]) {
				_bandBuffer64[g] = _freqBand64[g];
				_bufferDecrease64[g] = 0.005f;
			}

			if (_freqBand64[g] < _bandBuffer64[g]) {
				_bandBuffer64[g] -= _bufferDecrease64[g];
				_bufferDecrease64[g] *= 1.2f;
			}

		}
	}

	void MakeFrequenctBands () {

		/*22050 /512 = 43hertz per sample
		*
		*20 - 60 hertz
		 60 - 250 hertz
		 250 - 500 hertz
		 2000 - 4000 hertz
		 4000 - 6000 hertz
		 6000 - 20000 hertz
		*
		*0 - 2 =86 hertz
		*1 - 4 = 172 hertz  - 87-258
		*2 - 8 = 344 hertz - 259-602
		*3 - 16 = 688 hertz - 603-1290
		*4 - 32 = 1376 hertz -1291-2666
		*5 - 64 = 2752 hertz - 2667- 5418
		*6 - 128 = 5504 hertz - 5419-10922
		*7 - 256 = 11008hertz - 10923-21930
		*510
		 */

		int count = 0;
		for (int i = 0; i < 8; i++) {
			float average = 0;
			int sampleCount = (int) Mathf.Pow (2, i) * 2;

			if (i == 7) {
				//510 less than 512
				sampleCount += 2;
			}
			for (int j = 0; j < sampleCount; j++) {
				if (channel == _channel.Stereo) {
					average += (_samplesLeft[count] + _samplesRight[count]) * (count + 1);
				}

				if (channel == _channel.Left) {
					average += (_samplesLeft[count] * (count + 1));
				}
				if (channel == _channel.Right) {
					average += (_samplesRight[count]) * (count + 1);
				}
				count++;

			}

			average /= count;

			_freqBand[i] = average * 10;
		}
	}

	void MakeFrequenctBands64 () {

		/*
		 *
		 *0 - 15 = 1 sample  =16
		 *16 - 31 = 2 sample =32
		 *32 - 39 = 4 sample =32
		 *40 - 47 = 6 sample =48
		 *48 - 55 = 16 sample =128
		 *56 - 63 = 32 sample =256
		 *512
		 */

		int count = 0;
		int sampleCount = 1;
		int power = 0;

		for (int i = 0; i < 64; i++) {
			float average = 0;

			if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56) {

				power++;
				sampleCount = (int) Mathf.Pow (2, power);

				if (power == 3) {
					sampleCount -= 2; //40-47 = 6
				}
			}

			for (int j = 0; j < sampleCount; j++) {
				if (channel == _channel.Stereo) {
					average += (_samplesLeft[count] + _samplesRight[count]) * (count + 1);
				}

				if (channel == _channel.Left) {
					average += (_samplesLeft[count] * (count + 1));
				}
				if (channel == _channel.Right) {
					average += (_samplesRight[count]) * (count + 1);
				}
				count++;

			}

			average /= count;

			_freqBand64[i] = average * 10;
		}
	}

}