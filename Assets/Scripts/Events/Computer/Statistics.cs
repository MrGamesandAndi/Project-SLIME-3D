using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
	#region Variables
	public int id;
	private Image image;
	#endregion

	#region Basic Methods
	private void OnEnable()
	{
		image = GetComponent<Image>();

		if (PlayerPrefs.GetInt("Computer" + id) != 0)
		{
			image.color = new Color(255, 255, 255, 255);
		}
		else
		{
			image.color = new Color(255, 255, 255, 0);
		}
	}
	#endregion
}
