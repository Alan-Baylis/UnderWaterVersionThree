using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSinControl : MonoBehaviour {
	
	private Vector3 posOri;
	Vector3 heightAdjust;
	public static float amplitudeModifier = 0.3f;

	// Use this for initialization
	void Start () {
		posOri = transform.position;
	}

	// Update is called once per frame
	void Update () {

		heightAdjust = new Vector3 (
			0.0f,
			CalculateSinPosition(transform.position), 
			0.0f);

			transform.RotateAround (
				posOri, 
				Vector3.forward,
				2.5f * Time.deltaTime * Mathf.Cos (Time.time)
			);

		transform.position = posOri + (heightAdjust * amplitudeModifier);
	}

	public static float CalculateSinPosition(Vector3 position) {
		return Mathf.Sin(Time.time - position.x * 0.2f - position.z * 0.2f);
	}
}
