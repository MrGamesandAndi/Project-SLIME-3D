using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfFarAway : MonoBehaviour
{
	#region Variables
	private GameObject itemActivatorObject;
	private ItemActivator activationScript;
	#endregion

	#region Basic Methods
	private void Start()
	{
		itemActivatorObject = GameObject.Find("ItemActivatorObject");
		activationScript = itemActivatorObject.GetComponent<ItemActivator>();
		StartCoroutine("AddToList");
	}
	#endregion

	#region Custom Methods
	private IEnumerator AddToList()
	{
		yield return new WaitForSeconds(0.1f);
		activationScript.activatorItems.Add(new ActivatorItem { item = gameObject, itemPosition = transform.position});
	}
	#endregion
}
