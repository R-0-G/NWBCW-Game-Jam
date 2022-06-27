using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
	public NavMeshAgent agent;
	public HumanManager targetManager;
	private Transform target;
	private Vector3 trgPos;
	public float initiative;
	[SerializeField] private float friendTime = 5f;
	[SerializeField] private float splitTimer = 5f;
	[SerializeField] private TransformGroupManager jobs;
	[SerializeField] private TransformGroupManager doors;
	[SerializeField] private AudioPlayer audioPlayer;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private HumanColliderCheck colliderCheck;
	[SerializeField] private HumanStateMachine stateMachine;
	[SerializeField] private HumanGraphics graphics;

	public bool canBefriend = true;
	private Dictionary<Human, float> friendTimers = new Dictionary<Human, float>();
	private bool isNight = false;
	private bool wentToJob = false;



	private void Awake()
	{
		targetManager.Add(this);
		gameManager.timeManager.OnNight.AddListener(HandleNight);

	}

	private void OnDestroy()
	{
		targetManager.Remove(this);
		gameManager.timeManager.OnNight.RemoveListener(HandleNight);
	}

	private void HandleNight()
	{
		isNight = true;
		trgPos = doors.transforms[Random.Range(0, doors.transforms.Count)].position;
	}

	private void Start()
	{
		agent.speed = 10f;
		initiative = Random.value;
		FindNextTarget();

	}

	private void FindNextTarget()
	{
		if (CanActAutonomously() || !wentToJob) //went To job is hack
		{
			wentToJob = true;
			target = jobs.transforms[Random.Range(0, jobs.transforms.Count)];
			trgPos = target.position;
		}
	}

	private void NextTargetCheck()
	{
		if (colliderCheck.IsJobInRange(target))
		{
			audioPlayer.PlayRandom();
			FindNextTarget();
		}
	}

	private bool CanActAutonomously()
	{
		return false;
	}

	public void EnableFriendship()
	{
		canBefriend = true;
	}

	private void FixedUpdate()
	{
		if (!isNight)
		{
			NextTargetCheck();
			FriendCheck();
		}
		else
		{
			MakeMoneyCheck();
		}
		if (stateMachine.state != HumanStateMachine.State.DESTROYING)
		{
			agent.destination = trgPos;
		}
	}

	private void MakeMoneyCheck()
	{
		if (isNight && stateMachine.state != HumanStateMachine.State.DESTROYING)
		{
			if (colliderCheck.AnyDoorInRange(out Transform door))
			{
				for (int j = 0; j < gameManager.inventory.resources.Count; j++)
				{
					gameManager.inventory.resources[j].Gain(Random.Range(0, 5)); //TODO obviously magic numbers
				}
				stateMachine.state = HumanStateMachine.State.DESTROYING;
				Destroy(this.gameObject);
			}
		}
	}

	public Human GetLeader(Human other)
	{
		return (initiative > other.initiative) ? this : other;
	}

	private void FriendCheck()
	{
		if (canBefriend)
		{
			if (colliderCheck.FriendableHumansInRange(out List<Human> friendables))
			{
				for (int i = 0; i < friendables.Count; i++)
				{
					Human h = friendables[i];
					if (friendTimers.ContainsKey(h))
					{
						friendTimers[h] += Time.fixedDeltaTime;
					}
					else
					{
						friendTimers.Add(h, 0f);
					}
					if (friendTimers[h] > friendTime)
					{
						if (GetLeader(h) == h)
						{
							trgPos = h.transform.position;
						}
					}
					else
					{
						graphics.SetActive(friendTimers[h] / friendTime);
					}
				}
			}
		}
	}


	[ContextMenu("ungroup")]
	public void UnGroup()
	{
		graphics.Ungroup();
		friendTimers.Clear();
		canBefriend = false;
		FindNextTarget();
		Invoke("EnableFriendship", splitTimer);
	}
}
