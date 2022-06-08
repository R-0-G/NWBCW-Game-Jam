using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
	public NavMeshAgent agent;
	public Transform[] targets;

	private Transform target;
	private Vector3 trgPos;

	int i = 0;

	private void Start()
	{
		InvokeRepeating("FindNextTarget", 1f, 5f);
	}

	private void FindNextTarget()
	{
		// target = targets[Random.Range(0, targets.Length - 1)];
		target = targets[i];
		i++;
		if (i >= targets.Length) i = 0;
		trgPos = target.position;

		agent.destination = trgPos;
		agent.speed = 10f;
	}
}
