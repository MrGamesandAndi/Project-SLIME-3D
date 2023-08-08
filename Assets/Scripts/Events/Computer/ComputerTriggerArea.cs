using UnityEngine;

public class ComputerTriggerArea : MonoBehaviour
{
	#region Variables
	public int id;
	public bool isComputerFromRace = false;
	#endregion

	#region Basic Methods
	private void Start()
	{
		if (PlayerPrefs.GetInt("Computer" + id) == 1)
		{
			gameObject.GetComponent<BoxCollider>().enabled = false;
			GameManagerMaster.instance.savedComputers[id] = true;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponentInChildren<Animator>().SetTrigger("computerRecovered");
			gameObject.GetComponent<BoxCollider>().enabled = false;
			ComputerEvent.current.ComputerTriggerEnter(id);
			PlayerPrefs.SetInt("Computer" + id, 1);
			PlayerPrefs.SetInt("TotalComputers", PlayerPrefs.GetInt("TotalComputers", 0) + 1);

			if (isComputerFromRace)
			{
				AudioManager.Instance.PlayMusicWithCrossFade(GameManagerMaster.instance.levelSong);
				FindObjectOfType<FollowPath>().gameObject.SetActive(false);
			}
		}

		if (other.CompareTag("RoboHal"))
		{
			FindObjectOfType<FollowPath>().gameObject.SetActive(false);
			gameObject.GetComponent<BoxCollider>().enabled = false;
			GameObject.Find("Player").GetComponentInChildren<Health>().Die();
		}
	}
	#endregion
}
