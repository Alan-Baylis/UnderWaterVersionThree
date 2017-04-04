using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGroupHighWaveControl : MonoBehaviour {
	public bool highwaving;
	public float highwaveSize;
	private int i;

	private float waveTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		waveTime += Time.deltaTime;

		if (waveTime >=1){
			
			i = Random.Range (0, 10);

			if (i>=1) {
					highwaving = false;
				}

			waveTime = 0;
		}
	}

	public void StartWave(float amplitude) {
		foreach(GroundControl controller in GetComponentsInChildren<GroundControl>()) {
			controller.AdjustAmplitude(amplitude);
		}
	}
}
