using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BottomFeederControl : MonoBehaviour {

	public GameObject player;
	public float predatorMF;
	public float attackDist;
	public float escapeDist;

	public float driftForce;

	float distToPlayer;

	Rigidbody rigidBody;
	bool attacking = false;
	public bool readyToAttack = true;


	private float attackRest=0;

	// Use this for initialization
	void Start (){
		player = GameObject.Find ("PlayerShell");
		rigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		distToPlayer = Vector3.Distance(player.transform.position, transform.position);

		if (distToPlayer > attackDist && attacking == false){
			Idle ();
		}

		if (distToPlayer < attackDist && readyToAttack == true){
			Attack();
			attacking = true;
	     }

		if (distToPlayer > escapeDist || readyToAttack == false){
			StopAttack ();
			attacking = false;
		}
	}

	// fake as exist when player is far
	void Idle(){
		rigidBody.AddForce(new Vector3(Mathf.Sin(Time.time) * Random.value, 0, Mathf.Sin(Time.time) * Random.value) * driftForce);
	}

	// attack when player is close, change apperance (un-recoverable)
	void Attack(){
		Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
		directionToPlayer.y = 0;
		rigidBody.AddForce(directionToPlayer * predatorMF);
	}	

	// stop attack when player escape (far enough)
	void StopAttack(){

		if (attackRest <= 2) {
			attackRest += Time.deltaTime;
			readyToAttack = false;
		} else if (attackRest > 2){
			readyToAttack = true;
			attackRest = 0;
			gameObject.tag = "BottomFeeder";
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Predator")
		{
			GameObject.Destroy (other.gameObject);
		}
	}
}
