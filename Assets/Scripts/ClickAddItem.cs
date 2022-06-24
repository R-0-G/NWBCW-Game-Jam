using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickAddItem : MonoBehaviour
{
	[System.Serializable]
	public class ClickData
	{
		public GameObject itemToAdd;
		public int buttonIndex = 0;
		public UnityEvent spawnEvent;
	}

	public List<ClickData> datas;

	private void Update()
	{
		for (int i = 0; i < datas.Count; i++)
		{
			if (Input.GetMouseButtonDown(datas[i].buttonIndex))
			{
				if (datas[i].itemToAdd)
				{
					Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					position.z = 0f;
					Instantiate(datas[i].itemToAdd, position, Quaternion.identity);
				}
				else
				{
					datas[i].spawnEvent.Invoke();
				}
			}
		}
	}
}
