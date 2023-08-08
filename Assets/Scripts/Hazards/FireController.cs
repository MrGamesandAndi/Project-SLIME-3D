using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
	#region Variables
	public GameObject particles;
	public bool despawnsAfterUse = true;
	#endregion

	#region Basic Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (other.GetComponent<CharacterController3D>().isUsingPower)
			{
				if (despawnsAfterUse)
				{
					gameObject.GetComponent<BoxCollider>().enabled = false;
					particles.SetActive(false);
				}
			}
			else
			{
				other.GetComponent<Health>().Die();
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (!other.GetComponent<CharacterController3D>().isUsingPower)
			{
				other.GetComponent<Health>().Die();
			}
		}
	}
	#endregion

	#region Custom Methods

	#endregion
}
