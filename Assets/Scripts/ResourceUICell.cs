using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUICell : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI countText;
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private Image icon;

	private Resource resource;

	private int resourceover = -1;

	public void Configure(Resource resource, int resourceOverride = -1)
	{
		if (resourceover == -1)
		{
			resourceover = resourceOverride;
		}
		this.resource = resource;
		resource.GainedResource += HandleChanged;
		resource.SpentResource += HandleChanged;
		if (icon) icon.sprite = resource.icon;
		UpdateCount(resourceover != -1 ? resourceover : resource.count);
		if (nameText) nameText.text = resource.resourceName.ToString();
	}

	private void UpdateCount(int count)
	{
		if (countText)
		{
			countText.text = count.ToString();
		}
	}

	private void OnDestroy()
	{
		resource.GainedResource -= HandleChanged;
		resource.SpentResource -= HandleChanged;
	}

	private void HandleChanged(int count, Resource resource)
	{
		if (resourceover == -1)
		{
			UpdateCount(resource.count);
		}
	}
}
