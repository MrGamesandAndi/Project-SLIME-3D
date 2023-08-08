using UnityEngine;

public class TitleControls : MonoBehaviour
{
	#region Variables
	public AudioClip levelSong;
	#endregion

	#region Basic Methods
	private void Start()
	{
		AudioManager.Instance.PlayMusic(levelSong);
	}
	#endregion

	#region Custom Methods
	public void NewGame()
	{
		ScenesManager.instance.LoadGame();
	}

	public void QuitGame()
	{
		ScenesManager.instance.QuitGame();
	}

	public void Options()
	{

	}
	#endregion
}
