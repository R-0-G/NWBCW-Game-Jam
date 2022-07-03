using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShopItem : ScriptableObject
{
	public List<int> cost;
	public List<Resource> resources;
	public Sprite sprite;
	public string Description;
	public string Title;
	public Job job;
}
