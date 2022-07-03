using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForwardButton : MonoBehaviour
{
	[SerializeField] private GameObject container;
	[SerializeField] private TimeManager timeManager;
	private void Awake()
	{
		timeManager.OnNight.AddListener(Show);
		timeManager.OnMorning.AddListener(Hide);
	}
	private void OnDestroy()
	{
		timeManager.OnNight.RemoveListener(Show);
		timeManager.OnMorning.RemoveListener(Hide);
	}

	private void Show()
	{
		container.SetActive(true);
	}

	private void Hide()
	{
		container.SetActive(false);
	}
}
