using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
	[SerializeField] private Human human;
	[SerializeField] private TransformGroupManager doors;
	[SerializeField] private HumanManager manager;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private AudioSource source;

	private void Awake()
	{
		manager.humanSpawned += HandleSpawn;
		gameManager.timeManager.OnMorning.AddListener(HandleMorning);
		manager.splitSource = source;
	}
	private void OnDestroy()
	{
		manager.humanSpawned -= HandleSpawn;
		gameManager.timeManager.OnMorning.RemoveListener(HandleMorning);
	}


	private void HandleMorning()
	{
		for (int i = 0; i < gameManager.jobs.transforms.Count; i++)
		{
			bool zomb = i < gameManager.zombieCount;
			bool union = (i < gameManager.zombieCount + gameManager.zombieCount) && !zomb;
			manager.TriggerSpawnHuman(zomb, union);
		}
	}

	private void HandleSpawn(bool zomb, bool union)
	{
		Transform t = doors.transforms[Random.Range(0, doors.transforms.Count)];
		Human h = Instantiate(human, t.position, t.rotation);
		h.Configure(zomb, union);
	}
}