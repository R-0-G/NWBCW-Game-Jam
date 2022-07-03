using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
	public NavMeshAgent agent;
	public HumanManager humanManager;
	public float initiative;
	[SerializeField] private float splitTimer = 5f;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private HumanStateMachine stateMachine;
	[SerializeField] private HumanGraphics graphics;
	[SerializeField] private RelationshipManager relationshipManager;
	[SerializeField] private HumanAIController aIController;
	[SerializeField] private HumanColliderCheck colliderCheck;
	[SerializeField] private float highest;
	[SerializeField] private float productivityRecharge;
	[SerializeField] private float maxHappySpeed = 10f;
	[SerializeField] private float minHappySpeed = 3f;
	[SerializeField] private float overrideSpeed = 20f;
	[SerializeField] private float zombieValue;
	[SerializeField] private float unionCutoff = 10f;
	[SerializeField] private float unionWinCutoff = 2f;
	[SerializeField] private AudioSource source;
	[SerializeField] private AudioClip[] yawns;
	[SerializeField] private AudioClip dooropen;
	[SerializeField] private float randomYawnCutoff = 0.2f;
	[SerializeField] private float randomYawnDelay = 6f;

	private bool isZombie = false;
	private bool isUnion = false;

	private bool speedOverride = false;

	public void Configure(bool zomb, bool union)
	{
		if (zomb)
		{
			isZombie = true;
		}

		if (union)
		{
			isUnion = true;
		}
	}



	public float happiness;

	public HumanStateMachine StateMachine => stateMachine;

	public bool canBefriend = true;



	private void Awake()
	{
		humanManager.Add(this);
		stateMachine.onStateChanged += HandleStateChanged;
	}

	private void OnDestroy()
	{
		humanManager.Remove(this);
		stateMachine.onStateChanged -= HandleStateChanged;
	}

	private void HandleStateChanged(HumanStateMachine.State prev, HumanStateMachine.State next)
	{
		if (next == HumanStateMachine.State.DESTROYING)
		{
			Destroy(this.gameObject);
		}
		if (prev == HumanStateMachine.State.WORKING && next == HumanStateMachine.State.SLACKING)
		{
			for (int j = 0; j < gameManager.inventory.resources.Count; j++)
			{
				gameManager.inventory.resources[j].Gain(Random.Range(0, Mathf.CeilToInt(5 * happiness))); //TODO obviously magic numbers
			}
		}
	}

	private void Start()
	{
		initiative = Random.value;
		InvokeRepeating("YawnCheck", Random.Range(randomYawnDelay / 2f, randomYawnDelay), randomYawnDelay);
	}

	private void YawnCheck()
	{
		if (Random.value < randomYawnCutoff)
		{
			if (stateMachine.CurrentState == HumanStateMachine.State.WORKING || stateMachine.CurrentState == HumanStateMachine.State.SLACKING)
			{
				source.PlayOneShot(yawns[Random.Range(0, yawns.Length)], 0.5f);
			}
		}
	}



	public void EnableFriendship()
	{
		canBefriend = true;
	}

	private void FixedUpdate()
	{
		agent.destination = aIController.GetTarget();
		if (speedOverride)
		{
			agent.speed = overrideSpeed;
		}
		else
		{
			agent.speed = Mathf.Lerp(minHappySpeed, maxHappySpeed, happiness);

		}

		if (canBefriend)
		{
			if (colliderCheck.FriendableHumansInRange(out List<Human> humans))
			{
				for (int i = 0; i < humans.Count; i++)
				{
					if (!relationshipManager.isLocked)
					{
						relationshipManager.UpdateRelationship(humans[i], out highest);
					}
				}
				graphics.SetActive(highest);
			}
		}

		if (happiness < 1)
		{
			happiness += Time.fixedDeltaTime * productivityRecharge;
			if (happiness > 1f)
			{
				happiness = 1f;
			}
		}

		if (!isUnion && highest > unionCutoff)
		{
			isUnion = true;
			gameManager.unionCount++;
			if (gameManager.unionCount * unionWinCutoff > humanManager.List.Count)
			{
				gameManager.TriggerGameEnd(2);
			}
		}
	}


	public Human GetLeader(Human other)
	{
		return (initiative > other.initiative) ? this : other;
	}

	// private void FriendCheck()
	// {
	// 	if (canBefriend) //canbefriend should probably go in relationshipdict
	// 	{
	// 		if (colliderCheck.FriendableHumansInRange(out List<Human> friendables))
	// 		{
	// 			for (int i = 0; i < friendables.Count; i++)
	// 			{
	// 				Human h = friendables[i];
	// 				if (friendTimers.ContainsKey(h))
	// 				{
	// 					friendTimers[h] += Time.fixedDeltaTime;
	// 				}
	// 				else
	// 				{
	// 					friendTimers.Add(h, 0f);
	// 				}
	// 				if (friendTimers[h] > friendTime)
	// 				{
	// 					if (GetLeader(h) == h)
	// 					{
	// 						trgPos = h.transform.position;
	// 					}
	// 				}
	// 				else
	// 				{
	// 					graphics.SetActive(friendTimers[h] / friendTime);
	// 				}
	// 			}
	// 		}
	// 	}
	// }


	[ContextMenu("ungroup")]
	public void UnGroup()
	{
		if (stateMachine.CurrentState != HumanStateMachine.State.DESTROYING || stateMachine.CurrentState != HumanStateMachine.State.GOING_HOME)
		{
			graphics.Ungroup();
			// relationshipManager
			// friendTimers.Clear();
			// relationshipManager.ClearInProgress();
			stateMachine.TriggerStateChange(HumanStateMachine.State.FINDING_JOB);
			canBefriend = false;
			speedOverride = true;
			Invoke("Slow", 0.5f);
			// FindNextTarget();
			Invoke("EnableFriendship", splitTimer);
			happiness = 0f;
			ZombieCheck();

		}
	}

	private void Slow()
	{
		speedOverride = false;
	}

	private void ZombieCheck()
	{
		if (!isUnion && Random.value < zombieValue)
		{
			gameManager.zombieCount++;
			graphics.ConfigureZombie();
			isZombie = true;
		}
	}
}
