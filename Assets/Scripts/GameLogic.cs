// Special thanks to Lily, who changed the perspective into player's and made this game much better
// Special thanks to Bennett and Wyett for code help
// Special thanks to Sam who worked on anything I couldn't solve
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
	
	public int predatorAmount;
	public float cubeDistMin = 10.0f;
	public float cubeDistMax = 14.0f;
	public float predatorDist = 3.0f;
	public float colorChangeRate = 0.01f;
	public float whiteAlpha;
	public float blackAlpha;

	public GameObject player;
	public GameObject cube;
	public GameObject predator;
	public GameObject predatorPrefab;
	//public GameObject follower;
	//public GameObject followerPrefab;
	public GameObject white;
	public GameObject black; 
	public GameObject gameMessager;
	public GameObject whiteScreen;

	public Color winColor;
	public Color loseColor;
	public Color grayText;
	public Color lastEndColor; 
	
	public bool hasPlayedSound = false;
	public bool endGameReached = false;
	public bool RestartAllowed = false;
	public bool toReset = false;

	public Material predatorMat;

	private int i = 1;

	// Use this for initialization
	void Start () 
	{	
		predatorMat.color = new Vector4 (1,1,1,1);

		// make WhiteCube 
		player = GameObject.Find("Player");

		cube = Instantiate (Resources.Load("Cube")) as GameObject;
		makeCube ();

	// iniciating the win / lose scene, transparent before end game
		winColor = new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
		loseColor = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
		grayText = new Vector4 (0.5f, 0.5f, 0.5f, 0.0f);

		//load the old colors
		whiteAlpha = PlayerPrefs.GetFloat ("WhiteAlpha");
		blackAlpha = PlayerPrefs.GetFloat ("BlackAlpha");

	}
	
	// Update is called once per frame
	void Update () 
	{
		fadeIn ();

		if (i < predatorAmount) 
		{
			makePredator();
		}
		
		bool playerMoved = false;
		
		if (Input.anyKeyDown) 
		{
			playerMoved = true;
		}
	}

	// pick exist position, if the random position isn't right, draw another one and teleport the exist to it
	void makeCube(){ 
		Vector3 rndCubePos = new Vector3 (Random.Range(1.0f, 22.0f), 3.0f, Random.Range(1.0f, 11.0f));
		float dist = Vector3.Distance(player.transform.position, rndCubePos);
		
		if (dist > cubeDistMin && dist < cubeDistMax) 
		{
			cube.transform.position = rndCubePos;
			cube.name = "WhiteCube";
		} 
		else 
		{
			makeCube();
		}
	}

	// get into scene when start
	void fadeIn(){
		if (whiteAlpha > 0) {
			Image whiteImage = whiteScreen.GetComponent<Image> ();
			Color currentColor = whiteImage.color;
			whiteAlpha -= 0.016f;
			if (whiteAlpha < 0) whiteAlpha = 0;

			currentColor.a = whiteAlpha;
			whiteImage.color = currentColor;
		}

		if (blackAlpha > 0) {

			Image blackImage = black.GetComponent<Image> ();
			Color currentColor = blackImage.color;
			blackAlpha -= 0.016f;
			if (blackAlpha < 0) blackAlpha = 0;
			
			currentColor.a = blackAlpha;
			blackImage.color = currentColor;
		}
	}

	// pick predator position and put them in. Teleport when pos not good
	void makePredator(){ 
		Vector3 rndFollowerPos = new Vector3 (Random.Range(1.0f, 22.0f), 3.0f, Random.Range(1.0f, 10.0f));
		float dist = Vector3.Distance(player.transform.position, rndFollowerPos);

		if (dist > predatorDist) 
		{
			predator = Instantiate (predatorPrefab, rndFollowerPos, Quaternion.identity) as GameObject;
			i ++;
		}
	}

	public void Win(){
		Light cubeLight = GameObject.Find ("WhiteCube").GetComponent<Light> ();
		cubeLight.range += 0.1f;

		// Screen turn into white
		if (winColor.a < 1.01f) 
		{
			winColor.a += colorChangeRate;
		} 
		if (winColor.a >= 1f)
		{
			winColor.a = 1f;
			endGameReached = true;
		}
		
		Image whiteImage = whiteScreen.GetComponent<Image>();
		whiteImage.color = winColor;
		
		// allow player to enter a new game when the screen is generally black / white
		if (endGameReached == true) 
		{
			Text endText;
			endText = GameObject.Find ("EndText").GetComponent<Text>();
			grayText.a += colorChangeRate;
			endText.color = grayText;
			
			if (grayText.a >= 0.9f && toReset == false) 
			{
				toReset = true;
			}
		}
		
		// restart game when any key is pressed
		if (toReset == true && Input.anyKeyDown) 
		{
			PlayerPrefs.SetFloat ("BlackAlpha", 0);
			PlayerPrefs.SetFloat ("WhiteAlpha", 1f);
			Application.LoadLevel ("stage1");
		}
			
		// play winning sound, only once when called at the first time
		if (hasPlayedSound == false) 
		{ 
			GameObject.Find ("White").GetComponent<AudioSource> ().Play ();
			hasPlayedSound = true;
		}

		// slowly stop bgm
		GameObject.Find ("GameMaster").GetComponent<AudioSource> ().volume -= 0.05f;
	}

	public void Lose(){	
		// Screen turn into black
			if (loseColor.a < 1.01f) 
			{
				loseColor.a += colorChangeRate;
			} 
			if (loseColor.a >= 1f)
			{
				loseColor.a = 1f;
				endGameReached = true;
			}

		Image blackImage = GameObject.Find ("Black").GetComponent<Image>();
		blackImage.color = loseColor;

		// allow player to enter a new game when the screen is generally black / white
		if (endGameReached == true) 
		{
			Text endText;
			endText = GameObject.Find ("EndText").GetComponent<Text>();
			grayText.a += colorChangeRate;
			endText.color = grayText;
			
			if (grayText.a >= 0.9f && toReset == false) 
			{
					toReset = true;
			}
		}

		// restart game when any key is pressed
		if (toReset == true && Input.anyKeyDown) 
		{
			// playerPrefs is a great place to save anything! 
			// Infomation can be saved in here and then read
			// A nice place to save high scores~
			PlayerPrefs.SetFloat ("BlackAlpha", 1f); 
			PlayerPrefs.SetFloat ("WhiteAlpha", 0);
			Application.LoadLevel ("stage1");
		}
			
		// play winning sound, only once when called at the first time
		if (hasPlayedSound == false)
		{ 
			GameObject.Find ("Black").GetComponent<AudioSource>().Play();
			hasPlayedSound = true;
		}
			
		// slowly stop bgm
		GameObject.Find ("GameMaster").GetComponent<AudioSource> ().volume -= 0.05f;
	}
}