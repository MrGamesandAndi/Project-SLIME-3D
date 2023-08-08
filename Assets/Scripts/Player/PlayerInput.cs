using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	#region Variables
	public float moveAxisDeadZone = 0.2f;
	public Vector2 moveInput { get; private set; }
	public Vector2 lastMoveInput { get; private set; }
	public Vector2 cameraInput { get; private set; }
	public bool jumpInput { get; private set; }
	public bool doubleJumpInput { get; private set; }
	public bool hasMoveInput { get; private set; }
	public bool attackInputB { get; private set; }
	public bool attackInputX { get; private set; }
	public bool attackInputY { get; private set; }
	public bool pauseInput { get; private set; }
	public bool menuRightInput { get; private set; }
	public bool menuLeftInput { get; private set; }
	#endregion

	#region Custom Methods
	/// <summary>
	/// Controls player inputs.
	/// </summary>
	public void UpdateInput()
	{
		Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if (Mathf.Abs(moveInput.x) < moveAxisDeadZone)
		{
			moveInput.x = 0.0f;
		}

		if (Mathf.Abs(moveInput.y) < moveAxisDeadZone)
		{
			moveInput.y = 0.0f;
		}

		bool hasMoveInput = moveInput.sqrMagnitude > 0.0f;

		if (this.hasMoveInput && !hasMoveInput)
		{
			lastMoveInput = this.moveInput;
		}

		this.moveInput = moveInput;
		this.hasMoveInput = hasMoveInput;
		cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		jumpInput = Input.GetButtonDown("Jump");
		doubleJumpInput = Input.GetButtonDown("Jump");
		attackInputB = Input.GetButton("Fire2");
		attackInputY = Input.GetButtonDown("Fire3");
		attackInputX = Input.GetButton("Fire1");
		pauseInput = Input.GetButtonDown("Pause");
		menuLeftInput = Input.GetButtonDown("MenuLeftInput");
		menuRightInput = Input.GetButtonDown("MenuRightInput");
	}
	#endregion
}
