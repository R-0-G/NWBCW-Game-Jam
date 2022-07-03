using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTurnOff : MonoBehaviour
{
	public TMPro.TextMeshProUGUI trg;
	public void Trig(string text)
	{
		trg.text = text;
		Invoke("ON", 0);
		Invoke("OFF", 0.25f);
		Invoke("ON", 0.5f);
		Invoke("OFF", 0.75f);
	}

	public void ON()
	{
		trg.gameObject.SetActive(true);
	}

	public void OFF()
	{
		trg.gameObject.SetActive(false);
	}
}
