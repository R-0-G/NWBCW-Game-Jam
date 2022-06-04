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
}
