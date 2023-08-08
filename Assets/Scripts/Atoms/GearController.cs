using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
	#region Variables
	public AudioClip collectSfx;
	#endregion

	#region Basic Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			AudioManager.Instance.PlaySfx(collectSfx, 5f);
			gameObject.GetComponent<BoxCollider>().enabled = false;
			GameManagerMaster.instance.CallEventGear();
			Destroy(gameObject);
		}
	}
	#endregion
}
