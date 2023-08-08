using UnityEngine;

public class PanelGroup : MonoBehaviour
{
	#region Variables
	public GameObject[] panels;
	public TabGroup tabGroup;
	public int panelIndex;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		ShowCurrentPanel();	
	}
	#endregion

	#region Custom Methods
	private void ShowCurrentPanel()
	{
		for (int i = 0; i < panels.Length; i++)
		{
			if (i == panelIndex)
			{
				panels[i].gameObject.SetActive(true);
				LeanTween.scaleX(panels[i].gameObject, 1f, 0.3f).setIgnoreTimeScale(true);
			}
			else
			{
				LeanTween.scaleX(panels[i].gameObject, 0f, 0.3f).setIgnoreTimeScale(true);
				panels[i].gameObject.SetActive(false);
			}
		}
	}

	public void SetPageIndex(int index)
	{
		panelIndex = index;
		ShowCurrentPanel();
	}
	#endregion
}
