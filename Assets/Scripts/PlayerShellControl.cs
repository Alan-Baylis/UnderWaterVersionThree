using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerShellControl : MonoBehaviour {
	public float speed = 370; 
	public AnimationCurve speedCurve;
	public float speedCurveIncrement;
	public float floatingControl;
	public float speedDown;
	public int touchCount;
	public float shakeTimer;
	public int health = 6;
	public float hitBouncePower = 600;
	public Vector3 movement;
	public GameObject player;
	public GameObject cubePlayerIsOn;
	private Vector3 forwardVector;

	private Rigidbody rb;
	private JellyMesh jellyMesh;

	private float speedCurveTime;
	public bool hasWon = false;
	public bool hasLost = false;
	public Color white = Color.white;

	public ParticleSystem hitParticle;

	// Use this for initialization
	void Start () 
	{
		forwardVector = transform.forward;
		jellyMesh = GetComponent<JellyMesh>();
		speedCurve.postWrapMode = WrapMode.ClampForever;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// if fall off the platform, player lose
		if (transform.position.y < -5)
		{
			if (hasWon != true)
			{
				StartCoroutine(GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().DelayedEndGame(Color.black, "", 2));
			}
		}

	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");


			if (moveHorizontal == 0 && moveVertical == 0) {
				speedCurveTime = 0;
			} 
			if (speedCurveTime <= 1) {
				speedCurveTime += speedCurveIncrement;
			}
	

		Vector3 forwardProjection = transform.position + (forwardVector * 2);
		float hillModifier;
		if(GroundSinControl.CalculateSinPosition(transform.position) > GroundSinControl.CalculateSinPosition(forwardProjection)){
			hillModifier = 1.1f;
		}
		else {
			hillModifier = 0.85f;
		}

		forwardVector = Quaternion.AngleAxis(moveHorizontal, Vector3.up) * forwardVector;
		movement = forwardVector * moveVertical;
		float currentSpeed  = speed * speedCurve.Evaluate (speedCurveTime);
		jellyMesh.AddForce(movement * currentSpeed * hillModifier,true); // added hillmodifier to here
		Camera.main.transform.RotateAround(transform.position, Vector3.up, moveHorizontal);
	}
		
	void OnJellyCollisionEnter(JellyMesh.JellyCollision collision)
	{
		if(collision.Collision.gameObject.tag == "WhiteCube")
		{
			hasWon = true;
			GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.white, "This is based on a nightmare I had as a little girl\nIt was really scary\n thank you for playing");
			Light cubeLight = GameObject.Find ("WhiteCube").GetComponent<Light> ();
			cubeLight.range += 0.1f;
		}

		if(collision.Collision.gameObject.tag == "Predator" || collision.Collision.gameObject.tag == "PredatorStraight")
		{
			Debug.Log(collision.Collision.transform.position - transform.position);
			StartCoroutine(HitBounce(collision.Collision.transform.position - transform.position));
			hitParticle.Play();
			SubtractLife(1);
			//Destroy(collision.Collision.gameObject);
			collision.Collision.gameObject.tag = "Finish";
			hitParticle.Stop ();
		}

		if(collision.Collision.gameObject.tag == "HealthUp") {
			AddLife(1);
			Destroy(collision.Collision.gameObject);
			collision.Collision.gameObject.tag = "Finish";
		}

		if (collision.Collision.gameObject.tag == "ShellFish") {
			collision.Collision.gameObject.GetComponentInChildren<ShellFishControl> ().ShellAction ();
			Debug.Log ("Calling Shell!");
		}

		if (collision.Collision.gameObject.tag == "ShellFishShell"){
			GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.red, "");
			Destroy (gameObject);
		}
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
			GameObject.Find ("blackCurtain").GetComponent<blackCurtainControl>().EndGame(Color.red, "");
		}
	} 

	IEnumerator HitBounce(Vector3 forceVector) {
		for(float t = 0; t < 0.3f; t += Time.deltaTime ) {
			jellyMesh.AddForce(forceVector * hitBouncePower, false);
			yield return null;
		}
	}
}
