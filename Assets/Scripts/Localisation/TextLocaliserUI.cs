using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliserUI : MonoBehaviour
{
	#region Variables
	private TextMeshProUGUI textField;

	public LocalisedString localisedString;
	#endregion

	#region Basic Methods
	private void Start()
	{
		textField = GetComponent<TextMeshProUGUI>();
		textField.text = localisedString.value;
	}

	public void Update()
	{
		textField = GetComponent<TextMeshProUGUI>();
		textField.text = localisedString.value;
	}
	#endregion
}
