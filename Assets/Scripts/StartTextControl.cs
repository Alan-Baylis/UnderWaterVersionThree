using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartTextControl : MonoBehaviour {
	
	public TextMesh startText;

	Color startTextColor;

	bool playerMoved = false;

	
	// Use this for initialization
	void Start () 
	{
		if (playerMoved != true) 
		{
			startText = GetComponent<TextMesh>();
			startTextColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{	
		Text startText;
		startText = GameObject.Find ("StartText").GetComponent<Text>();

		if (Input.anyKey) 
		{
			playerMoved = true;
		}

		if (playerMoved == true) 
		{
			startTextColor.a -= 0.03f;
			startText.color = startTextColor;
		}

	}

	void TextReset()
	{
		playerMoved = false;
	}
}
