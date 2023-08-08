using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
	#region Variables
	public GameObject logo;
	public float speed = 10f;
	#endregion

	#region Basic Methods
	private void Start()
	{
		StartCoroutine("Rotate");
	}
	#endregion

	#region Custom Methods
	public IEnumerator Rotate()
	{
		LeanTween.rotate(logo, new Vector3(0, 225, 0), speed * Time.deltaTime);

		while (LeanTween.isTweening(logo))
		{
			yield return null;
		}

		LeanTween.rotate(logo, new Vector3(0, 135, 0), speed * Time.deltaTime);

		while (LeanTween.isTweening(logo))
		{
			yield return null;
		}

		StartCoroutine("Rotate");
	}
	#endregion
}
