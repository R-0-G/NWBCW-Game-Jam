using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanColliderCheck : MonoBehaviour
{
	[SerializeField] private HumanManager humans;
	[SerializeField] private TransformGroupManager doors;
	[SerializeField] private float friendRadius = 1f;
	[SerializeField] private float doorRadius = 0.1f;
	[SerializeField] private float jobRadius = 0.01f;
	public bool FriendableHumansInRange(out List<Human> humansInRange)
	{
		humansInRange = new List<Human>();
		for (int i = 0; i < humans.List.Count; i++)
		{
			Human h = humans.List[i];

			if (h.transform != transform && h.canBefriend)
			{
				if (Vector2.Distance(h.transform.position, transform.position) < friendRadius)
				{
					humansInRange.Add(h);
				}
			}
		}
		return humansInRange.Count > 0;
	}

	public bool AnyDoorInRange(out Transform doorInRange)
	{
		for (int i = 0; i < doors.transforms.Count; i++)
		{
			Transform door = doors.transforms[i];

			if (Vector2.Distance(door.position, transform.position) < friendRadius)
			{
				doorInRange = door;
				return true;
			}

		}
		doorInRange = null;
		return false;
	}

	public bool IsJobInRange(Transform jobTrans)
	{
		return Vector2.Distance(jobTrans.position, transform.position) < jobRadius;
	}
}
