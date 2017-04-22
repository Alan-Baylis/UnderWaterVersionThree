using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneCamera : MonoBehaviour {

	public Text introText;
	public float sceneChangeDelay;
	// Use this for initialization
	IEnumerator Start () {
		Debug.Log(Mathf.Sin(0));
		while(Time.time < sceneChangeDelay) {
			Debug.Log(Mathf.Sin(Time.time));
			Debug.Log(Time.time);
			yield return null;
		}
		//yield return new WaitForSeconds(sceneChangeDelay);
		Debug.Log("Scene change at " + Time.time.ToString());
		UnityEngine.SceneManagement.SceneManager.LoadScene("GM-level1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
