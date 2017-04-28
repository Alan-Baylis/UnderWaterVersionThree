using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralOut : MonoBehaviour {

	private Vector3 position;
	private Vector3 heightAdjust;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		heightAdjust = new Vector3 (
			0.0f, 
			GroundSinControl.CalculateSinPosition(transform.position),
			0.0f);

		transform.position = position + heightAdjust;
	}
}
