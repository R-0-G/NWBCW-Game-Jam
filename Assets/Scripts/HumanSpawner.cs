using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
	[SerializeField] private Human human;
	private void Start()
	{
		Instantiate(human, transform.position, transform.rotation);
	}
}
