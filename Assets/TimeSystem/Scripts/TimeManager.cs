using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class TimeManager : ScriptableObject
{
	public DateTime time;
	public float dayDurationInSeconds;
	public int nightTime;
	public int lunchTime;
	public int morningTime;
	public int breakTime;
	public UnityEvent OnMorning;
	public UnityEvent OnBreak;
	public UnityEvent OnLunch;
	public UnityEvent OnNight;

	public void DoStart()
	{
		time = new DateTime(2022, 06, 14, 8, 0, 0);
	}

	public void DoUpdate()
	{
		time = time.AddSeconds(Time.deltaTime * (86400f / dayDurationInSeconds));

		if (time.Hour == nightTime) OnNight.Invoke();
		if (time.Hour == lunchTime) OnLunch.Invoke();
		if (time.Hour == morningTime) OnMorning.Invoke();
		if (time.Hour == breakTime) OnBreak.Invoke();
	}
}
