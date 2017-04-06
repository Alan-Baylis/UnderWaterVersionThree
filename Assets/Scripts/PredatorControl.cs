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
//	bool isShaking = false;
	bool attacking = false;

	public Material predatorMat;
	public Material predatorAttMat;

	private Vector3 heightAdjust;
	private Vector3 posOri;

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
	}

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
		if(other.gameObject.tag == "Predator")
		{
			GameObject.Destroy (other.gameObject);
		}
	}
}
