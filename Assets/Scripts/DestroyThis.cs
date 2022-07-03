using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
	public HumanManager man;
	public void Destroyis()
	{
		man.splitSource.PlayOneShot(man.cash);
		Destroy(this.gameObject);
	}
}
