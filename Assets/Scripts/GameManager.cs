using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameManager : ScriptableObject
{
	public delegate void GameEvent();
	public delegate void BoolEvent(int b);
	public GameEvent GameBegin;
	public BoolEvent GameEnd;

	public int zombieCount = 0;
	public int unionCount = 0;

	public void TriggerGameBegin()
	{
		if (GameBegin != null)
		{
			zombieCount = 0;
			unionCount = 0;
			GameBegin();
		}
	}

	public void TriggerGameEnd(int won)
	{
		if (GameEnd != null)
		{
			GameEnd(won);
		}
	}

	public int currentMoneyTarget;
	public int[] dailyTargets;
	public TimeManager timeManager; //Use time manager instead
	public Inventory inventory;

	public TransformGroupManager jobs;



}
