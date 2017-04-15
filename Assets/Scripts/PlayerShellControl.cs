﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerShellControl : MonoBehaviour {
	public float speed = 370; 
	public float floatingControl;
	public float speedDown;
	public int touchCount;
	public float shakeTimer;
	public int health = 6;
	public Vector3 movement;
	public GameObject player;
	public GameObject cubePlayerIsOn;
	private Vector3 forwardVector;

	private Rigidbody rb;
	private JellyMesh jellyMesh;
	private Color black;


	public bool hasWon = false;
	public bool hasLost = false;
	public Color white = Color.white;

	// Use this for initialization
	void Start () 
	{
		forwardVector = transform.forward;
		black = Color.black;
		jellyMesh = GetComponent<JellyMesh>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// if fall off the platform, player lose
		if (transform.position.y < -5)
		{
			if (hasWon != true)
			{
				GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.black);
			}
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 forwardProjection = transform.position + (forwardVector * 2);
		float projectedSlope = Mathf.Sin (Time.time - Time.deltaTime - forwardProjection.x * 0.2f - forwardProjection.z * 0.2f);
		float hillModifier;
		if(Mathf.Sin (Time.time - transform.position.x * 0.2f - transform.position.z * 0.2f) > projectedSlope){
			hillModifier = 1.5f;
		}
		else {
			hillModifier = 0.95f;
		}
		forwardVector = Quaternion.AngleAxis(moveHorizontal, Vector3.up) * forwardVector;
		movement = forwardVector * moveVertical;
		jellyMesh.AddForce(movement * speed,true);

		Camera.main.transform.RotateAround(transform.position, Vector3.up, moveHorizontal);
	}
		
	void OnJellyCollisionEnter(JellyMesh.JellyCollision collision)
	{
		if(collision.Collision.gameObject.tag == "WhiteCube")
		{
			Debug.Log ("collison with white cube");
			hasWon = true;
			GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.white);
		}

		if(collision.Collision.gameObject.tag == "Predator" || collision.Collision.gameObject.tag == "PredatorStraight")
		{
			SubtractLife(1);
			Destroy(collision.Collision.gameObject);
			collision.Collision.gameObject.tag = "Finish";
		}
		if(collision.Collision.gameObject.tag == "HealthUp") {
			AddLife(1);
			Destroy(collision.Collision.gameObject);
			collision.Collision.gameObject.tag = "Finish";
		}
//		if (collision.Collision.gameObject.tag == "GroundCube") {
//			cubePlayerIsOn = collision.Collision.gameObject;
//		
//		}
	}

	public void AddLife(int amount) {
		health += amount;
		Debug.Log("Gained " + amount.ToString() + " life");
	}

	public void SubtractLife(int amount) {
		health -= amount;
		Debug.Log("Lost " + amount.ToString() + " life");
		Debug.Log("Life Amount " + health.ToString());
		if(health <= 0 && hasWon == false) {
			GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.red);
		}
	} 
}
