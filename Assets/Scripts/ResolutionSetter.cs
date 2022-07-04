using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSetter : MonoBehaviour
{
	private void Awake()
	{
		Screen.SetResolution(1920, 1080, true);
	}
}
