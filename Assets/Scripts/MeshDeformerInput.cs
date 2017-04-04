 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformerInput : MonoBehaviour {
	
	public float force = 10f;

	public float forceOffset = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		//if (Input.GetMouseButton(0)) {
//			HandleInput();
//		//}
		Ray deformingRay = new Ray(transform.position + (Vector3.down * 5), Vector3.up * 10);

		RaycastHit hit;

		if (Physics.Raycast (deformingRay, out hit, 10f)) {
			Debug.Log (hit.collider.gameObject.name);
			MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer> ();

			if (deformer) {
				Vector3 point = hit.point;
				point += hit.normal * forceOffset;
				deformer.AddDeformingForce (point, force);
			}
		} else {
			Debug.DrawRay(transform.position + (Vector3.down * 5), Vector3.up * 10);
		}
	}

//	// cast a ray upward and if it hit something, deform that
//	void HandleInput () {
//		Ray deformingRay = new Ray(transform.position, transform.up);
//
////		Debug.DrawRay (deformingRay);
//
//		RaycastHit hit;
//
//		if (Physics.Raycast(deformingRay, out hit)) {
//			MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
//
//			if (deformer) {
//				Vector3 point = hit.point;
//				point += hit.normal * forceOffset;
//				deformer.AddDeformingForce(point, force);
//			}
//		}
//	}

}
