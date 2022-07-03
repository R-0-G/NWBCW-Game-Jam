using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitGroup : MonoBehaviour
{
	[SerializeField] private HumanManager humans;
	[SerializeField] private float radius = 5f;
	[SerializeField] private AudioClip one;
	[SerializeField] private AudioClip two;
	[SerializeField] private AudioClip four;
	[SerializeField] private AudioClip seven;
	private void Start()
	{
		int coutn = 0;
		for (int i = 0; i < humans.List.Count; i++)
		{
			if (Vector2.Distance(transform.position, humans.List[i].transform.position) < radius)
			{
				coutn++;
				humans.List[i].UnGroup();
			}
		}

		if (coutn < 2)
		{
			humans.splitSource.PlayOneShot(one);
		}
		else if (coutn < 4)
		{
			humans.splitSource.PlayOneShot(two);
		}
		else if (coutn < 7)
		{
			humans.splitSource.PlayOneShot(four);
		}
		else
		{
			humans.splitSource.PlayOneShot(seven);
		}


		GameObject.FindObjectOfType<CameraFollow>().ShouldShake();
		Destroy(this.gameObject);
	}
}
