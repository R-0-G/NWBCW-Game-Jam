using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipDictionary : MonoBehaviour
{
	private Guid relationshipGUID;
	public Guid RelationshipGUID => relationshipGUID;
	private Dictionary<Human, float> relationshipTimers = new Dictionary<Human, float>();
	[SerializeField] private float freezeTimer = 1f;
	[SerializeField] private float lockedInRelationshipTimer = 1f;
	[SerializeField] private float downTickTimer = 100f;

	public void Clear()
	{
		relationshipTimers.Clear();
	}

	public void FixedUpdateTimer(Human h, out float highestRelationship) //returns max timer
	{
		if (relationshipTimers.ContainsKey(h))
		{
			relationshipTimers[h] += Time.fixedDeltaTime;
		}
		else
		{
			relationshipTimers.Add(h, 0f);
		}
		highestRelationship = 0f; //TODO
								  // if (relationshipTimers[h] > friendTime)
								  // {
								  // 	if (GetLeader(h) == h)
								  // 	{
								  // 		trgPos = h.transform.position;
								  // 	}
								  // }
								  // else
								  // {
								  // 	graphics.SetActive(friendTimers[h] / friendTime);
								  // }
	}

}
