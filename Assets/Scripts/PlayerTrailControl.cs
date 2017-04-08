using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailControl : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerShell");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position;// - new Vector3 (0,0.1f,0);
	}
}
