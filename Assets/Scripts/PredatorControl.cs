using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PredatorControl : MonoBehaviour {

	public GameObject player;
	public GameObject gameMessager;

	public float predatorMF;
	public float attackDist;
	public float escapeDist;
	float distToPlayer;

	Rigidbody rbPredator;
	bool minusAlready = false;
	bool isShaking = false;
	bool attacking = false;

	public Material predatorMat;
	public Material predatorAttMat;

	// Use this for initialization
	void Start (){
		player = GameObject.Find ("PlayerShell");
		gameMessager = GameObject.Find ("GameMessager");
		rbPredator = GetComponent<Rigidbody> ();
		this.gameObject.GetComponent<Renderer> ().material = predatorMat;
	}
	
	// Update is called once per frame
	void Update () {

		distToPlayer = Vector3.Distance(player.transform.position, transform.position);

//		Debug.Log (distToPlayer);

		if (distToPlayer > attackDist && attacking == false){
			Idle ();
		}

		if (distToPlayer < attackDist){
			Attack();
			attacking = true;
	     }

		if (distToPlayer > escapeDist && attacking == true){
			StopAttack ();
			attacking = false;
		}
	}

	// fake as exist when player is far
	void Idle(){
		transform.Rotate (new Vector3 (45, 15, 30) * Time.deltaTime);
	}

	// attack when player is close, change apperance (un-recoverable)
	void Attack(){
		// move to player
		transform.Rotate (new Vector3 (45, 15, 30) * 5 * Time.deltaTime);
		Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
		directionToPlayer.y = 0;
		rbPredator.AddForce(directionToPlayer * predatorMF);

		// change color
		this.gameObject.GetComponent<Renderer> ().material = predatorAttMat;
	}	

	// stop attack when player escape (far enough)
	void StopAttack(){
		Debug.Log ("Escaped");
	}

	IEnumerator ShakeRoutine(){
		isShaking = true;
		ShakeCamera sc= GameObject.Find ("GameMaster").GetComponent<ShakeCamera> ();
		sc.enabled = true;
		yield return new WaitForSeconds (1f);
		sc.enabled = false;
		isShaking = false;
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Predator")
		{
			GameObject.Destroy (other.gameObject);
			Debug.Log ("they've killed each other!");
		}
//
//		if (other.gameObject.tag == "Player") {
//		} 
	}
}
