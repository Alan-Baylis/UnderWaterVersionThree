using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackCurtainControl : MonoBehaviour {

	private Color black;
	private Color trans;
	private Color currentColor;

	private Image curtainImage;

	public bool gameEnded = false;
	private bool fading = false;
	public Text endText;

	// Use this for initialization
	void Start () {
		black = Color.black;
		trans = new Color (0, 0, 0, 0);
		curtainImage = gameObject.GetComponent<Image> ();
		currentColor = curtainImage.color;
		endText.GetComponent<Text> ().color = trans;
		StartCoroutine (FadeToColor (trans, 2));
	}
	
	// Update is called once per frame
	void Update () {
		curtainImage.color = currentColor;

		if (gameEnded && !fading) {
			if (Input.anyKeyDown) {
				Application.LoadLevel ("GM-level1");
			}
		}
	}

	public IEnumerator FadeToColor(Color newColor, float duration) {
		fading = true;
		float t = 0;
		Color baseColor = currentColor;
		while (t < duration) {
			currentColor = Color.Lerp (baseColor, newColor, t / duration);
			t += Time.deltaTime;
			yield return null;
		}
		currentColor = newColor;
		fading = false;
	}

	public void EndGame(Color deathColor) {
		if(!fading && !gameEnded)
		StartCoroutine (FadeToColor (deathColor, 2));
		endText.GetComponent<Text> ().color = Color.gray;
		gameEnded = true;
	}
}
