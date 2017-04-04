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
//	bool minusAlready = false;
//	bool isShaking = false;
	bool attacking = false;

	public Material predatorStraightMat;
//	public Material predatorAttMat;

	private Vector3 heightAdjust;
	public Vector3 tempPos;

	// Use this for initialization
	void Start (){

		tempPos = transform.position;

		player = GameObject.Find ("PlayerShell");
		gameMessager = GameObject.Find ("GameMessager");
		rbPredator = GetComponent<Rigidbody> ();

		float scale = Random.Range (0.8f, 1.2f);
		transform.localScale = new Vector3(scale,scale,scale);
//		this.gameObject.GetComponent<Renderer> ().material = predatorMat;
	}
	
	// Update is called once per frame
	void Update () {

		distToPlayer = Vector3.Distance(player.transform.position, transform.position);

//		tempPos = transform.position;
//
//		heightAdjust = new Vector3 (
//			0.0f, 
//			Mathf.Sin (Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f), 
//			0.0f);

		// attack when close
		if (distToPlayer < attackDist){
			Attack();
			attacking = true;
	     }

		// stop if far enough
		if (distToPlayer > escapeDist){
//			StopAttack ();
			attacking = false;
		}
			

		// idle movement -  float on wave
		if (attacking == false){
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

		transform.LookAt (player.transform);
	}


	// attack when player is close, change apperance (un-recoverable)
	void Attack(){
		// move to player
//		transform.Rotate (new Vector3 (45, 15, 30) * 5 * Time.deltaTime);
		Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
		directionToPlayer.y = 0;
		rbPredator.AddForce(directionToPlayer /* transform.forward */ * predatorMF);
//		this.gameObject.GetComponent<Renderer> ().material = predatorAttMat;
	}	

	// stop attack when player escape (far enough)
//	void StopAttack(){
//	}

//	IEnumerator ShakeRoutine(){
//		isShaking = true;
//		ShakeCamera sc= GameObject.Find ("GameMaster").GetComponent<ShakeCamera> ();
//		sc.enabled = true;
//		yield return new WaitForSeconds (1f);
//		sc.enabled = false;
//		isShaking = false;
//	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "PredatorStraight")
		{
			if (other.transform.localScale.x <= transform.localScale.x) {

				transform.localScale = new Vector3 (2*transform.localScale.x-other.transform.localScale.x,
					2*transform.localScale.y-other.transform.localScale.y,
					2*transform.localScale.z-other.transform.localScale.z
				);

				GameObject.Destroy (other.gameObject);

			} 
		}
	}
}
