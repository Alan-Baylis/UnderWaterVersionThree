using UnityEngine;
using System.Collections;

public class GroundControl : MonoBehaviour {

//	public float delayAdjustX;
//	public float delayAdjustZ;
	float amplitude;
	float adjustedAmplitude;
	public float bigWaveTimer;
	private Vector3 position;

	public bool touchingHighWaveMaker = false;

	Vector3 heightAdjust;

	// Use this for initialization
	void Start () {
		position = transform.position;
		amplitude = 1;
		adjustedAmplitude = 1;
		bigWaveTimer = 0;
	}

	// Update is called once per frame
	void Update () {
		//amplitude = GameObject.Find ("GroundGroup1").GetComponent<WholePlatformWaveChange>().waveAmplitude;
		if(bigWaveTimer > 0) {
			bigWaveTimer -= Time.deltaTime;
			if(bigWaveTimer < 0) {
				adjustedAmplitude = 1;
			}
		}
			heightAdjust = new Vector3 (
				0.0f, 
				Mathf.Sin (Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f), 
				0.0f);

		if(Mathf.Abs(heightAdjust.y) < 0.1) {
			if(amplitude != adjustedAmplitude) {
				bigWaveTimer = 7;
			}
			amplitude = adjustedAmplitude;
		}
		transform.position = position + heightAdjust * amplitude;
	}

	public void AdjustAmplitude(float newAmp) {
		adjustedAmplitude = newAmp;
	}

	public void MoveCube(Vector3 newPosition) {
		newPosition.y = 0;
		position = newPosition;
	}
}
