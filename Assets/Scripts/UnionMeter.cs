using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionMeter : MonoBehaviour
{
	public GameManager gameManager;
	public HumanManager hm;

	public TMPro.TextMeshProUGUI t;

	// Update is called once per frame
	void Update()
	{
		float percent = (gameManager.unionCount * 2f) / hm.List.Count;
		int ipercent = Mathf.CeilToInt(percent * 100f);


		t.text = "CC " + ipercent + "%";
	}
}
