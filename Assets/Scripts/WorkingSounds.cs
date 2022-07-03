using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class WorkingSounds : MonoBehaviour
{
	[SerializeField] private HumanManager manager;
	[SerializeField] private AudioClip[] workingClips;
	[SerializeField] private AudioSource source;
	[SerializeField] private AudioMixerSnapshot noWorking;
	[SerializeField] private AudioMixerSnapshot someWorking;
	[SerializeField] private float fadeTime = 0.5f;

	int lastHumansCuont = 0;
	bool wasZero = true;

	private void Awake()
	{
		noWorking.TransitionTo(0f);
	}

	private void Update()
	{
		int count = manager.List.Where(x => x.StateMachine.CurrentState == HumanStateMachine.State.WORKING).Count();

		if (count != lastHumansCuont)
		{
			source.Stop();
			if (count > 0)
			{
				if (wasZero)
				{
					someWorking.TransitionTo(fadeTime);
				}
				count = Mathf.Clamp(count, 1, workingClips.Length) - 1;
				source.clip = workingClips[count];
				source.Play();
			}
			else
			{
				if (!wasZero)
				{
					noWorking.TransitionTo(fadeTime);
				}
			}
			lastHumansCuont = count;
			wasZero = count == 0;
		}
	}
}
