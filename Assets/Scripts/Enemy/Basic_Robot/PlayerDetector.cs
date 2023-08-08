using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
	#region Variables
	public bool PlayerInRange => detectedPlayer != null;
	public CharacterController3D detectedPlayer;
	public Health playerHealth = null;
	#endregion

	#region Basic Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<CharacterController3D>())
		{
			detectedPlayer = other.GetComponent<CharacterController3D>();
			playerHealth = other.GetComponent<Health>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<CharacterController3D>())
		{
			StartCoroutine(ClearDetectedPlayerAfterDelay());
		}
	}
	#endregion

	#region Custom Methods
	private IEnumerator ClearDetectedPlayerAfterDelay()
	{
		yield return new WaitForSeconds(3f);
		detectedPlayer = null;
	}

	public Vector3 GetNearestPlayerPosition()
	{
		return detectedPlayer?.transform.position ?? Vector3.zero;
	}
	#endregion
}
