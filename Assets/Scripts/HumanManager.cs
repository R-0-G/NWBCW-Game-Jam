using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HumanManager : ScriptableObject
{
	public List<Human> List = new List<Human>();
	public AudioClip cash;
	public AudioSource splitSource;
	// public 

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

	public delegate void HumanEvent(bool zomb, bool union);
	public event HumanEvent humanSpawned;

	public void TriggerSpawnHuman(bool zomb, bool union)
	{
		if (humanSpawned != null)
		{
			humanSpawned(zomb, union);
		}
	}
}
