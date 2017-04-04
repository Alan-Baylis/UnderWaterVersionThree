using UnityEngine;
using System.Collections;

public class waterDeformDemo : MonoBehaviour {

	MeshFilter mf; // used by Unity as a "model picker"
	Vector3[] unmodifiedMesh;
	public float amplitude = 1;
	public float frequency = 1;
	// Use this for initialization
	void Start () {
		mf = GetComponent<MeshFilter>();

		// optimizes a bit
		mf.mesh.MarkDynamic();

		// save a copy before distorting it
		unmodifiedMesh = mf.mesh.vertices.Clone() as Vector3[];
	}
	
	// Update is called once per frame
	void Update () {
		// Start with blank copy
		Vector3[] vertices = unmodifiedMesh.Clone() as Vector3[];

		for(int i = 0; i < vertices.Length; i++){
			vertices [i] += Vector3.up * (Mathf.Sin (Time.time * frequency + i) * amplitude);
		}

		// put the vertices back into the mesh
		mf.mesh.vertices = vertices;
		mf.mesh.RecalculateNormals();

		// Determines if this is within culling plane
		mf.mesh.RecalculateBounds();

		// put the new mesh into the MeshCollider
		GetComponent<MeshCollider>().sharedMesh = mf.mesh;
	}
}
