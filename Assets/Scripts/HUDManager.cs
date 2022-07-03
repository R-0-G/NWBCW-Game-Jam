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
	[SerializeField] private Resource money;
	[SerializeField] private GameObject winScreen;
	[SerializeField] private GameObject loseScreen;
	private int moneyRaised = 0;

	private void Awake()
	{
		selectionManager.NewSelection += HandleNewSelection;
		manager.timeManager.OnDayEnd.AddListener(HandleDayEnd);
		manager.timeManager.OnMorning.AddListener(HandleMorning);
		money.GainedResource += HandleGained;
		manager.GameEnd += HandleGameEnd;
		manager.currentMoneyTarget = manager.dailyTargets[0];
		moneyRaised = 0;
		progressSlider.value = 0;
		progressSlider.maxValue = manager.currentMoneyTarget;

	}

	private void HandleGained(int count, Resource r)
	{
		moneyRaised += count;
		progressSlider.value = moneyRaised;
	}
	private void HandleGameEnd(bool won)
	{
		if (won)
		{
			winScreen.SetActive(true);
		}
		else
		{
			loseScreen.SetActive(true);
		}
	}

	private void OnDestroy()
	{
		selectionManager.NewSelection -= HandleNewSelection;
		manager.GameEnd -= HandleGameEnd;
		manager.timeManager.OnDayEnd.RemoveListener(HandleDayEnd);
		manager.timeManager.OnMorning.RemoveListener(HandleMorning);
	}

	private void HandleMorning()
	{
		progressSlider.maxValue = manager.currentMoneyTarget;

	}

	private void HandleDayEnd()
	{
		if (manager.currentMoneyTarget < moneyRaised)
		{
			manager.TriggerGameEnd(false);
		}

		else if (manager.dailyTargets.Length <= manager.timeManager.dayCount)
		{
			manager.TriggerGameEnd(true);
		}

		moneyRaised = 0;

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
