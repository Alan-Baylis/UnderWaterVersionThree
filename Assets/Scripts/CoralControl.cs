// a thing slows player down --> poison and paralyze?

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoralControl : MonoBehaviour {

	//public 	Light playerLight;
	public GameObject player;
	public GameObject gameMessager;

	public float speedOri;
	public float reactDist;
	public float speedDownRate;
	public float lowestSpeed;
	private float distToPlayer;

	bool inControll = false;

	// Use this for initialization
	void Start () 
	{
		speedOri = player.GetComponent<PlayerShellControl> ().speed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// if player is close, slow it down 
		distToPlayer = Vector3.Distance (player.transform.position, transform.position);

		if (distToPlayer < reactDist * 2) {
			inControll = true;
		} else {
			inControll = false;
		}

		if (inControll == true) {
			if (distToPlayer <= reactDist) {
				SpeedDown ();
				gameMessager.GetComponent<Text> ().text = "in coral zone";
			} else {
				RecoverSpeed();
				gameMessager.GetComponent<Text> ().text = "speed recovering";
			}
		}

		//Debug.Log (player.GetComponent<PlayerShellControl> ().speed);
	}

	void SpeedDown(){
		if (player.GetComponent<PlayerShellControl> ().speed > lowestSpeed) {
			player.GetComponent<PlayerShellControl> ().speed -= speedDownRate;
		} 

		else {
			player.GetComponent<PlayerShellControl> ().speed = lowestSpeed;
			gameMessager.GetComponent<Text> ().text = "speed low";
		}
	}

	void RecoverSpeed(){
		if (player.GetComponent<PlayerShellControl> ().speed < speedOri){
			player.GetComponent<PlayerShellControl> ().speed += speedDownRate;
		}

		else {
			player.GetComponent<PlayerShellControl> ().speed = speedOri;
		}
	}
}
