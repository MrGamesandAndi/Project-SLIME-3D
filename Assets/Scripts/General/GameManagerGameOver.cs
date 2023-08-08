using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerGameOver : MonoBehaviour
{
	#region Variables
	private GameManagerMaster gameManagerMaster;
	#endregion

	#region Basic Methods
	private void OnEnable()
	{
		SetInitialReferences();
		gameManagerMaster.RestartLevelEvent += RestartScene;
	}

	private void OnDisable()
	{
		gameManagerMaster.RestartLevelEvent -= RestartScene;
	}
	#endregion

	#region Custom Methods
	private void SetInitialReferences()
	{
		gameManagerMaster = GetComponent<GameManagerMaster>();
	}

	public void RestartScene()
	{
		ScenesManager.instance.ReloadScene(gameManagerMaster.currentScene);
	}
	#endregion
}
