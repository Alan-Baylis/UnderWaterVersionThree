using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHiddenWave : MonoBehaviour {

	private Vector3 position;
	private bool isWaving;
	Vector3 heightAdjust;
	private float highWaveTimer = 0;
	private int highWavingCounter = 0;

	BoxCollider col;

	// Use this for initialization
	void Start () {
		position = transform.position;
		col = GetComponent<BoxCollider> ();
	}

	// Update is called once per frame
	void Update () {

		highWaveTimer += Time.deltaTime;
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

		transform.position = position + heightAdjust;
	}
//
//	public void MoveCube(Vector3 newPosition) {
//		newPosition.y = 0;
//		position = newPosition;
//	}
}
