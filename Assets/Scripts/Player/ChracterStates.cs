using UnityEngine;

public class ChracterStates : MonoBehaviour
{
	#region Variables
	private CharacterController3D characterController;
	#endregion

	#region Basic Methods
	private void Start()
	{
		characterController = FindObjectOfType<CharacterController3D>();
	}
	#endregion

	#region Custom Methods
	public void EnableMovement()
	{
		characterController.enabled = true;
	}

	public void DisableMovement()
	{
		characterController.enabled = false;
	}

	public void Restart()
	{
		GameManagerMaster.instance.CallEventRestartLevel();
	}
	#endregion
}
