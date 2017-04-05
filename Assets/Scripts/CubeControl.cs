using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3 (45, 15, 30) * Time.deltaTime);
	}
}
