using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitGroup : MonoBehaviour
{
	[SerializeField] private HumanManager humans;
	[SerializeField] private float radius = 5f;
	private void Start()
	{
		for (int i = 0; i < humans.List.Count; i++)
		{
			if (Vector2.Distance(transform.position, humans.List[i].transform.position) < radius)
			{
				humans.List[i].UnGroup();
			}
		}
		GameObject.FindObjectOfType<CameraFollow>().ShouldShake();
		Destroy(this.gameObject);
	}
}
