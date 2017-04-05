using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PredatorStraightControl : MonoBehaviour {

	public GameObject player;
	public GameObject gameMessager;

	public float predatorMF;
	public float attackDist;
	public float escapeDist;
	float distToPlayer;

	Rigidbody rbPredator;
	private bool attacking = false;

	public Material predatorStraightMat;

	private Vector3 heightAdjust;
	private Vector3 tempPos;

	// Use this for initialization
	void Start (){

		tempPos = transform.position;

		player = GameObject.Find ("PlayerShell");
		gameMessager = GameObject.Find ("GameMessager");
		rbPredator = GetComponent<Rigidbody> ();

		float scale = Random.Range (0.8f, 1.2f);
		transform.localScale = new Vector3(scale,scale,scale);
	}
	
	// Update is called once per frame
	void Update () {

		distToPlayer = Vector3.Distance(player.transform.position, transform.position);

		// attack when close
		if (distToPlayer < attackDist){
			Attack();
			attacking = true;
	     }

		// stop if far enough
		if (distToPlayer > escapeDist){
			attacking = false;
			tempPos = transform.position;
		}
			
		// idle movement -  float on wave
		if (attacking == false){
			Idle ();
		}
	}

	void Idle(){
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
	}

	void Attack(){
		// lerp to player
		Vector3 temp;
		temp = transform.position;
		transform.position = Vector3.Lerp (temp, player.transform.position, 0.1f);
		transform.LookAt (player.transform);
	}	

// //shake camera
//	IEnumerator ShakeRoutine(){
//		isShaking = true;
//		ShakeCamera sc= GameObject.Find ("GameMaster").GetComponent<ShakeCamera> ();
//		sc.enabled = true;
//		yield return new WaitForSeconds (1f);
//		sc.enabled = false;
//		isShaking = false;
//	}

	// when predators touch each other, compare size, the smaller one got eaten
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "PredatorStraight")
		{
//			if (other.transform.localScale.x <= transform.localScale.x) {
//
//				transform.localScale = new Vector3 (2*transform.localScale.x-other.transform.localScale.x,
//					2*transform.localScale.y-other.transform.localScale.y,
//					2*transform.localScale.z-other.transform.localScale.z
//				);
		
		GameObject.Destroy (other.gameObject);
		}
	}
}
