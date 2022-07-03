using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipManager : MonoBehaviour
{
	public List<RelationshipDictionary> relationshipLevels;

	public int UpdateRelationship(Human h, out float highest)
	{
		highest = 0f;
		for (int i = 0; i < relationshipLevels.Count; i++)
		{
			RelationshipDictionary level = relationshipLevels[i];
			if (!level.IsLocked(h))
			{
				float value = level.UpdateRelationship(h);
				if (value > highest)
				{
					highest = value;
				}
				return i;
			}
		}
		return -1;
	}

	public void ClearInProgress()
	{
		for (int i = 0; i < relationshipLevels.Count; i++)
		{
			RelationshipDictionary level = relationshipLevels[i];
			level.ClearInProgress();
		}
	}
}
