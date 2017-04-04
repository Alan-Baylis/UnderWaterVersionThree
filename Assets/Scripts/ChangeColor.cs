using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeColor : MonoBehaviour {

//	float currentTime;
	public bool change;
	//public GameObject numberSize;
	public Vector3 sizeControl;
	public Vector3 oriPos;
	Text selfText;

	// Use this for initialization
	void Start () 
	{
		selfText = GetComponent<Text> ();
		change = false;
		//sizeControl = numberSize.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (change) 
		{
			selfText.color -= new Color(0,0,0,0.01f);
			transform.parent.localScale += new Vector3 (0.01f, 0.01f, 0.01f);
			transform.parent.position += new Vector3 (0.0f, 0.8f, 0.0f);	
			if(selfText.color.a <=0)
			{
				change = false;
			}
		}
	}

	public void ChangeSelfColor()
	{
		selfText.color = Color.white;
		change = true;
	}
}
