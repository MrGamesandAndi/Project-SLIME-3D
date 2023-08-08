using System.Collections.Generic;
using UnityEngine;

public class CyStore : MonoBehaviour
{
	#region Variables
	public List<string> slimes;
	public List<bool> powers;
	public AudioClip collectSfx;
	#endregion

	#region Basic Methods
	private void Start()
	{
		for (int i = 0; i < powers.Count; i++)
		{
			if (PlayerPrefs.GetInt(slimes[i], 0) == 1)
			{
				powers[i] = true;
			}
			else
			{
				powers[i] = false;
			}
		}
	}
	#endregion

	#region Custom Methods
	private void OnTriggerEnter(Collider other)
	{
		for (int i = 0; i < slimes.Count; i++)
		{
			if (powers[i])
			{
				PlayerPrefs.SetInt(slimes[i]+"_unlocked", 1);
				gameObject.GetComponent<Animator>().SetTrigger("Wave");
				AudioManager.Instance.PlaySfx(collectSfx);
			}
		}
	}
	#endregion
}
