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
	public float attackTime;
	float distToPlayer;

	Rigidbody rbPredator;
	private bool attacking = false;

	public Material predatorStraightMat;

	private Vector3 heightAdjust;
	private float verticalAttackOffset;

	public ParticleSystem attackParticle;
	public ParticleSystem waterParticle;


	// Use this for initialization
	void Start (){

		player = GameObject.Find ("PlayerShell");
		rbPredator = GetComponent<Rigidbody> ();

//		float scale = Random.Range (0.8f, 1.2f);
//		transform.localScale = new Vector3(scale,scale,scale);
		verticalAttackOffset = Resources.Load<GameObject>("GroundSinWave0.2Blue0").GetComponent<MeshFilter>().sharedMesh.bounds.size.y / 2;
		verticalAttackOffset += player.GetComponent<MeshFilter>().mesh.bounds.size.y;
	}
	
	// Update is called once per frame
	void Update () {

		distToPlayer = Vector3.Distance(player.transform.position, transform.position);

		// attack when close
		if (distToPlayer < attackDist && !attacking){
			StartCoroutine(Attack());
			attacking = true;
			attackParticle.Play ();

//			// check if hit a gardenEel
//			Ray stopRay = new Ray(transform.position, Vector3.forward * 1);
//			RaycastHit stopRayhit;
//			if (Physics.Raycast (stopRay, out stopRayhit, 10f)) {
//				if (stopRayhit.collider.gameObject.tag == "Eel"){
//					
//				}
//			}	
	     }

		// stop if far enough
		if (distToPlayer > escapeDist && attacking){
			attacking = false;
			attackParticle.Stop ();

		}
			
		// idle movement -  float on wave
		if (!attacking){
			Idle ();
		}
	}

	void Idle(){
		// raycasting way of floating
		float yPos = Mathf.Sin (Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f) + 2;
		Vector3 temp = transform.position;
		temp.y = yPos;
		if(Vector3.Distance(transform.position, temp) > 0.3f) {
			transform.position = Vector3.Lerp(transform.position, temp, 0.1f);
		}
}

	IEnumerator Attack(){
		Vector3 loomingPos = Vector3.Lerp(transform.position, player.transform.position, 0.6f);
		Vector3 startPosition = transform.position;
		loomingPos.y += 1.2f;
		for(float t = 0; t < 1; t += Time.deltaTime) {
			transform.LookAt(player.transform);
			transform.position = Vector3.Lerp(startPosition, loomingPos, t);
			yield return null;
		}
		while(attacking) {
		for(float t = 0; t < 3; t += Time.deltaTime) {
			transform.LookAt(player.transform);
			loomingPos = player.transform.position + new Vector3(0, 1.2f, 0);
			Vector3 temp = new Vector3(transform.position.x, loomingPos.y + GroundSinControl.CalculateSinPosition(transform.position) + verticalAttackOffset, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, temp, 0.1f);
			transform.RotateAround(loomingPos, Vector3.up, 20 * Time.deltaTime);
			yield return null;
		}
			transform.LookAt(player.transform);
			Vector3 divingVector = player.transform.position;
			divingVector.y = Mathf.Sin(Time.time + attackTime - Mathf.Floor(player.transform.position.x) * 0.2f - Mathf.Floor(player.transform.position.z) * 0.2f) + verticalAttackOffset;
			Vector3 startPos = transform.position;
			for(float t = 0; t < attackTime; t += Time.deltaTime) {
				transform.LookAt(divingVector);
				transform.position = Vector3.Slerp(startPos, divingVector, t / attackTime);
				yield return null;
			}
			divingVector = startPos + new Vector3(divingVector.x - startPos.x, startPos.y, divingVector.z);
			startPos = transform.position;
			for(float t = 0; t < attackTime; t += Time.deltaTime) {
				transform.LookAt(divingVector);
				transform.position = Vector3.Slerp(startPos, divingVector, t / attackTime);
				//transform.Translate(Vector3.Slerp(divingVector, divingVector * -1, t) * Time.deltaTime);
				yield return null;
			}
			gameObject.tag = "PredatorStraight";
			yield return new WaitForSeconds(2);
		}
		/*// lerp to player
		do {
			transform.LookAt (player.transform);
			Debug.Log("Attack!");
			Vector3 targetPosition = player.transform.position;
			startPosition = transform.position;
			//Debug.Log("AttackVector: " + attackVector.ToString());
			Ray heightRay = new Ray(startPosition, Vector3.down * 10);
			RaycastHit heightRayHit;
			float attackHeight = 0;
			if(Physics.Raycast(heightRay, out heightRayHit, 10)) {
				attackHeight = heightRayHit.distance;
			}
			for(float t = 0; t < 1; t += Time.deltaTime) {
				transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0, 1, t));
				if(Physics.Raycast(heightRay, out heightRayHit, attackHeight)) {
					if(heightRayHit.distance < attackHeight) {
						startPosition.y += 1f * Mathf.Clamp(attackHeight / heightRayHit.distance, 0, 1);
						heightRay = new Ray(startPosition, Vector3.down * 10);
					}
				}
				else if(Physics.Raycast(new Ray(transform.position, Vector3.down * 10), out heightRayHit, attackHeight)) {
					startPosition.y += (0.7f * Mathf.Clamp(attackHeight / heightRayHit.distance, 0, 1) * Time.deltaTime);
					heightRay = new Ray(startPosition, Vector3.down * 10);
				}

				yield return null;
			}
			Debug.Log("After attack " + (player.transform.position - transform.position).ToString());
			//Vector3 temp = transform.position;
			//transform.position = Vector3.Lerp (temp, player.transform.position, 0.1f);
			yield return new WaitForSeconds(2);
		} while(attacking);*/
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

		if(other.gameObject.tag == "GroundCube")
		{
			waterParticle.Play ();		
		}
	}

	void OnCollisionExit (Collision other) {
		if(other.gameObject.tag == "GroundCube")
		{
			waterParticle.Stop ();		
		}
	}

	void OnTriggerEnter (Collider other){

		if(other.gameObject.tag == "Eel")
		{
			attacking = false;
			attackParticle.Stop ();
		}
	}
}
