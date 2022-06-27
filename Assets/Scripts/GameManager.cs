using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameManager : ScriptableObject
{
	public delegate void GameEvent();
	public GameEvent GameBegin;
	public GameEvent GameEnd;

	public void TriggerGameBegin()
	{
		if (GameBegin != null)
		{
			GameBegin();
		}
	}

	public int currentMoneyTarget;
	public int[] dailyTargets;
	public TimeManager timeManager; //Use time manager instead
	public Inventory inventory;

	public TransformGroupManager jobs;



}
