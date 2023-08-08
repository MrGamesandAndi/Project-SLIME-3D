using UnityEngine;

public class CheckpointTriggerArea : MonoBehaviour
{
	#region Variables
	public int checkpointId;
	#endregion

	#region Basic Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GameManagerMaster.instance.dataController.ActiveCheckpoint = checkpointId;
			ResetCheckpoint(false);
		}
	}
	#endregion

	public void ResetCheckpoint(bool state)
	{
		gameObject.GetComponent<SphereCollider>().enabled = state;
	}
}
