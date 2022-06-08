using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAddItem : MonoBehaviour
{
	public GameObject itemToAdd;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			position.z = 0f;
			Instantiate(itemToAdd, position, Quaternion.identity);
		}
	}
}
