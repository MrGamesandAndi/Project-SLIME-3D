using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
	#region Variables
	public TabGroup tabGroup;
	public Image background;
	public TextMeshProUGUI text;
	#endregion

	#region Basic Methods
	private void Start()
	{
		background = GetComponent<Image>();
		text = GetComponentInChildren<TextMeshProUGUI>();
		tabGroup.Subscribe(this);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		tabGroup.OnTabSelected(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tabGroup.OnTabExit(this);
	}
	#endregion
}
