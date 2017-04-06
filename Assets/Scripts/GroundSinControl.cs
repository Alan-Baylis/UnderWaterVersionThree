using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSinControl : MonoBehaviour {
	
	private Vector3 posOri;
	Vector3 heightAdjust;
	private Rigidbody rb;
//
	GameObject player;
	float distToPlayer;
//	Quaternion rotation;
//
	// Use this for initialization
	void Start () {
		posOri = transform.position;
		player = GameObject.Find ("PlayerShell");
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {

		// old transform code
		heightAdjust = new Vector3 (
			0.0f, 
			Mathf.Sin (Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f), 
			0.0f);

		// turn towards the player when it gets close and itself is higher than the cube player is on
//		if (distToPlayer <= 5f 
////			&& distToPlayer >= 1f
//			&& player.transform.position.y - transform.position.y <= 1.15f 
//		) {
//			transform.RotateAround (
//				posOri, 
//				//Vector3.one, 
//				(player.transform.position - transform.position - new Vector3(90,0,0)) /* Mathf.Pow(-1, Mathf.Floor(Time.time))*/,
//				5f * Time.deltaTime * Mathf.Sin (Time.time /*+transform.position.x + transform.position.z */)
//			);
			//trying to use
			// transform.up
			// vector3.up
			// lerp
			// vector3.angelAxis
//			transform.up = new Vector3(
//				player.transform.position.x - transform.position.x,
//				1,
//				player.transform.position.z - transform.position.z
//			) * 10f;


			// something more cool but not usefull
//						Quaternion rotation = Quaternion.LookRotation( 
//							new Vector3(
//								(player.transform.position.x - transform.position.x), 
//								transform.position.y * 0.01f,
//								(player.transform.position.z - transform.position.z)
//							)
//						);	

//			transform.rotation = rotation;

			// a cool effect that's not helping
//			transform.RotateAround (
//				posOri, 
//				player.transform.position - transform.position,
//				2.5f * Time.deltaTime
//			);

////			 another cool but not helping thing
//						Quaternion rotation = Quaternion.LookRotation( player.transform.position - transform.position);
//			
//						transform.rotation = rotation;

			// something more cool but not usefull
//			Quaternion rotation = Quaternion.LookRotation( 
//				new Vector3(
//					(player.transform.position.x - transform.position.x) * 0.1f, 
//					transform.position.y,
//					(player.transform.position.z - transform.position.z) * 0.1f
//				)
//			);			
//			transform.rotation = rotation;

//		} else {
			transform.RotateAround (
				posOri, 
			//Vector3.one, 
				Vector3.forward/* Mathf.Pow(-1, Mathf.Floor(Time.time))*/,
				2.5f * Time.deltaTime * Mathf.Cos (Time.time /*+transform.position.x + transform.position.z */)
			);
//		}
		
		//transform.position = posOri + heightAdjust;

		//rb.MovePosition (posOri + heightAdjust);//
		transform.position = posOri + heightAdjust;
	}
//
//	void FixUpdate(){
//		rb.MovePosition(transform.position + transform.up * Time.deltaTime);
//	}
}
