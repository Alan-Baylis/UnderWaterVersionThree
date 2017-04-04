using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour {

	AudioSource bgm;
	// Use this for initialization
	void Start () {
		bgm = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		bgm.volume = Mathf.Sin (0.3f * Time.time)* Mathf.Sin(0.3f* Time.time);
	}
}
