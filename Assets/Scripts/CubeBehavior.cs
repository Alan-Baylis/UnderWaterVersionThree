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
