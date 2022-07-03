using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanGraphics : MonoBehaviour
{
	[SerializeField] private SpriteRenderer sp;
	public void Ungroup()
	{
		sp.color = new Color(0f, 0f, 0f, 1f);
	}

	public void SetActive(float t)
	{
		float value = Mathf.InverseLerp(0f, 1f, sp.color.r);
		float lerpValue = t;
		if (value < lerpValue)
		{
			sp.color = new Color(lerpValue, 0f, 0f, 1f);
		}
	}
}
