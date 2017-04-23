using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackCurtainControl : MonoBehaviour {

	private Color trans;
	private Color currentColor;

	private Image curtainImage;

	public bool gameEnded = false;
	private bool fading = false;

	public Text endText;
	public Text paragraphText;

	// Use this for initialization
	void Start () {
		trans = Color.clear;
		curtainImage = gameObject.GetComponent<Image> ();
		currentColor = curtainImage.color;
		endText.GetComponent<Text> ().color = trans;
		//StartCoroutine (FadeToColor (trans, 2));
	}
	
	// Update is called once per frame
	void Update () {
		curtainImage.color = currentColor;

		if (gameEnded && !fading) {
			if (Input.anyKeyDown) {
				GroundSinControl.amplitudeModifier = 0.1f;
				UnityEngine.SceneManagement.SceneManager.LoadScene("GM-level1");
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

	public void EndGame(Color deathColor, string textContent) {
		if(!fading && !gameEnded)
		StartCoroutine (FadeToColor (deathColor, 2));
		paragraphText.rectTransform.localPosition = new Vector3(0, 200, 0);
		StartCoroutine (FadeTextToColor(Color.gray, 1, 4));
		paragraphText.text = textContent;
		gameEnded = true;
	}

	IEnumerator FadeTextToColor(Color newColor, float duration, float delay) {
		yield return new WaitForSeconds(delay);
		Color start = endText.color;
		for(float t = 0; t < duration; t += Time.deltaTime) {
			endText.color = Color.Lerp(start, newColor, t / duration);
			paragraphText.color = Color.Lerp(start, newColor, t / duration);
			yield return null;
		}
	}

	public IEnumerator DelayedEndGame(Color deathColor, string textContent, float delay) {
		yield return new WaitForSeconds(delay);
		EndGame(deathColor, textContent);
	}
}
