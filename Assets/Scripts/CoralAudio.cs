using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralAudio : MonoBehaviour {

	private AudioSource bubbleUp;
	private int i;
	private float distTP;
//	private float distTPori;
	private GameObject player;

	// Use this for initialization
	void Start () {
		bubbleUp = GetComponent <AudioSource> ();
		player = GameObject.Find ("PlayerShell");
//		distTPori = Vector3.Distance(transform.position, player.transform.position);

	}
	
	// Update is called once per frame
	void Update () {

		distTP = Vector3.Distance(transform.position, player.transform.position);

		i = Random.Range (1, 700);
		if (i <= 2 ) {
			bubbleUp.volume = Mathf.Clamp01(distTP) * 0.25f;
			bubbleUp.Play();
		} 
			
	}
}
