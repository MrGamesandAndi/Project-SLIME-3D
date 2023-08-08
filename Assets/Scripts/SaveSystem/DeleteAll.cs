using UnityEngine;

public class DeleteAll : MonoBehaviour
{
	#region Custom Methods
	public void Cancel()
	{
		gameObject.SetActive(false);
	}

	public void Delete()
	{
		PlayerPrefs.DeleteAll();
		ScenesManager.instance.ReloadScene(ScenesManager.SceneIndexes.TITLE_SCREEN);
	}
	#endregion
}
