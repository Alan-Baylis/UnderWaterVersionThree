using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjMakerTemp : MonoBehaviour {

	private GameObject player;
	GameObject activeObj;
	public GameObject objPrefab;

	public int limit;

	public float rangeXmin;
	public float rangeXmax;
	public float rangeZmin;
	public float rangeZmax;
	public float distMin;
	public float distMax;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerShell");

		for(int i = 0; i < limit; i++) {
			makeObject();
		}
	}

	// pick predator position and put them in. Teleport when pos not good
	void makeObject(){ 
		Vector3 rndObjPos = new Vector3 (Random.Range(rangeXmin, rangeXmax), 1.1f, Random.Range(rangeZmin, rangeZmax));
		float dist = Vector3.Distance(player.transform.position, rndObjPos);
		while(dist < distMin || dist > distMax) {
			rndObjPos = new Vector3 (Random.Range(rangeXmin, rangeXmax), 1.1f, Random.Range(rangeZmin, rangeZmax));
			dist = Vector3.Distance(player.transform.position, rndObjPos);
		}
		activeObj = Instantiate (objPrefab,rndObjPos, Quaternion.identity) as GameObject;
	}
}
