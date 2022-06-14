using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
	[SerializeField] private string[] loadScenes;
	[SerializeField] private string[] unloadSceneNames;
	[SerializeField] private bool loadOnStart;
	[SerializeField] private string singleScene;
	public void Load()
	{
		if (!string.IsNullOrWhiteSpace(singleScene))
		{
			SceneManager.LoadSceneAsync(singleScene, LoadSceneMode.Single);
		}
		else
		{
			for (int i = 0; i < loadScenes.Length; i++)
			{
				SceneManager.LoadSceneAsync(loadScenes[i], LoadSceneMode.Additive);
			}
			for (int i = 0; i < unloadSceneNames.Length; i++)
			{
				SceneManager.UnloadSceneAsync(unloadSceneNames[i]);
			}
		}
	}

	private void Start()
	{
		if (loadOnStart)
		{
			Load();
		}
	}
}
