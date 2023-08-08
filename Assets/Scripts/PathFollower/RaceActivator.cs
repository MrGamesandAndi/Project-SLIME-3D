using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceActivator : MonoBehaviour
{
	#region Variables
	public FollowPath followPath;
	public GameObject computer;
	public AudioClip raceMusic;
	#endregion

	#region Basic Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			AudioManager.Instance.PlayMusicWithCrossFade(raceMusic);
			gameObject.GetComponent<SphereCollider>().enabled = false;
			followPath.enabled = true;
			computer.SetActive(true);
		}
	}
	#endregion

	#region Custom Methods

	#endregion
}
