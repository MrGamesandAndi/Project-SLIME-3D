using System.Collections.Generic;
using UnityEngine;

public class ButtonNavigation : MonoBehaviour
{
	#region Variables
	public List<GameObject> buttons;
	public int currentButton = 0;

	private PlayerInput playerInput;
	private bool isAxisInUse = false;
	#endregion

	#region Basic Methods
	private void Start()
	{
		playerInput = FindObjectOfType<PlayerInput>();
		LeanTween.scale(buttons[currentButton].gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f).setIgnoreTimeScale(true);
	}
	private void Update()
	{
		if (playerInput.jumpInput)
		{
			LeanTween.scale(buttons[currentButton].gameObject, new Vector3(-1.2f, -1.2f, -1.2f), 0.1f).setIgnoreTimeScale(true);
			LeanTween.scale(buttons[currentButton].gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f).setIgnoreTimeScale(true);
			buttons[currentButton].GetComponentInChildren<MenuControls>().SetAction();
		}

		if (Input.GetAxisRaw("Vertical") != 0)
		{
			if (isAxisInUse == false)
			{
				SelectButton(Input.GetAxisRaw("Vertical"));
				isAxisInUse = true;
			}
		}

		if (Input.GetAxisRaw("Vertical") == 0)
		{
			isAxisInUse = false;
		}
	}
	#endregion

	#region Custom Methods
	public void SelectButton(float isUp)
	{
		if (isUp < 0)
		{
			if (currentButton == buttons.Count - 1)
			{
				LeanTween.scale(buttons[buttons.Count - 1].gameObject, new Vector3(1f, 1f, 1f), 0.1f).setIgnoreTimeScale(true);
				currentButton = 0;
			}
			else
			{
				LeanTween.scale(buttons[currentButton].gameObject, new Vector3(1f, 1f, 1f), 0.1f).setIgnoreTimeScale(true);
				currentButton++;				
			}
		}

		if (isUp > 0)
		{
			if (currentButton == 0)
			{
				LeanTween.scale(buttons[0].gameObject, new Vector3(1f, 1f, 1f), 0.1f).setIgnoreTimeScale(true);
				currentButton = buttons.Count - 1;
			}
			else
			{
				LeanTween.scale(buttons[currentButton].gameObject, new Vector3(1f, 1f, 1f), 0.1f).setIgnoreTimeScale(true);
				currentButton--;
			}
		}

		LeanTween.scale(buttons[currentButton].gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f).setIgnoreTimeScale(true);
	}
	#endregion
}
