using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralIn : MonoBehaviour {

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
			Mathf.Sin (Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f), 
			0.0f);

		transform.position = position + heightAdjust;
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			GameObject.Find ("PlayerShell").GetComponent<PlayerShellControl>().speed = 2.0f;
		}

		if(other.gameObject.tag == "Predator")
		{
			other.gameObject.GetComponent<PredatorControl>().predatorMF = 5;
		}
	}
}
