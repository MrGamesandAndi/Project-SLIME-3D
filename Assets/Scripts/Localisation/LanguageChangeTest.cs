using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageChangeTest : MonoBehaviour
{
	#region Basic Methods
	private void Update()
	{
		if (Input.GetKey(KeyCode.Alpha1))
		{
			LocalisationSystem.language = LocalisationSystem.Language.English;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (Input.GetKey(KeyCode.Alpha2))
		{
			LocalisationSystem.language = LocalisationSystem.Language.Spanish;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
	#endregion
}
