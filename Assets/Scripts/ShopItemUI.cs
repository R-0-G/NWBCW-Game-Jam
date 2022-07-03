using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ShopItemUI : MonoBehaviour
{
	private ShopItem shopItem;
	public Image image;
	public TextMeshProUGUI title;
	public TextMeshProUGUI desc;
	public ResourceUICell resourcePrefab;
	public RectTransform resourceContainer;
	public Inventory inv;

	public Placer placerPrefab;

	public UnityEvent onInteractable;
	public UnityEvent onNotInteractable;

	private bool isInteractable = false;

	public void Configure(ShopItem item)
	{
		shopItem = item;

		for (int i = 0; i < item.resources.Count; i++)
		{
			item.resources[i].GainedResource += HandleResourceUpdated;
			item.resources[i].SpentResource += HandleResourceUpdated;
			HandleResourceUpdated(0, item.resources[i]);
		}
		if (image) image.sprite = item.sprite;
		if (title) title.text = item.Title;
		if (desc) desc.text = item.Description;
		SpawnResourceCells();
	}

	public void SpawnResourceCells()
	{
		List<Resource> resources = shopItem.resources;
		for (int i = 0; i < resources.Count; i++)
		{
			ResourceUICell cell = Instantiate(resourcePrefab, resourceContainer);
			cell.Configure(resources[i], shopItem.cost[i]);
		}
	}

	private void OnDestroy()
	{
		for (int i = 0; i < shopItem.resources.Count; i++)
		{
			shopItem.resources[i].GainedResource -= HandleResourceUpdated;
			shopItem.resources[i].SpentResource -= HandleResourceUpdated;
		}
	}

	private void HandleResourceUpdated(int count, Resource resource)
	{
		int shopIndex = shopItem.resources.IndexOf(resource);
		if (shopIndex > -1) //if we cost this resoure in some way
		{
			int shopCost = shopItem.cost[shopIndex];

			bool newInteractable = resource.count >= shopCost;
			if (isInteractable != newInteractable)
			{
				SetInteractable(newInteractable);
			}
		}
	}

	private void SetInteractable(bool interactable)
	{
		if (interactable && !isInteractable)
		{
			onInteractable.Invoke();
		}
		else if (!interactable && isInteractable)
		{
			onNotInteractable.Invoke();
		}

		isInteractable = interactable;
	}

	public void OnClick()
	{
		for (int i = 0; i < shopItem.resources.Count; i++)
		{
			Resource shopItemResource = shopItem.resources[i];
			int shopCount = shopItem.cost[i];
			shopItemResource.Spend(shopCount);
		}
		Instantiate(placerPrefab).Configure(shopItem);
	}
}
