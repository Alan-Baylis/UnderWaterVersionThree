using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour {

	AudioSource bgm;
	//PlayerShellControl shellControl;
	private GameObject player;
	// Use this for initialization
	void Start () {
		//shellControl = GameObject.Find("PlayerShell").GetComponent<PlayerShellControl>();
		bgm = GetComponent<AudioSource> ();
		player = GameObject.Find ("PlayerShell");
	}
	
	// Update is called once per frame
	void Update () {
		float val = 0;
//		if (shellControl.cubePlayerIsOn != null) {
//			val = 1 + shellControl.cubePlayerIsOn.transform.position.y;
//		}

		if (player != null) {
			val = 0.5f * player.transform.position.y;
		} 

		bgm.volume = val; //Mathf.Sin (0.2f * Time.time)* Mathf.Sin(0.2f* Time.time);
	}
}
