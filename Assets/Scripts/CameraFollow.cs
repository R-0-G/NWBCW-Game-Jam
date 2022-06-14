using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	[SerializeField] private float speed;
	[SerializeField] private float zoomSpeed;
	[SerializeField] private Camera cam;

	private void Update()
	{
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

		cam.fieldOfView += scroll;
		cam.orthographicSize -= scroll;
		if (cam.fieldOfView > 120) cam.fieldOfView = 120;
		if (cam.fieldOfView < 30) cam.fieldOfView = 30;
		if (cam.orthographicSize > 40) cam.orthographicSize = 40;
		if (cam.orthographicSize < 1) cam.orthographicSize = 1;

		// transform.Translate(0, 0, scroll * zoomSpeed, Space.World);

		if (shouldShake)
		{
			if (remainingShake > 0)
			{
				transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

				remainingShake -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shouldShake = false;
				remainingShake = 0f;
				transform.localPosition = originalPos;
			}
		}
	}

	private bool shouldShake = false;

	[ContextMenu("Shake")]
	public void ShouldShake()
	{
		originalPos = transform.localPosition;
		shouldShake = true;
		remainingShake = shakeDuration;
	}

	public float shakeDuration = 0.4f;


	// How long the object should shake for.
	public float remainingShake = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	private Vector3 originalPos;

}
