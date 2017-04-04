using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundShifter : MonoBehaviour {

	Transform player;
	// Use this for initialization
	void Start () {
		player = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.identity;
	}

	void OnTriggerExit(Collider other) {
		if(other.tag == "GroundCube") {
			/*Vector3 adjustmentPosition = player.position - other.transform.position;
			adjustmentPosition.z = Mathf.Round(adjustmentPosition.z);
			other.GetComponent<GroundControl>().MoveCube(other.transform.position + (adjustmentPosition * 2));
			*/
			Transform oldParent = other.transform.parent;
			other.transform.SetParent(player);
			//Debug.Log(other.transform.localPosition);
			Vector3 newPos = other.transform.localPosition * -1;
			other.transform.localPosition = newPos;
			newPos = other.transform.position;
			other.GetComponent<GroundControl>().MoveCube(newPos);

			//Debug.Log(other.transform.localPosition);
			other.transform.SetParent(oldParent);
			//Debug.Break();
		}
	}
}
