using UnityEngine;

public enum ButtonType
{
	RESUME,
	SAVE,
	QUIT,
	NEW_GAME,
	QUIT_GAME,
	OPTIONS,
	LANGUAGE,
	DELETE_ALL,
	CREDITS,
	TITLE
}

public class MenuControls : MonoBehaviour
{
	#region Variables
	public ButtonType type;
	public GameObject title;
	public GameObject options;
	public TextLocaliserUI textLocaliserUI;
	public ScenesManager.SceneIndexes quitTo;
	public GameObject warning;

	private PlayerInput playerInput;
	#endregion

	#region Basic Methods
	private void Start()
	{
		playerInput = FindObjectOfType<PlayerInput>();
	}
	#endregion

	#region Custom Methods
	public void SetAction()
	{
		switch (type)
		{
			case ButtonType.RESUME:
				Resume();
				break;
			case ButtonType.SAVE:
				Save();
				break;
			case ButtonType.QUIT:
				Quit();
				break;
			case ButtonType.NEW_GAME:
				NewGame();
				break;
			case ButtonType.QUIT_GAME:
				QuitGame();
				break;
			case ButtonType.OPTIONS:
				OpenOptions();
				break;
			case ButtonType.LANGUAGE:
				ChangeLanguage();
				break;
			case ButtonType.DELETE_ALL:
				DeleteAllProgress();
				break;
			case ButtonType.CREDITS:
				Credits();
				break;
			case ButtonType.TITLE:
				Title();
				break;
			default:
				break;
		}
	}
	public void Resume()
	{
		GameManagerTogglePause.instance.TogglePause();
		GameManagerToggleMenu.instance.ToggleMenu();
		GameManagerMaster.instance.CallEventMenuToggle();
	}

	public void Save()
	{
		GameManagerMaster.instance.dataController.TotalAtoms = GameManagerCollectables.instance.GetTotalAtomQuantity();
	}

	public void Quit()
	{
		GameManagerMaster.instance.dataController.ActiveCheckpoint = 0;
		Resume();
		ScenesManager.instance.LoadLevel(GameManagerMaster.instance.currentScene, quitTo);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void NewGame()
	{
		ScenesManager.instance.LoadGame();
	}

	public void Title()
	{
		ScenesManager.instance.LoadLevel(ScenesManager.SceneIndexes.CREDITS, ScenesManager.SceneIndexes.TITLE_SCREEN);
	}

	public void Credits()
	{
		ScenesManager.instance.LoadLevel(ScenesManager.SceneIndexes.TITLE_SCREEN, ScenesManager.SceneIndexes.CREDITS);
	}

	public void OpenOptions()
	{
		title.SetActive(false);
		options.SetActive(true);
	}

	public void CloseOptions()
	{
		title.SetActive(true);
		options.SetActive(false);
	}

	public void ChangeLanguage()
	{
		if (LocalisationSystem.language == LocalisationSystem.Language.English)
		{
			LocalisationSystem.SetLanguage(LocalisationSystem.Language.Spanish);
			textLocaliserUI.localisedString = "language_spanish";
		}
		else
		{
			LocalisationSystem.SetLanguage(LocalisationSystem.Language.English);
			textLocaliserUI.localisedString = "language_english";
		}
	}

	public void DeleteAllProgress()
	{
		warning.SetActive(true);
	}
	#endregion
}
