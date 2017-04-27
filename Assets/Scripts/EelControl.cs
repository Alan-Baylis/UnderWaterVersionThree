using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public void EelRaise(){
		StartCoroutine (EelDance ());
	}

	public void EelDown(){
		StartCoroutine (EelFall ());
	}

	IEnumerator EelDance() {
		for (float t = 0; t <= 1; t += 0.1f*Time.deltaTime) {
			transform.position = Vector3.Lerp(transform.position, 
				new Vector3(transform.position.x, 
					(Mathf.Sin(Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f) + 2f),
					transform.position.z), 0.0001f*t);

			yield return null;
		}
	}

	IEnumerator EelFall() {
		for (float t = 0; t <= 1; t += 0.1f*Time.deltaTime) {
			transform.position = Vector3.Lerp(transform.position, 
				new Vector3(transform.position.x, 
					Mathf.Sin(Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f),
					transform.position.z), 0.05f*t);

			yield return null;
		}
	}
}
