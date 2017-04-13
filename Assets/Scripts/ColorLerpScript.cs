using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerpScript : MonoBehaviour {

	public Color colorOri;
	public Color colorTarget;
	public Color colorCurrent;

	public float scaleMin;
	private Vector3 scaleOri;

	// Use this for initialization
	void Start () {
		colorCurrent = Color.clear;
		scaleOri = GetComponent<RectTransform> ().localScale;
	}
	
	// Update is called once per frame
	void Update () {
		colorCurrent = Color.LerpUnclamped(colorOri,colorTarget,Mathf.Sin(Time.time));
		gameObject.GetComponent<Text> ().color = colorCurrent;
		GetComponent<RectTransform> ().localScale = Vector3.LerpUnclamped (scaleOri,scaleOri * scaleMin,Mathf.Sin(Time.time));
	}
}
