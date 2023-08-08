using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
	#region Variables
	private TextLocaliserUI textLocaliserUI;
	#endregion

	#region Basic Methods
	private void Start()
	{
		textLocaliserUI = transform.GetComponent<TextLocaliserUI>();
	}
	#endregion

	#region Custom Methods
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
	#endregion
}
