using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI playerName;
	[SerializeField] private TextMeshProUGUI playerResource;
	[SerializeField] private TextMeshProUGUI infoBoxTitle;
	[SerializeField] private ActiveSelection selectionManager;
	[SerializeField] private GameObject infoBox;
	[SerializeField] private Slider progressSlider;
	[SerializeField] private GameManager manager;

	private void Awake()
	{
		selectionManager.NewSelection += HandleNewSelection;
		manager.timeManager.OnDayEnd.AddListener(HandleDayEnd);
	}

	private void OnDestroy()
	{
		selectionManager.NewSelection -= HandleNewSelection;
		manager.timeManager.OnDayEnd.RemoveListener(HandleDayEnd);
	}

	private void HandleDayEnd()
	{
		manager.currentMoneyTarget = manager.dailyTargets[manager.timeManager.dayCount];
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
