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
	[SerializeField] private float friendRadius = 1f;
	[SerializeField] private float friendTime = 5f;
	[SerializeField] private float splitTimer = 5f;
	[SerializeField] private TransformGroupManager jobs;
	[SerializeField] private SpriteRenderer sp;

	public bool canBefriend = true;
	private Dictionary<Human, float> friendTimers = new Dictionary<Human, float>();

	int i = 0;

	private void Awake()
	{
		targetManager.Add(this);
	}

	private void OnDestroy()
	{
		targetManager.Remove(this);
	}

	private void Start()
	{
		agent.speed = 10f;
		initiative = Random.value;
		FindNextTarget();

	}

	private void FindNextTarget()
	{
		target = jobs.transforms[Random.Range(0, jobs.transforms.Count)];
		trgPos = target.position;

	}

	private void NextTargetCheck()
	{
		if (Vector2.Distance(target.position, transform.position) < 0.01f)
		{
			FindNextTarget();
		}
	}

	public void EnableFriendship()
	{
		canBefriend = true;
	}

	private void FixedUpdate()
	{
		NextTargetCheck();
		FriendCheck();
		agent.destination = trgPos;
	}

	public Human GetLeader(Human other)
	{
		return (initiative > other.initiative) ? this : other;
	}

	private void FriendCheck()
	{
		if (canBefriend)
		{
			Human h = null;
			for (int i = 0; i < targetManager.List.Count; i++)
			{
				h = targetManager.List[i];

				if (h != this && h.canBefriend)
				{
					if (Vector2.Distance(h.transform.position, transform.position) < friendRadius)
					{
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
							float value = Mathf.InverseLerp(0f, 1f, sp.color.r);
							float lerpValue = friendTimers[h] / friendTime;
							if (value < lerpValue)
							{
								sp.color = new Color(lerpValue, 0f, 0f, 1f);
							}
						}
					}
				}
			}

		}
	}


	[ContextMenu("ungroup")]
	public void UnGroup()
	{
		sp.color = new Color(0f, 0f, 0f, 1f);
		friendTimers.Clear();
		canBefriend = false;
		FindNextTarget();
		Invoke("EnableFriendship", splitTimer);
	}
}
