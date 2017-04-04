using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSinControl2 : MonoBehaviour {
	
	private Vector3 posOri;
	Vector3 heightAdjust;

	// Use this for initialization
	void Start () {
		posOri = transform.position;
	}

	// Update is called once per frame
	void Update () {

		heightAdjust = new Vector3 (
			0.0f, 
			0.5f * Mathf.Sin (Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f) + 0.5f, 
			0.0f);

			transform.RotateAround (
				posOri, 
			//Vector3.one, 
				Vector3.forward/* Mathf.Pow(-1, Mathf.Floor(Time.time))*/,
				2.5f * Time.deltaTime * Mathf.Cos (Time.time /*+transform.position.x + transform.position.z */)
			);
		
		transform.position = posOri + heightAdjust;
	}
}
