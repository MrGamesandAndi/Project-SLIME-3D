using UnityEngine;

public class GameManagerMaster : MonoBehaviour
{
	#region Variables
	public delegate void GameManagerEventHandler();
	public delegate void GameManagerEventHandlerFloat(float value);
	public static GameManagerMaster instance;

	public event GameManagerEventHandler MenuToggleEvent;
	public event GameManagerEventHandler RestartLevelEvent;
	public event GameManagerEventHandlerFloat AtomEvent;
	public event GameManagerEventHandler GearEvent;
	public bool isGameOver;
	public bool isMenuOn;
	public bool[] savedComputers = new bool[30];
	public AudioClip levelSong;
	public ScenesManager.SceneIndexes currentScene;
	public DataController dataController;
	public GameObject player;
	public SlimeStates levelPower;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		player.SetActive(true);
		AudioManager.Instance.PlayMusic(levelSong);
	}

	#endregion

	#region Custom Methods
	public void CallEventMenuToggle()
	{
		MenuToggleEvent?.Invoke();
	}

	public void CallEventRestartLevel()
	{
		isGameOver = true;
		RestartLevelEvent?.Invoke();
	}

	public void CallEventAtom(float value)
	{
		AtomEvent?.Invoke(value);
	}

	public void CallEventGear()
	{
		GearEvent?.Invoke();
	}
	#endregion
}
