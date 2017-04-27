using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellFishControl : MonoBehaviour {

	private Rigidbody rb;
	private bool hasFallen = false;
	private bool hasRisen = true;
	private float timer = 0;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {

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

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Predator" && hasRisen == true){
			ShellAction ();
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Predator" && hasFallen == true){
			Destroy(other.gameObject);
		}
	}

	public void ShellAction(){
		StartCoroutine (ShellFallWait ());
	}

	IEnumerator ShellRaise() {
		
		for (float t = 0; t <= 1; t += 0.1f*Time.deltaTime) {
			rb.useGravity = false;
			transform.position = Vector3.Lerp(transform.position, 
				new Vector3(transform.position.x, 
					(Mathf.Sin(Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f) + 3f),
					transform.position.z), t);
			hasRisen = true;
			yield return null;
		}
	}

	IEnumerator ShellFallWait() {
		for (float t = 0; t <= 1; t += 0.2f*Time.deltaTime) {
			if (t >= 0.3f) {
				rb.useGravity = true;
				hasFallen = true;
			}
			yield return null;
		}
	}
}
