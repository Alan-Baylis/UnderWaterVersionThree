using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SardinHolderControl : MonoBehaviour {

	Vector3 posOri;
	Vector3 posTemp;
	Vector3 heightAdjust;
	Vector3 playerTemp;

	GameObject player;
	JellyMesh playerMesh;

	public float pushSpeed;
	public float maxHeight;

	float dist;
	float touchTime = 0.0f;

	// Use this for initialization
	void Start () {
		posOri = transform.position;
		player = GameObject.Find ("PlayerShell");
		playerMesh = player.GetComponent<JellyMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		heightAdjust = new Vector3 (
			0.0f, 
			Mathf.Abs(Mathf.Sin (Time.time /*- transform.position.x * 0.2f - transform.position.z * 0.2f*/)), 
			0.0f);
		
//		RotateAround();

		transform.position = posOri + heightAdjust;

		dist = Vector3.Distance (posOri, player.transform.position);
		if (dist < 2 && player.transform.position.y - transform.position.y < maxHeight) {
			playerMesh.AddForce(new Vector3(0, pushSpeed, 0), false);
//			player.transform.position = playerTemp + new Vector3(0, pushSpeed * Time.deltaTime, 0);
			/*if (touchTime >= 0 && touchTime <= 0) {
				touchTime = Time.time;
			}
			if ( Mathf.Sin(Time.time - touchTime) >= 0 ){
				playerTemp = player.transform.position;
				player.transform.position = 
					playerTemp + 
					new Vector3 (0, 0.8f * Mathf.Sin(Time.time - touchTime),0);
			}*/

		}
	}

	void RotateAround(){

		transform.RotateAround
		( 
			posOri, 
			Vector3.left, 
			20 * Time.deltaTime * (Mathf.Sin(Time.time)+1)
		); 
	}
}
