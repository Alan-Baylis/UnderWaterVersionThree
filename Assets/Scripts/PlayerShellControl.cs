using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerShellControl : MonoBehaviour {
	public float speed; 
	public float floatingControl;
	public float speedDown;
	public int touchCount;
	public float shakeTimer;
	public Vector3 movement;
	public GameObject player;
//	public GameObject gameMessager;

	private Rigidbody rb;
	private JellyMesh jellyMesh;
	private Color black;

	public bool hasWon = false;
	public bool hasLost = false;
	public Color white = Color.white;
//	Light playerLight;

//	// light color related, for visualizing follower touching stuff
//	Vector4 lightColor;
//	Color touch0 = new Vector4 (0.0f, 1.0f, 1.0f, 1.0f);
//	Color touch1 = new Vector4 (0.3f, 0.6f, 0.6f, 1.0f);
//	Color touch2 = new Vector4 (0.6f, 0.3f, 0.3f, 1.0f);
//	Color touch3 = new Vector4 (1.0f, 0.0f, 0.0f, 1.0f);

	// Use this for initialization
	void Start () 
	{
		black = Color.black;
	//	rb = GetComponent<Rigidbody>();
//		playerLight = GetComponentInChildren<Light>();
//		lightColor = playerLight.color;
		jellyMesh = GetComponent<JellyMesh>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (hasWon == true) 
		{
			GameObject.Find ("GameMaster").GetComponent<GameLogic> ().Win();
		}

		if (hasLost == true) 
		{
			GameObject.Find ("GameMaster").GetComponent<GameLogic> ().Lose();
		}

		// if fall off the platform, player lose
		if (transform.position.y < -5)
		{
			if (hasWon != true)
			{
				//GameObject.Find ("GameMaster").GetComponent<GameLogic> ().Lose();
				GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.black);
			}
		}
	}
	
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		movement = Camera.main.transform.forward * moveVertical;//new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//rb.AddForce (movement * speed);

	//	float moveTorque = 800f;
//		if (jellyMesh != null) {
	//		jellyMesh.AddTorque (new Vector3 (moveVertical * moveTorque, moveHorizontal * moveTorque, 0), false); //try to rotate around x axis
			jellyMesh.AddForce(movement * speed,true);
//		}
		Camera.main.transform.RotateAround(transform.position, Vector3.up, moveHorizontal);
		//trying camera, not working : (
//		rb.transform.position += camera.transform.forward * Time.deltaTime * moveVertical;
//		rb.transform.Rotate (0f, moveHorizontal, 0f);
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "WhiteCube")
		{
			hasWon = true;
			GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.white);
		}

		if(other.gameObject.tag == "Predator" || other.gameObject.tag == "PredatorStraight" && hasWon == false)
		{
//			gameMessager.GetComponent<Text> ().text = "You got eaten!";
			//hasLost = true;
			GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.red);
		}
	}

//	// change OnCollisionEnter to this one in order to use the softbody physic
//	void OnJellyCollisionEnter(JellyMesh.JellyCollision collision)
//	{
//		if(collision.gameObject.tag == "WhiteCube")
//		{
//			hasWon = true;
//			GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.white);
//		}
//
//		if(collision.gameObject.tag == "Predator" || collision.gameObject.tag == "PredatorStraight" && hasWon == false)
//		{
//			//			gameMessager.GetComponent<Text> ().text = "You got eaten!";
//			//hasLost = true;
//			GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.red);
//		}
//	}

//	public void PredatorTouched(){
//		Debug.Log ("PredatorTouched!");
//	}

//	// when follower touched, reduce light and speed
//	public void followerTouched()
//	{
//		touchCount ++;
//
//		if (touchCount > 0 && touchCount <= 1) 
//		{
//			ChangeColor count4;
//			count4 = GameObject.Find ("3").GetComponent<ChangeColor>();
//			count4.ChangeSelfColor();
//			lightColor = Color.Lerp (touch0,touch1,Time.time);
//			playerLight.color = lightColor;
//		}
//
//		if (touchCount > 1 && touchCount <= 2) 
//		{
//			ChangeColor count3;
//			count3 = GameObject.Find ("2").GetComponent<ChangeColor>();
//			count3.ChangeSelfColor();
//			lightColor = Color.Lerp (touch1,touch2,Time.time);
//			playerLight.color = lightColor;
//		}
//
//		if (touchCount > 2 && touchCount <= 3) 
//		{
//			ChangeColor count2;
//			count2 = GameObject.Find ("1").GetComponent<ChangeColor>();
//			count2.ChangeSelfColor();
//			lightColor = Color.Lerp (touch2,touch3,Time.time);
//			playerLight.color = lightColor;
//		}
//
//		if (touchCount > 3 && touchCount <= 4) 
//		{
//			ChangeColor count1;
//			count1 = GameObject.Find ("0").GetComponent<ChangeColor>();
//			count1.ChangeSelfColor();
////			lightColor = Color.Lerp (touch2,Color.black,Time.time);
////			playerLight.color = lightColor;
//		}
//
//		// try to use a color changing code
//		if (speed > 0.0f && hasWon == false) 
//		{
//			//GameObject.Find ("Predator(Clone)").GetComponent<AudioSource> ().Play ();
//			speed -= speedDown;
//
//		} 
//
//		if (speed <= 0.0f) 
//		{
//			speed = 0.0f;
//
//			if (hasWon != true)
//			{
//				hasLost = true;
//			}
//		}
//	}
}
