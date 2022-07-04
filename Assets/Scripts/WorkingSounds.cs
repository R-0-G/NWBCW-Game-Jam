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
	int lastIndex = 0;
	bool wasZero = true;
	int index = 0;

	private void Awake()
	{
		noWorking.TransitionTo(0f);
	}

	private void replay()
	{
		source.clip = workingClips[index];
		source.Play();
	}

	private void Update()
	{
		int count = manager.List.Where(x => x.StateMachine.CurrentState == HumanStateMachine.State.WORKING).Count();
		index = Mathf.Clamp(count, 1, workingClips.Length) - 1;

		if (count != lastHumansCuont)
		{
			if (lastIndex != index || !source.isPlaying)
			{
				source.Stop();
				CancelInvoke();
				if (count > 0)
				{
					if (wasZero)
					{
						someWorking.TransitionTo(fadeTime);
						source.clip = workingClips[index];
						source.Play();
						Invoke("replay", source.clip.length);
					}
					if (index != lastIndex)
					{
						source.clip = workingClips[index];
						source.Play();
						Invoke("replay", source.clip.length);
					}
				}
				else
				{
					if (!wasZero)
					{
						noWorking.TransitionTo(fadeTime);
					}
				}
				lastIndex = index;

			}
			lastHumansCuont = count;
			wasZero = count == 0;
		}
	}
}
