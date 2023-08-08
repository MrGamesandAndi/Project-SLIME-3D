using UnityEngine;

public class GameManagerTogglePause : MonoBehaviour
{
	#region Variables
	private GameManagerMaster gameManagerMaster;
	private bool isPaused;
	public static GameManagerTogglePause instance;
	#endregion

	#region Basic Methods
	private void OnEnable()
	{
		instance = this;
		SetInitialReferences();
		gameManagerMaster.MenuToggleEvent += TogglePause;
	}

	private void OnDisable()
	{
		gameManagerMaster.MenuToggleEvent -= TogglePause;
	}
	#endregion

	#region Custom Methods
	private void SetInitialReferences()
	{
		gameManagerMaster = GetComponent<GameManagerMaster>();
	}

	public void TogglePause()
	{
		if (isPaused)
		{
			Time.timeScale = 1f;
			isPaused = false;
		}
		else
		{
			Time.timeScale = 0f;
			isPaused = true;
		}
	}
	#endregion
}
