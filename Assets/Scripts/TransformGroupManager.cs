using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TransformGroupManager : ScriptableObject
{
	public List<Transform> transforms;

	public void Add(Transform t)
	{
		if (!transforms.Contains(t))
		{
			transforms.Add(t);
		}
	}

	public void Remove(Transform t)
	{
		if (transforms.Contains(t))
		{
			transforms.Remove(t);
		}
	}
}
