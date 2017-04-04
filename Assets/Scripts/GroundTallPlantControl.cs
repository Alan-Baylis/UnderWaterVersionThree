using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTallPlantControl : MonoBehaviour {

	Vector3 posOri;
	Vector3 posTemp;

	// Use this for initialization
	void Start () {
		posOri = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		RotateAround ();
	}

	void RotateAround(){
		//		transform.RotateAround(
		//			/*Vector3.zero*/ posOri, 
		//			/*Vector3.up*/ /*new Vector3 (0.0f, (transform.position.x+transform.position.y-transform.position.z),0.0f)*/
		//			Vector3.one, 
		//			20 * Time.deltaTime * (Mathf.Sin(Time.time/*+posOri.x*/)+posOri.x));

		transform.RotateAround(
			posOri, 
			Vector3.up, 
			20 * Time.deltaTime * ((Mathf.Sin(Time.time+transform.position.x-posOri.x)+1)));
	}
}
