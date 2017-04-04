using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour {

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shake = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	private float nowShakeAmount = 0.7f;
	private float nowShake = 0f;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable()
	{
		nowShakeAmount = shakeAmount;
		nowShake = shake;

	}
	
	void Update()
	{
		if (nowShake > 0)
		{
			camTransform.localPosition = camTransform.localPosition + Random.insideUnitSphere * nowShakeAmount;
			nowShake -= Time.deltaTime;
			nowShakeAmount *= decreaseFactor;
		}
	}
}

