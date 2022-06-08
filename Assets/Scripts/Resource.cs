using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Resource : ScriptableObject
{
	public delegate void ResourceEvent(int count, Resource resource);
	public event ResourceEvent SpentResource;
	public event ResourceEvent GainedResource;

	public string resourceName;
	public Sprite icon;
	public int count;

	public bool CanSpend(int countToSpend)
	{
		return count >= countToSpend;
	}

	public void Spend(int countToSpend)
	{
		count -= countToSpend;

		if (SpentResource != null)
		{
			SpentResource(countToSpend, this);
		}
	}

	public void Gain(int countToGain)
	{
		count += countToGain;
		if (GainedResource != null)
		{
			GainedResource(countToGain, this);
		}
	}
}
