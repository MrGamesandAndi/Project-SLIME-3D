using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiaController : MonoBehaviour
{
	#region Variables
	public GameObject computer;

	private SphereCollider trigger;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		trigger = GetComponent<SphereCollider>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			GameManagerCollectables.instance.CollectBolt();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if(GameManagerCollectables.instance.currentBoltCounter >= 5)
			{
				GameManagerCollectables.instance.DeleteBolts();
				trigger.enabled = false;
				computer.SetActive(true);
			}
		}
	}
	#endregion

	#region Custom Methods

	#endregion
}
