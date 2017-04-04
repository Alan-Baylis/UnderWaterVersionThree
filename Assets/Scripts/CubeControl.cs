using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour {

	//public 	Light playerLight;


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3 (45, 15, 30) * Time.deltaTime);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") {
			Debug.Log ("win");
		} 
	}
}
