using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTestScripts : MonoBehaviour {

	Vector3 posOri;
	Vector3 posTemp;

	// Use this for initialization
	void Start () {
		posOri = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
//		Circle ();
//		Tan();
//		EvenOdd();
//		RotateAround();
//		Linear ();
		Square (); // noding 
	}
		
	void Circle(){
//		posTemp = new Vector3 (
//			Mathf.Cos(Time.time-transform.position.x), 
//			Mathf.Sin(Time.time-transform.position.x), 
//			Mathf.Sqrt(1-Mathf.Pow(0.5f * Mathf.Cos(Time.time-transform.position.x),2)-Mathf.Pow(0.5f * Mathf.Sin(Time.time-transform.position.x),2)));

//		posTemp = new Vector3 (
//			0.0f, 
//			Mathf.Cos(Time.time-transform.position.x)*Mathf.Sin(Time.time-transform.position.x),
//			0.0f);

//		posTemp = new Vector3 (
//			0.0f, 
//			Mathf.Cos(Time.time-transform.position.x)*Mathf.Sin(Time.time-transform.position.z),
//			0.0f);
//
		posTemp = new Vector3 (
			0.0f, 
			Mathf.Cos(Time.time-transform.position.z*0.2f)*Mathf.Sin(Time.time-transform.position.z*0.2f),
			0.0f);

		transform.position = posOri + posTemp;
	}

	void Tan(){
		posTemp = new Vector3 (0, Mathf.Tan(Time.time),0.0f);
		transform.position = posOri + posTemp;
	}

	void EvenOdd(){
		posTemp = new Vector3 (
			0.0f, 
			Mathf.Sin(Time.time)* Mathf.Pow(-1, Mathf.RoundToInt(transform.position.x+transform.position.z)),
//			0.0f,
			0.0f);
		transform.position = posOri + posTemp;
	}

	void RotateAround(){
//		transform.RotateAround(
//			/*Vector3.zero*/ posOri, 
//			/*Vector3.up*/ /*new Vector3 (0.0f, (transform.position.x+transform.position.y-transform.position.z),0.0f)*/
//			Vector3.one, 
//			20 * Time.deltaTime * (Mathf.Sin(Time.time/*+posOri.x*/)+posOri.x));

		transform.RotateAround(
			posOri, 
			Vector3.one, 
			20 * Time.deltaTime * ((Mathf.Sin(Time.time+transform.position.x-posOri.x)+1)));
	}

	void Linear (){
		posTemp.x = Mathf.Sin(Time.time)*posOri.z + posOri.y;
		transform.position = posTemp + posOri;
	}

	void Square(){
		posTemp.y = Mathf.Sqrt (Mathf.Abs (Mathf.Sin (Time.time)));
		transform.position = posOri + posTemp;
	}
}
