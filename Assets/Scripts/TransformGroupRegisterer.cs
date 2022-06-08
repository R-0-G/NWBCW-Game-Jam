using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformGroupRegisterer : MonoBehaviour
{
	[SerializeField] private TransformGroupManager manager;
	private void Awake()
	{
		manager.Add(transform);
	}

	private void OnDestroy()
	{
		manager.Remove(transform);
	}
}
