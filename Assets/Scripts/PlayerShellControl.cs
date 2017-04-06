using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerShellControl : MonoBehaviour {
	float speed = 370; 
	public float floatingControl;
	public float speedDown;
	public int touchCount;
	public float shakeTimer;
	public Vector3 movement;
	public GameObject player;

	private Rigidbody rb;
	private JellyMesh jellyMesh;
	private Color black;

	public bool hasWon = false;
	public bool hasLost = false;
	public Color white = Color.white;

	// Use this for initialization
	void Start () 
	{
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
		
		movement = Camera.main.transform.forward * moveVertical;

		jellyMesh.AddForce(movement * speed,true);

		Camera.main.transform.RotateAround(transform.position, Vector3.up, moveHorizontal);
	}
		
	void OnJellyCollisionEnter(JellyMesh.JellyCollision collision)
	{
		if(collision.Collision.gameObject.tag == "WhiteCube")
		{
			hasWon = true;
			GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.white);
		}

		if(collision.Collision.gameObject.tag == "Predator" || collision.Collision.gameObject.tag == "PredatorStraight" && hasWon == false)
		{
			GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.red);
		}
	}
}
