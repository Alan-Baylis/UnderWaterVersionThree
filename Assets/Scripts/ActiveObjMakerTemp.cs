using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjMakerTemp : MonoBehaviour {
	private GameObject player;
	private int i = 1;
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
	}
	
	// Update is called once per frame
	void Update () {
		
		if (i < limit) 
		{
			makeObject();
		}
			
	}

	// pick predator position and put them in. Teleport when pos not good
	void makeObject(){ 
		Vector3 rndObjPos = new Vector3 (Random.Range(rangeXmin, rangeXmax), 1.1f, Random.Range(rangeZmin, rangeZmax));
		float dist = Vector3.Distance(player.transform.position, rndObjPos);

		if (dist > distMin && dist < distMax) 
		{
			activeObj = Instantiate (objPrefab,rndObjPos, Quaternion.identity) as GameObject;
			i ++;
		}
	}
}
