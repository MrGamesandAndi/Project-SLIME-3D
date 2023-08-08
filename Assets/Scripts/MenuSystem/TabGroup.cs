using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
	#region Variables
	public List<TabButton> tabButtons;
	public Color tabIdle;
	public Color tabActive;
	public Color textIdle;
	public Color TextActive;
	public TabButton selectedTab;
	public PanelGroup panelGroup;

	private PlayerInput playerInput;
	#endregion

	#region Basic Methods
	private void Start()
	{
		playerInput = FindObjectOfType<PlayerInput>();
	}

	private void Update()
	{
		if (playerInput.menuRightInput)
		{
			OnTabExit(selectedTab);

			if((tabButtons.IndexOf(selectedTab) + 1) > (tabButtons.Count - 1))
			{
				SelectByIndex(0);
			}
			else
			{
				SelectByIndex(tabButtons.IndexOf(selectedTab) + 1);
			}
		}

		if (playerInput.menuLeftInput)
		{
			OnTabExit(selectedTab);

			if ((tabButtons.IndexOf(selectedTab) - 1) == -1)
			{
				SelectByIndex(3);
			}
			else
			{
				SelectByIndex(tabButtons.IndexOf(selectedTab) - 1);
			}
		}
	}
	#endregion

	#region Custom Methods
	public void Subscribe(TabButton button)
	{
		if (tabButtons == null)
		{
			tabButtons = new List<TabButton>();
		}

		tabButtons.Add(button);
	}

	public void OnTabExit(TabButton button)
	{
		ResetTabs();
	}

	public void SelectByIndex(int button)
	{
		selectedTab = tabButtons[button];
		ResetTabs();
		selectedTab.background.color = tabActive;
		selectedTab.text.color = TextActive;
		LeanTween.scale(selectedTab.text.gameObject, new Vector3(2f, 2f, 2f), 0.1f).setIgnoreTimeScale(true);
		LeanTween.scale(selectedTab.text.gameObject, new Vector3(1f, 1f, 1f), 0.1f).setIgnoreTimeScale(true);

		if (panelGroup != null)
		{
			panelGroup.SetPageIndex(selectedTab.transform.GetSiblingIndex());
		}
	}

	public void OnTabSelected(TabButton button)
	{
		selectedTab = button;
		ResetTabs();
		button.background.color = tabActive;
		button.text.color = TextActive;
		LeanTween.scale(button.text.gameObject, new Vector3(2f, 2f, 2f), 0.1f).setIgnoreTimeScale(true);
		LeanTween.scale(button.text.gameObject, new Vector3(1f, 1f, 1f), 0.1f).setIgnoreTimeScale(true);

		if (panelGroup != null)
		{
			panelGroup.SetPageIndex(button.transform.GetSiblingIndex());
		}
	}

	public void ResetTabs()
	{
		foreach (TabButton button in tabButtons)
		{
			if (selectedTab != null && button == selectedTab)
			{
				continue;
			}

			button.background.color = tabIdle;
			button.text.color = textIdle;
		}
	}
	#endregion
}
