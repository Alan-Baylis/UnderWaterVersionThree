using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCube : MonoBehaviour {
	Vector3 posTemp;
	private Vector3 posOri;
	Vector3 heightAdjust;
	public AnimationCurve rotationCurve;

	// Use this for initialization
	void Start () {
		posOri = transform.position;
		//rotationCurve.postWrapMode = WrapMode.ClampForever;
		//rotationCurve.postWrapMode = WrapMode.Loop;
	}

	// Update is called once per frame
	void Update () {

		heightAdjust = new Vector3 (
			0.0f,
			GroundSinControl.CalculateSinPosition(transform.position), 
			0.0f);

		transform.RotateAround(
			posOri, 
			Vector3.forward,
			0.25f * Mathf.PI);
//			rotationCurve.Evaluate (Time.time));

		transform.position = posOri + heightAdjust;
	}
}
