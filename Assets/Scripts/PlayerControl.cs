using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	float lightAmplitudeControl = 0.3f;
	float oriLtRange;
	Light playerLight;
	public GameObject playerShell;
	GameObject cube;
	float distToCubeOri;
	Vector3 cubePos;
	bool cubeFound = false;
	
	// Use this for initialization
	void Start () 
	{
		playerLight = GetComponent<Light>();
		oriLtRange = playerLight.range;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (cubeFound == false){
			cube = GameObject.Find ("Cube");
			Debug.Log (cube.transform.position);
			cubePos = cube.transform.position;
			distToCubeOri = Vector3.Distance (transform.position, cubePos);

			cubeFound = true;
		}

		float amplitude = (Mathf.Sin (2.0f * Time.time) + 1.0f) * lightAmplitudeControl;	
		float distToCube = Vector3.Distance (transform.position, cubePos);

		playerLight.range = oriLtRange * (1f + 0.15f * amplitude + 0.07f * (distToCubeOri - distToCube));
	}
}

