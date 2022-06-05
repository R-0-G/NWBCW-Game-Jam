using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI playerName;
	[SerializeField] private TextMeshProUGUI playerResource;
	[SerializeField] private TextMeshProUGUI infoBoxTitle;
	[SerializeField] private ActiveSelection selectionManager;
	[SerializeField] private GameObject infoBox;

	private void Awake()
	{
		selectionManager.NewSelection += HandleNewSelection;
	}

	private void OnDestroy()
	{
		selectionManager.NewSelection -= HandleNewSelection;
	}

	private void HandleNewSelection(SelectionInfo info)
	{
		if (info != null)
		{
			infoBox.SetActive(true);
			infoBoxTitle.text = info.Title;
		}
		else
		{
			infoBox.SetActive(false);
		}
	}
}
