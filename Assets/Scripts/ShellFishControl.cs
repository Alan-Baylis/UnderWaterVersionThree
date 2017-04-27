using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellFishControl : MonoBehaviour {

	private Rigidbody rb;
	private Vector3 posOri;
	private bool hasFallen = false;
	private bool hasRaise = true;
	private float timer = 0;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		//posOri = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F) && hasRaise == true) {
			StartCoroutine (ShellFallWait ());
		}

		if (hasFallen == true) {
			timer += Time.deltaTime;
			if (timer >= 10) {
				rb.useGravity = false;
				hasFallen = false;
				timer = 0;
				StartCoroutine (ShellRaise ());
			}
		}
	}

//	void OnTriggerEnter(Collider other){
//		if (other.gameObject.tag == "Predator"){
//			StartCoroutine (ShellFallWait ());
//		}
//	}

	IEnumerator ShellRaise() {
		Vector3 posTemp = gameObject.GetComponentInParent<Transform>().position + new Vector3(0,2,0);
		for (float t = 0; t <= 1; t += 0.1f*Time.deltaTime) {
			rb.useGravity = false;
			transform.position = Vector3.Lerp(transform.position, 
				/*(gameObject.GetComponentInParent<Transform>().position + new Vector3(0,2,0))*/ posTemp, t);
			hasRaise = true;
			yield return null;
		}
	}

	IEnumerator ShellFallWait() {
		for (float t = 0; t <= 1; t += 0.2f*Time.deltaTime) {
			if (t >= 0.1f) {
				rb.useGravity = true;
				hasFallen = true;
			}
			yield return null;
		}
	}
}
