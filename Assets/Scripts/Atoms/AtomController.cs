using UnityEngine;

public class AtomController : MonoBehaviour
{
	#region Variables
	public int value = 0;
	public AudioClip collectSfx;
	#endregion

	#region Basic Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			AudioManager.Instance.PlaySfx(collectSfx);
			gameObject.GetComponent<SphereCollider>().enabled = false;
			GameManagerMaster.instance.CallEventAtom(value);
			Destroy(gameObject);
		}
	}
	#endregion
}
