using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjMakerTemp : MonoBehaviour {

	GameObject activeObj;
	public GameObject objPrefab;
	public int limit;
	public float rangeXmin;
	public float rangeXmax;
	public float rangeZmin;
	public float rangeZmax;

	// Use this for initialization
	void Start () {

		for(int i = 0; i < limit; i++) {
			makeObject();
		}
	}

	// pick predator position and put them in. Teleport when pos not good
	void makeObject(){ 
		Vector3 rndObjPos = new Vector3 (Random.Range(rangeXmin, rangeXmax), 1.1f, Random.Range(rangeZmin, rangeZmax));
		activeObj = Instantiate (objPrefab,rndObjPos, Quaternion.identity) as GameObject;
	}
}
