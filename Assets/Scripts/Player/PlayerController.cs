using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "Extras/PlayerController")]
public class PlayerController : Controller
{
	#region Variables
	public float controlRotationSensitivity = 3.0f;

	private PlayerInput playerInput;
	private PlayerCamera playerCamera;
	#endregion

	#region Custom Methods
	public override void Init()
	{
		playerInput = FindObjectOfType<PlayerInput>();
		playerCamera = FindObjectOfType<PlayerCamera>();
	}

	public override void OnCharacterUpdate()
	{
		playerInput.UpdateInput();
		UpdateControlRotation();
		characterController.SetMovementInput(GetMovementInput());
		characterController.SetJumpInput(playerInput.jumpInput, playerInput.doubleJumpInput);
		characterController.SetAttackInput(playerInput.attackInputB, playerInput.attackInputY, playerInput.attackInputX);
	}

	public override void OnCharacterFixedUpdate()
	{
		playerCamera.SetPosition(characterController.transform.position);
		playerCamera.SetControlRotation(characterController.GetControlRotation());
	}

	private void UpdateControlRotation()
	{
		Vector2 camInput = playerInput.cameraInput;
		Vector2 controlRotation = characterController.GetControlRotation();

		float pitchAngle = controlRotation.x;
		pitchAngle -= camInput.y * controlRotationSensitivity;

		float yawAngle = controlRotation.y;
		yawAngle += camInput.x * controlRotationSensitivity;

		controlRotation = new Vector2(pitchAngle, yawAngle);
		characterController.SetControlRotation(controlRotation);
	}

	private Vector3 GetMovementInput()
	{
		Quaternion yawRotation = Quaternion.Euler(0.0f, characterController.GetControlRotation().y, 0.0f);
		Vector3 forward = yawRotation * Vector3.forward;
		Vector3 right = yawRotation * Vector3.right;
		Vector3 movementInput = (forward * playerInput.moveInput.y + right * playerInput.moveInput.x);

		if (movementInput.sqrMagnitude > 1f)
		{
			movementInput.Normalize();
		}

		return movementInput;
	}
	#endregion
}
