using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameManager : ScriptableObject
{
	public delegate void GameEvent();
	public delegate void BoolEvent(bool b);
	public GameEvent GameBegin;
	public BoolEvent GameEnd;

	public void TriggerGameBegin()
	{
		if (GameBegin != null)
		{
			GameBegin();
		}
	}

	public void TriggerGameEnd(bool won)
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
