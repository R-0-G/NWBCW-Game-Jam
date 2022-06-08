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

	public void Configure(Resource resource)
	{
		if (icon) icon.sprite = resource.icon;
		if (countText) countText.text = resource.count.ToString();
		if (nameText) nameText.text = resource.resourceName.ToString();
	}
}
