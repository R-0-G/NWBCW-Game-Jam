using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

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
	[SerializeField] private GameObject loseUnion;
	[SerializeField] private AudioClip winAudio;
	[SerializeField] private AudioClip loseAudio;
	[SerializeField] private AudioSource source;

	public UnityEvent OnMorning;
	public UnityEvent OnBreak;
	public UnityEvent OnLunch;
	public UnityEvent OnNight;
	public UnityEvent OnDayEnd;

	private int moneyRaised = 0;

	public TMPro.TextMeshProUGUI goal;
	public TMPro.TextMeshProUGUI current;



	// private void Update()
	// {
	// }

	private void Awake()
	{

		manager.timeManager.OnMorning.AddListener(OnMorning.Invoke);
		manager.timeManager.OnBreak.AddListener(OnBreak.Invoke);
		manager.timeManager.OnLunch.AddListener(OnLunch.Invoke);
		manager.timeManager.OnNight.AddListener(OnNight.Invoke);
		manager.timeManager.OnDayEnd.AddListener(OnDayEnd.Invoke);

		selectionManager.NewSelection += HandleNewSelection;
		manager.timeManager.OnDayEnd.AddListener(HandleDayEnd);
		manager.timeManager.OnMorning.AddListener(HandleMorning);
		money.GainedResource += HandleGained;
		manager.GameEnd += HandleGameEnd;
		manager.currentMoneyTarget = manager.dailyTargets[0];
		moneyRaised = 0;
		progressSlider.value = 0;
		progressSlider.maxValue = manager.currentMoneyTarget;
		manager.TriggerGameBegin();

	}

	private void HandleGained(int count, Resource r)
	{
		goal.text = "Goal: " + manager.currentMoneyTarget.ToString() + "$";
		current.text = "Current: " + moneyRaised + "$";
		moneyRaised += count;
		progressSlider.value = moneyRaised;
	}
	private void HandleGameEnd(int won)
	{
		if (won == 0)
		{
			source.PlayOneShot(winAudio);
			winScreen.SetActive(true);
		}
		else if (won == 1)
		{
			source.PlayOneShot(loseAudio);
			loseScreen.SetActive(true);
		}
		else
		{
			source.PlayOneShot(loseAudio);
			loseUnion.SetActive(true);
		}
	}

	private void OnDestroy()
	{
		manager.timeManager.OnMorning.RemoveListener(OnMorning.Invoke);
		manager.timeManager.OnBreak.RemoveListener(OnBreak.Invoke);
		manager.timeManager.OnLunch.RemoveListener(OnLunch.Invoke);
		manager.timeManager.OnNight.RemoveListener(OnNight.Invoke);
		manager.timeManager.OnDayEnd.RemoveListener(OnDayEnd.Invoke);
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
			manager.TriggerGameEnd(1);
		}

		else if (manager.dailyTargets.Length <= manager.timeManager.dayCount)
		{
			manager.TriggerGameEnd(0);
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
