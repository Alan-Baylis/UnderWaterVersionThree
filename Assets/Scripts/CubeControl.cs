using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour {
	private Vector3 heightAdjust;
	private Vector3 posOri;

	// Use this for initialization
	void Start () 
	{
		posOri = transform.position;
//		Debug.Log (posOri);
	}
	
	// Update is called once per frame
	void Update () 
	{
//		// raycasting way of floating
//		Ray floatRay = new Ray(transform.position, Vector3.down * 10);
//		RaycastHit floatRayhit;
//
//		if (Physics.Raycast (floatRay, out floatRayhit, 10f)) {
//			// get the game object's y, change itself to make itself float
//			if (floatRayhit.collider.gameObject.tag == "GroundCube"){
//				Vector3 groundCubePos;
//				Vector3 temp = transform.position;
//				groundCubePos = floatRayhit.collider.gameObject.transform.position;
//				transform.position = Vector3.Lerp (temp,new Vector3 (temp.x, groundCubePos.y + 2.2f, temp.z),0.1f);
//			}
//		}

		heightAdjust = new Vector3 (
			0.0f, 
			Mathf.Sin (Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f), 
			0.0f);

//		transform.RotateAround (
//			posOri, 
//			Vector3.forward,
//			2.5f * Time.deltaTime * Mathf.Cos (Time.time)
//		);

		transform.Rotate (new Vector3 (45, 15, 30) * Time.deltaTime);
		transform.position = posOri + heightAdjust + new Vector3(0,2.2f,0);
	}
}
