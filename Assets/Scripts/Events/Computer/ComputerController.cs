using UnityEngine;

public class ComputerController : MonoBehaviour
{
	#region Variables
	public int levelId;
	public bool isSaved = false;
	public bool isMaster = false;
	#endregion

	#region Basic Methods
	private void Start()
	{
		ComputerEvent.current.OnComputerTriggerEnter += OnComputerEnter;
	}

	private void OnDestroy()
	{
		ComputerEvent.current.OnComputerTriggerEnter -= OnComputerEnter;
	}
	#endregion

	#region Custom Methods
	private void OnComputerEnter(int id)
	{
		if (id == levelId)
		{
			GameManagerMaster.instance.savedComputers[id] = true;

			if (isMaster)
			{
				ScenesManager.instance.LoadLevel(GameManagerMaster.instance.currentScene, ScenesManager.SceneIndexes.CREDITS);
			}
		}
	}
	#endregion
}
