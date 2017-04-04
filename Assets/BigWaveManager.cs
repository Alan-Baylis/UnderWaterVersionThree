using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWaveManager : MonoBehaviour {

	public float waveRollTime;
	private float waveTimer;
	public float waveSize;
	// Use this for initialization
	void Start () {
		waveTimer = waveRollTime;
	}
	
	// Update is called once per frame
	void Update () {
		waveTimer -= Time.deltaTime;
		if(waveTimer < 0) {
			waveTimer = waveRollTime;
			float dice = Random.value;
			if(dice < 0.4) {
				int wave = Random.Range(2, transform.childCount - 3);
				transform.GetChild(wave).GetComponent<GroundGroupHighWaveControl>().StartWave(waveSize);
				transform.GetChild(wave - 1).GetComponent<GroundGroupHighWaveControl>().StartWave(waveSize / 2);
				transform.GetChild(wave + 1).GetComponent<GroundGroupHighWaveControl>().StartWave(waveSize / 2);
				transform.GetChild(wave - 2).GetComponent<GroundGroupHighWaveControl>().StartWave(waveSize / 4);
				transform.GetChild(wave + 2).GetComponent<GroundGroupHighWaveControl>().StartWave(waveSize / 4);
			}
		}
	}
}
