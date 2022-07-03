using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkingSounds : MonoBehaviour
{
	[SerializeField] private HumanManager manager;
	[SerializeField] private AudioClip[] workingClips;
	[SerializeField] private AudioSource source;

	int lastHumansCuont = 0;

	private void Update()
	{
		int count = manager.List.Where(x => x.StateMachine.CurrentState == HumanStateMachine.State.WORKING).Count();
		count = Mathf.Clamp(count, 0, workingClips.Length);

		if (count != lastHumansCuont)
		{
			source.Stop();
			source.clip = workingClips[count];
			source.Play();
			lastHumansCuont = count;
		}
	}
}
