using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerpScript : MonoBehaviour {

	public Color colorOri;
	public Color colorTarget;
	public Color colorCurrent;

	// Use this for initialization
	void Start () {
		colorCurrent = Color.clear;

	}
	
	// Update is called once per frame
	void Update () {
		colorCurrent = Color.LerpUnclamped(colorOri,colorTarget,Mathf.Sin(Time.time));
		gameObject.GetComponent<Text> ().color = colorCurrent;
	}
}
