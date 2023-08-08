using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWater : MonoBehaviour
{
	#region Variables
	private GameObject pressZone;
	public GameObject water;
	public Transform stopPoint;
	public float timer;
	#endregion

	#region Basic Methods
	private void Start()
	{
		pressZone = transform.Find("Press_Zone").gameObject;
	}
	#endregion

	#region Custom Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			pressZone.transform.position = new Vector3(pressZone.transform.position.x, pressZone.transform.position.y - 0.29f, pressZone.transform.position.z);
			LeanTween.moveY(water, stopPoint.position.y, timer * Time.deltaTime);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			pressZone.transform.position = new Vector3(pressZone.transform.position.x, pressZone.transform.position.y + 0.29f, pressZone.transform.position.z);
		}
	}
	#endregion
}
