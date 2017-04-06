using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour {
	private Vector3 heightAdjust;
	private Vector3 posOri;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// raycasting way of floating
		Ray floatRay = new Ray(transform.position, Vector3.down * 10);
		RaycastHit floatRayhit;

		if (Physics.Raycast (floatRay, out floatRayhit, 10f)) {
			// get the game object's y, change itself to make itself float
			if (floatRayhit.collider.gameObject.tag == "GroundCube"){
				Vector3 groundCubePos;
				Vector3 temp = transform.position;
				groundCubePos = floatRayhit.collider.gameObject.transform.position;
				transform.position = Vector3.Lerp (temp,new Vector3 (temp.x, groundCubePos.y + 2.2f, temp.z),0.1f);
			}
		}

		transform.Rotate (new Vector3 (45, 15, 30) * Time.deltaTime);
	}
}
