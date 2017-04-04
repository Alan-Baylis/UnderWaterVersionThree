using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTemp : MonoBehaviour {
	private Rigidbody rb;
    Vector3 movement;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		movement = 
			//Camera.main.transform.forward * moveVertical;//
			new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * 100);
//		Camera.main.transform.RotateAround(transform.position, Vector3.up, moveHorizontal);
		//trying camera, not working : (
		//		rb.transform.position += camera.transform.forward * Time.deltaTime * moveVertical;
		//		rb.transform.Rotate (0f, moveHorizontal, 0f);
	}
}
