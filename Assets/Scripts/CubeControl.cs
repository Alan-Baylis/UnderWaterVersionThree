using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour {
	private Vector3 heightAdjust;
	private Vector3 posOri;

	// Use this for initialization
	void Start () 
	{
		posOri = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		heightAdjust = new Vector3 (
			0.0f, 
			GroundSinControl.CalculateSinPosition(transform.position),
			0.0f);

		transform.Rotate (new Vector3 (45, 15, 30) * Time.deltaTime);
		transform.position = posOri + heightAdjust;// + new Vector3(0,2.2f,0);
	}
}
