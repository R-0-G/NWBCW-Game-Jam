using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
	public delegate void TurnEvent();
	public TurnEvent TurnBegin;

	private Player player;

	public void Configure(Player activePlayer)
	{
		player = activePlayer;
	}

	public void Begin()
	{

	}
}
