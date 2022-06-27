using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLooper : MonoBehaviour
{
	[SerializeField] private AudioClip[] clips;

	[SerializeField] private bool isRandom;
	[SerializeField] private float pauseBreaks;
	[SerializeField] private AudioSource source;

	private int index = -1;

	private void Awake()
	{
		StartCoroutine(StartAudioLoop());
	}

	public IEnumerator StartAudioLoop()
	{
		while (true)
		{
			index = isRandom ? Random.Range(0, clips.Length) : index + 1;
			if (isRandom)
			{
				source.clip = clips[index];
				source.Play();
				yield return new WaitForSeconds(source.clip.length);
				yield return new WaitForSeconds(pauseBreaks);
			}
		}
	}
}
