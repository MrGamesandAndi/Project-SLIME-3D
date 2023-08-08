using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
	public enum SceneIndexes
	{
		MANAGER = 0,
		TITLE_SCREEN = 1,
		HUB = 2,
		SALA_PRODUCCION = 3,
		SALA_FRIO = 4,
		SALA_BASURA = 5,
		SALA_AGUA = 6,
		SALA_TELETRANSPORTE = 7,
		CREDITS = 8
	}
	#region Variables
	public static ScenesManager instance;
	public GameObject loadingScreen;

	private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
	#endregion

	#region Basic Methods
	private void Awake()
	{
		instance = this;

		SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
	}
	#endregion

	#region Custom Methods
	public void LoadGame()
	{
		loadingScreen.gameObject.SetActive(true);
		scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
		scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.HUB, LoadSceneMode.Additive));
		StartCoroutine(GetSceneLoadProgress());
	}

	public void LoadLevel(SceneIndexes currentScene, SceneIndexes sceneToLoad)
	{
		loadingScreen.gameObject.SetActive(true);
		scenesLoading.Add(SceneManager.UnloadSceneAsync((int)currentScene));
		scenesLoading.Add(SceneManager.LoadSceneAsync((int)sceneToLoad, LoadSceneMode.Additive));
		StartCoroutine(GetSceneLoadProgress());
	}

	public void ReloadScene(SceneIndexes currentScene)
	{
		loadingScreen.gameObject.SetActive(true);
		scenesLoading.Add(SceneManager.UnloadSceneAsync((int)currentScene));
		scenesLoading.Add(SceneManager.LoadSceneAsync((int)currentScene, LoadSceneMode.Additive));
		StartCoroutine(GetSceneLoadProgress());
	}

	public IEnumerator GetSceneLoadProgress()
	{
		for (int i = 0; i < scenesLoading.Count; i++)
		{
			while (!scenesLoading[i].isDone)
			{
				yield return null;
			}
		}

		loadingScreen.gameObject.SetActive(false);
	}

	public string ReturnLevelName()
	{
		return SceneManager.GetActiveScene().name;
	}

	public void QuitGame()
	{
		Application.Quit();
	}
	#endregion
}
