using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
	[SerializeField] private Human human;
	[SerializeField] private TransformGroupManager doors;
	[SerializeField] private HumanManager manager;
	[SerializeField] private GameManager gameManager;

	private void Awake()
	{
		manager.humanSpawned += HandleSpawn;
		gameManager.timeManager.OnMorning.AddListener(HandleMorning);
	}
	private void OnDestroy()
	{
		manager.humanSpawned -= HandleSpawn;
		gameManager.timeManager.OnMorning.RemoveListener(HandleMorning);
	}

	private void HandleMorning()
	{
		Debug.LogError("HANDLING MORNING");
		for (int i = 0; i < gameManager.jobs.transforms.Count; i++)
		{
			manager.TriggerSpawnHuman();
		}
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
