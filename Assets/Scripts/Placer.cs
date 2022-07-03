using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
	private bool canPlace = false;
	private ShopItem item;
	public void Configure(ShopItem item)
	{
		this.item = item;
		Invoke("CanPlace", 0.05f);
	}

	private void CanPlace()
	{
		canPlace = true;
	}
	private void Update()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		position.z = 0f;
		transform.position = position;

		if (canPlace)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Instantiate(item.job, position, Quaternion.identity);
				Destroy(this.gameObject);
			}
		}
	}
}
