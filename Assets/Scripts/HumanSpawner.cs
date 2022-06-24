using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
	[SerializeField] private Human human;
	[SerializeField] private TransformGroupManager doors;
	[SerializeField] private HumanManager manager;

	private void Awake()
	{
		manager.humanSpawned += HandleSpawn;
	}
	private void OnDestroy()
	{
		manager.humanSpawned -= HandleSpawn;
	}

	private void HandleSpawn()
	{
		Transform t = doors.transforms[Random.Range(0, doors.transforms.Count)];
		Instantiate(human, t.position, t.rotation);
	}
	private void Start()
	{
		//HandleSpawn();
	}
}
