using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholePlatformWaveChange : MonoBehaviour {

	public float waveAmplitude = 1;

	bool highwaving = false;
	public float highwaveSize;
	private int i;

	public float fullWaveTime = 2; 
	private float waveTime = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {		

//		waveTime += Time.deltaTime;
//		
//		if (waveTime >=1 && highwaving==false){
//					
//			i = Random.Range (0, 100);
//			
//			if (i <10) {
//				Debug.Log (i * fullWaveTime);
//				HighWave ();
//				Invoke ("StopHighWave", i * fullWaveTime);
//			}
//		} 

		if (highwaving == false){	
			i = Random.Range (0, 100);
			if (i <10) {
				HighWave();
			}
		} 

		if (highwaving == true){
			waveTime += Time.deltaTime;
			if (waveTime >= i){
				StopHighWave ();
			}
		}
	}

	void HighWave(){
		highwaving = true;
		waveAmplitude = 1 + Mathf.Abs(Mathf.Sin(Time.time));
		Debug.Log ("wave started" + i);
	}

	void StopHighWave() {
		Debug.Log ("wave stopped");
		highwaving = false;
		waveAmplitude = 1;
		waveTime = 0;
	}
}
