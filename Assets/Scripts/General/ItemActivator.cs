using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivator : MonoBehaviour
{
	#region Variables
	public int distanceFromPlayer;

	private GameObject player;
	public List<ActivatorItem> activatorItems;
	#endregion

	#region Basic Methods
	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		activatorItems = new List<ActivatorItem>();

		StartCoroutine("CheckActivation");
	}
	#endregion

	#region Custom Methods
	private IEnumerator CheckActivation()
	{
		List<ActivatorItem> removeList = new List<ActivatorItem>();

		if (activatorItems.Count > 0)
		{
			foreach (ActivatorItem item in activatorItems)
			{
				if (Vector3.Distance(player.transform.position, item.itemPosition) > distanceFromPlayer)
				{
					if (item.item == null)
					{
						removeList.Add(item);
					}
					else
					{
						item.item.SetActive(false);
					}
				}
				else
				{
					if (item.item == null)
					{
						removeList.Add(item);
					}
					else
					{
						item.item.SetActive(true);
					}
				}
			}
		}
		yield return new WaitForSeconds(0.01f);

		if(removeList.Count > 0)
		{
			foreach (ActivatorItem item in removeList)
			{
				activatorItems.Remove(item);
			}
		}

		yield return new WaitForSeconds(0.01f);
		StartCoroutine("CheckActivation");
	}
	#endregion
}

public class ActivatorItem
{
	#region Variables
	public GameObject item;
	public Vector3 itemPosition;
	#endregion
}
