using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActiveSelection : ScriptableObject
{
	public delegate void SelectionEvent(SelectionInfo info);
	public event SelectionEvent NewSelection;
	public void SetNewSelection(SelectionInfo info)
	{
		if (NewSelection != null)
		{
			NewSelection(info);
		}
	}
}
