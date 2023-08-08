using UnityEngine;

public class GameManagerToggleMenu : MonoBehaviour
{
	#region Variables
	private GameManagerMaster gameManagerMaster;
	private PlayerInput playerInput;
	public static GameManagerToggleMenu instance;

	public GameObject menu;
	#endregion

	#region Basic Methods
	private void Update()
	{
		CheckForMenuToggleRequest();
	}

	private void OnEnable()
	{
		instance = this;
		SetInitialReferences();
	}
	#endregion

	#region Custom Methods
	private void SetInitialReferences()
	{
		gameManagerMaster = GetComponent<GameManagerMaster>();
		playerInput = FindObjectOfType<PlayerInput>();
	}

	private void CheckForMenuToggleRequest()
	{
		if (playerInput.pauseInput && !gameManagerMaster.isGameOver) 
		{
			ToggleMenu();
		}
	}

	public void ToggleMenu()
	{
		if (menu != null)
		{
			menu.SetActive(!menu.activeSelf);
			gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
			gameManagerMaster.CallEventMenuToggle();
		}
	}
	#endregion
}
