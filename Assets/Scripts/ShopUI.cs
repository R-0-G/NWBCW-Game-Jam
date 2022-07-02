using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
	public ShopInventory inventory;
	public ShopItemUI cellPrefab;
	public RectTransform container;

	private void Start()
	{
		Populate();
	}

	public void Populate()
	{
		for (int i = 0; i < inventory.items.Count; i++)
		{
			ShopItemUI cell = Instantiate(cellPrefab, container);
			cell.Configure(inventory.items[i]);
		}
	}
}
