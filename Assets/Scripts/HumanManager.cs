using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HumanManager : ScriptableObject
{
	public List<Human> List = new List<Human>();

	public void Add(Human h)
	{
		if (!List.Contains(h))
		{
			List.Add(h);
		}
	}

	public void Remove(Human h)
	{
		if (List.Contains(h))
		{
			List.Remove(h);
		}
	}

	public delegate void HumanEvent();
	public event HumanEvent humanSpawned;

	public void TriggerSpawnHuman()
	{
		if (humanSpawned != null)
		{
			humanSpawned();
		}
	}
}
