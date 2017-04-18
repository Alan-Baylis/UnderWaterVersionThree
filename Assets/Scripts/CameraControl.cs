using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraControl : MonoBehaviour {


	Image titleText;
	Text instructionText;
	public GameObject player;

	private Vector3 offset;

//	public float MoveSpeed = 10f;

	private Vector3 playerVelocity;

	public float minAngle;

	public float maxAngle;

	public float maxDistance;

	public float minDistance;

	public float cameraCorrectionSpeed;

	private bool begun;

	float arcPoint;
	
	// Use this for initialization
	IEnumerator Start () 
	{
		titleText = GameObject.Find ("TitleImage").GetComponent<Image> ();
		instructionText = GameObject.Find ("startText").GetComponent<Text> ();
		begun = false;
		player = GameObject.Find ("PlayerShell");
		offset = transform.position - player.transform.position;
		while(!Input.anyKey) {
			yield return null;
		}
		StartCoroutine (FadeOutText ());
		StartCoroutine(GameStartAmplitude());
		begun = true;
//		playerVelocity = new Vector3 (0, 0, 10);
	}

//  // make camera rotate with player when they turn
//	void FixedUpdate(){
//		Rigidbody playerRB = player.transform.parent.gameObject.GetComponent<Rigidbody> ();
//		float blendAmount = 0.01f * Mathf.Clamp (playerRB.velocity.magnitude, 0.0f, 1.0f);
//		// velocity = Get the world-space speed
//		// magnitude = Returns the length of this vector
//
//		Vector3 rbVelocity = playerRB.velocity;
//		rbVelocity.y = 0; //because we don't want camera to go above player
//
//		playerVelocity = Vector3.Lerp (playerVelocity, rbVelocity, blendAmount); 
//		//that just makes a vector that is 90% playerVelocity and 10% playerRB.velocity
//		//so it slowly follows playerVelocity
//	}

	// late update is run after all update functions

	void Update(){
		//transform.position = player.transform.position + offset;

//		transform.position = Vector3.Lerp (transform.position, player.transform.position + offset, MoveSpeed * Time.deltaTime);
	}

	void LateUpdate () 
	{ 
		if(begun) {

			Vector3 newPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
			if(Vector3.Distance(transform.position, newPos) > maxDistance){
				transform.position = Vector3.Slerp(transform.position, newPos, 0.01f);
			}
			else if(Vector3.Distance(transform.position, newPos) < minDistance){
				transform.position = Vector3.SlerpUnclamped(newPos, transform.position, 1.005f);
			}
			RaycastHit hit;
			Ray shootingRay = new Ray(transform.position, Vector3.down);
			if(Physics.Raycast(shootingRay, out hit, 100)) {
				Vector3 verticalVector = new Vector3(transform.position.x, hit.point.y, transform.position.z);
				if(GroundSinControl.CalculateSinPosition(transform.position) + 2.4f > transform.position.y) {
					transform.position = Vector3.SlerpUnclamped(verticalVector, transform.position, 1.005f);
				}
				else if(Vector3.Angle(Vector3.down, player.transform.position - transform.position) < minAngle) {
					transform.position = Vector3.Slerp(transform.position, verticalVector, cameraCorrectionSpeed);
				}
				else if(Vector3.Angle(Vector3.down, player.transform.position - transform.position) > maxAngle){
					transform.position = Vector3.SlerpUnclamped(verticalVector, transform.position, 1 + cameraCorrectionSpeed);
				}
			}
/*			Vector3 anchorPos = RotatePointAroundPivot(player.transform.position, player.transform.position + (Vector3.up * 2f), 90 * Time.deltaTime);
			if(Vector3.Distance(transform.position, player.transform.position) > 2) {
				transform.position = Vector3.Slerp(transform.position, anchorPos, 0.01f);
			} 
			else if(Vector3.Distance(transform.position, player.transform.position) > 1f) {
				transform.position = Vector3.Slerp(anchorPos, transform.position, 0.01f);
			}
			Debug.Log(Vector3.Distance(transform.position, player.transform.position) );*/
			Vector3 curRotation = player.transform.rotation.eulerAngles;
			transform.rotation = Quaternion.Euler(0, curRotation.y, 0);
			transform.LookAt(player.transform);
		}
		//transform.position = player.transform.position + offset;
////
  // camera rotating part 2, commend out the "+ offset" line and use below
//			+ (playerVelocity.normalized * -1f) + new Vector3(0,0.1f,0); 
			//move the camera behind the player
//		transform.LookAt (player.transform);
	}

	IEnumerator FadeOutText() {
		Color startColorTitle = titleText.color;
		Color endColorTitle = startColorTitle;
		endColorTitle.a = 0;

		Color startColorInstruction = instructionText.color;
		Color endColorInstruction = startColorInstruction;
		endColorInstruction.a = 0;

		for (float t = 0; t <= 1; t += 0.5f*Time.deltaTime) {
			titleText.color = Color.Lerp (startColorTitle, endColorTitle, t);
			instructionText.color = Color.Lerp (startColorInstruction, endColorInstruction, t);
			yield return null;
		}

		Destroy (titleText.gameObject);
		Destroy (instructionText.gameObject);
	}

	IEnumerator GameStartAmplitude() {
		float startAmplitude = GroundSinControl.amplitudeModifier;
		float lerpTime = 2;
		for(float t = 0; t <= lerpTime; t += Time.deltaTime) {
			GroundSinControl.amplitudeModifier = Mathf.SmoothStep(startAmplitude, 1, t / lerpTime); 
			yield return null;
		}
	}

	Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, float angle) {
		Vector2 offset = new Vector2(pivot.z, pivot.y);
		Vector2 offsetPoint = new Vector2(point.z, point.y) - offset;
		float cosVal = Mathf.Cos(angle * Mathf.Deg2Rad);
		float sinVal = Mathf.Sin(angle * Mathf.Deg2Rad);
		Vector2 rotatedPoint = new Vector2(offsetPoint.x * cosVal - offsetPoint.y * sinVal, offsetPoint.x * sinVal + offsetPoint.y * cosVal);
		rotatedPoint += offset;
		return new Vector3(point.x, rotatedPoint.y, rotatedPoint.x);
	}
}
