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
				gameManager.inventory.resources[j].Gain(Random.Range(0, 5)); //TODO obviously magic numbers
			}
		}
	}

	private void Start()
	{
		agent.speed = 10f;
		initiative = Random.value;
	}

	public void EnableFriendship()
	{
		canBefriend = true;
	}

	private void FixedUpdate()
	{
		agent.destination = aIController.GetTarget();

		if (canBefriend)
		{
			if (colliderCheck.FriendableHumansInRange(out List<Human> humans))
			{
				for (int i = 0; i < humans.Count; i++)
				{
					relationshipManager.UpdateRelationship(humans[i], out highest);
				}
				graphics.SetActive(highest);
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
		graphics.Ungroup();
		// relationshipManager
		// friendTimers.Clear();
		relationshipManager.ClearInProgress();
		canBefriend = false;
		// FindNextTarget();
		Invoke("EnableFriendship", splitTimer);
	}
}
