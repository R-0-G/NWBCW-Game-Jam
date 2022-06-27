using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickAddItem : MonoBehaviour
{
	[System.Serializable]
	public class ClickData
	{
		public GameObject itemToAdd;
		public int buttonIndex = 0;
		public UnityEvent spawnEvent;
		public bool blockIfOverUI;
	}

	public List<ClickData> datas;

	private EventSystem eventSystem;

	private void Start()
	{
		FindEventSystem();
	}

	private void FindEventSystem()
	{
		eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
	}

	private void Update()
	{
		if (eventSystem) FindEventSystem();
		for (int i = 0; i < datas.Count; i++)
		{
			bool block = eventSystem != null && eventSystem.IsPointerOverGameObject() && datas[i].blockIfOverUI;
			if (Input.GetMouseButtonDown(datas[i].buttonIndex) && !block)
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
