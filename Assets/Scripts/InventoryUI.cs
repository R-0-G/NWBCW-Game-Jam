using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	[SerializeField] private Inventory inventory;
	[SerializeField] private ResourceUICell resourcePrefab;
	[SerializeField] private RectTransform container;

	public void SpawnResourceCells()
	{
		List<Resource> resources = new List<Resource>(inventory.resources);
		for (int i = 0; i < resources.Count; i++)
		{
			ResourceUICell cell = Instantiate(resourcePrefab, container);
			cell.Configure(resources[i]);
		}
	}

	private void Start()
	{
		SpawnResourceCells();
	}
}
