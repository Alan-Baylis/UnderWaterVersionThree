using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SardinHolderControl : MonoBehaviour {

	Vector3 posOri;
	GameObject player;
	JellyMesh playerMesh;
	public float pushSpeed;
	public float maxHeight;
	float dist;

	// Use this for initialization
	void Start () {
		posOri = transform.position;
		player = GameObject.Find ("PlayerShell");
		playerMesh = player.GetComponent<JellyMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		dist = Vector3.Distance (posOri, player.transform.position);
		if (dist < 1.1f && player.transform.position.y - transform.position.y < maxHeight) {
			playerMesh.AddForce(new Vector3(0, pushSpeed, 0), false);
		}
	}
}
