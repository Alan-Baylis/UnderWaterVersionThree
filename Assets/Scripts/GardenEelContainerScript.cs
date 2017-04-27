using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEelContainerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player"){
			gameObject.GetComponentInChildren<EelControl> ().EelRaise ();
		}
	}

//	void OnCollisionExit(Collision other){
//		if (other.gameObject.tag == "Player"){
//			gameObject.GetComponentInChildren<EelControl> ().EelDown ();
//		}
//	}
}
