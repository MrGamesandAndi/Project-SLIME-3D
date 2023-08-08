using UnityEngine;

public class ButtonTriggerArea : MonoBehaviour
{
	#region Basic Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			gameObject.GetComponent<SphereCollider>().enabled = false;
			TimerEvent.current.TimerTriggerEnter();
		}
	}
	#endregion
}
