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
	public int dayEndTime;
	public UnityEvent OnMorning;
	public UnityEvent OnBreak;
	public UnityEvent OnLunch;
	public UnityEvent OnNight;
	public UnityEvent OnDayEnd;

	public enum State { MORNING, LUNCH, BREAK, NIGHT, DAYEND }

	public State timeState = State.NIGHT;

	public int dayCount = 0;

	private float scale = 1f;

	public void DoStart()
	{
		dayCount = 0;
		time = new DateTime(2022, 06, 14, 8, 0, 0);
	}

	public void EnableFastForward()
	{
		scale = 20f;
	}

	public void DisableFastForward()
	{
		scale = 1f;
	}


	public void DoUpdate()
	{
		time = time.AddSeconds(Time.deltaTime * (86400f / dayDurationInSeconds) * scale);

		if (time.Hour == nightTime && timeState != State.NIGHT)
		{
			timeState = State.NIGHT;
			OnNight.Invoke();
		}
		else if (time.Hour == lunchTime && timeState != State.LUNCH)
		{
			timeState = State.LUNCH;
			OnLunch.Invoke();
		}
		else if (time.Hour == morningTime && timeState != State.MORNING)
		{
			timeState = State.MORNING;
			OnMorning.Invoke();
		}
		else if (time.Hour == breakTime && timeState != State.BREAK)
		{
			timeState = State.BREAK;
			OnBreak.Invoke();
		}
		else if (time.Hour == dayEndTime && timeState != State.DAYEND)
		{
			dayCount++;
			timeState = State.DAYEND;
			OnDayEnd.Invoke();
		}
	}
}
