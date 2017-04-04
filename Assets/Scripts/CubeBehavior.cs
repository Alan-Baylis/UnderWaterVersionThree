using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CubeBehavior : MonoBehaviour {

	Action<float> TransformationFunction;

	public static GridManager gridManager;

	private bool isWaving;
	Vector3 heightAdjust;
	private float highWaveTimer = 0;
	private int highWavingCounter = 0;

	BoxCollider col;

	// Use this for initialization
	void Awake () {
		col = GetComponent<BoxCollider> ();
		TransformationFunction = DefaultSineMovement;
	}

	// Update is called once per frame
	void Update () {
		TransformationFunction(0.2f);
/*		highWaveTimer += Time.deltaTime;
		if (highWaveTimer >= 10) {
			isWaving = true;
		}

		// highwaving
		if (isWaving == true && highWavingCounter <= 3) {
			heightAdjust = new Vector3 (
				0.0f, 
				3 * Mathf.Sin (Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f), 
				0.0f);

			if (heightAdjust.y <= -2.9f) {
				highWavingCounter += 1;
			}
		
		//stopping highwaving
		}
		else if (isWaving == true && highWavingCounter > 3){
				isWaving = false;
				highWaveTimer = 0.0f;
				highWavingCounter = 0;
			
			}
		else {
				heightAdjust = new Vector3(0.0f, 0.0f, 0.0f);
			}
			
		if (heightAdjust.y <= 0f) {
				col.enabled = false;
			} 
		else {
				col.enabled = true;
			}

		transform.position = transform.position + heightAdjust;*/
	}

	void DefaultSineMovement(float offset) {
		heightAdjust = new Vector3 (
			0.0f, 
			Mathf.Sin (Time.time - transform.position.x * offset - transform.position.z * offset) * Time.deltaTime, 
			0.0f);

		transform.position = transform.position + heightAdjust;
	}

	void BigWave(float offset) {
		heightAdjust = new Vector3 (
			0.0f, 
			Mathf.Sin (Time.time - transform.position.x * offset - transform.position.z * offset) * Time.deltaTime * 3, 
			0.0f);

		transform.position = transform.position + heightAdjust;
	}


	public void StartBigWave() {
		TransformationFunction = BigWave;
	}
}
