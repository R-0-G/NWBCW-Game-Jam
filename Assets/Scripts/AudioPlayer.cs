using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	[SerializeField] private List<AudioClip> clips;
	[SerializeField] private AudioSource source;
	public void PlayRandom()
	{
		source.clip = clips[Random.Range(0, clips.Count)];
		source.Play();
	}
}
