using UnityEngine;

public class Teleporter : MonoBehaviour
{
	#region Variables
	public ScenesManager.SceneIndexes teleportScene;
	public GameObject blocker;
	public int minComputersToUnlock = 0;
	#endregion

	#region Basic Methods

	private void Start()
	{
		if (PlayerPrefs.GetInt("TotalComputers", 0) >= minComputersToUnlock)
		{
			blocker.SetActive(false);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		ScenesManager.instance.LoadLevel(ScenesManager.SceneIndexes.HUB, teleportScene);
	}
	#endregion
}
