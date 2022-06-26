using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI text;
	[SerializeField] private TimeManager manager;

	private void Awake()
	{
		// manager.OnBreak.AddListener();
	}

	private void Start()
	{
		manager.DoStart();
	}

	private void Update()
	{
		manager.DoUpdate();
		text.text = manager.time.ToShortTimeString();
	}
}
