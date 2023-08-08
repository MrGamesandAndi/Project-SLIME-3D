using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
	#region Variables
	[Header("Controllers")]
	public Controller controller;
	public MovementSettings movementSettings;
	public GravitySettings gravitySettings;
	public RotationSettings rotationSettings;
	public Health health;
	public AudioClip jumpSfx;
	public Material normalMat;
	public Material powerMat;
	public GameObject model;
	public bool isUsingPower;

	public Vector3 velocity => unityCharacterController.velocity;
	public Vector3 horizontalVelocity => unityCharacterController.velocity.SetY(0.0f);
	public Vector3 verticalVelocity => unityCharacterController.velocity.Multiply(0.0f, 1.0f, 0.0f);
	public bool isGrounded { get; private set; }
	public bool doubleJumpInput { get; private set; }
	public bool attackInputB { get; private set; }
	public bool attackInputY { get; private set; }
	public bool attackInputX { get; private set; }

	[Header("Attacking tools")]
	public GameObject shootingAmmo;
	public float gunChargeSpeed = 1f;
	public float gunChargeMax = 5f;
	public GameObject shootingZone;
	public float bulletSpeed = 1f;

	private CharacterController unityCharacterController;
	private CharacterAnimator characterAnimator;
	private float targetHorizontalSpeed;
	private float horizontalSpeed;
	private float verticalSpeed;
	private float gunChargeLevel;
	private Vector2 controlRotation;
	private Vector3 movementInput;
	private Vector3 lastMovementInput;
	private bool hasMovementInput;
	private bool jumpInput;
	private bool canDoubleJump;
	private bool isChargingGun;
	private GameObject currentBullet;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		controller.Init();
		controller.characterController = this;
		unityCharacterController = GetComponent<CharacterController>();
		characterAnimator = GetComponent<CharacterAnimator>();
		health = GetComponent<Health>();
		canDoubleJump = false;
		isChargingGun = false;
		currentBullet = null;
	}

	private void Update()
	{
		controller.OnCharacterUpdate();
	}

	private void FixedUpdate()
	{
		UpdateState();
		controller.OnCharacterFixedUpdate();
	}
	#endregion

	#region Custom Methods
	/// <summary>
	/// Applies speed to the player.
	/// </summary>
	private void UpdateState()
	{
		UpdateHorizontalSpeed();
		UpdateVerticalSpeed();
		UpdateGroundpoundAttack();
		UpdateSlimeState();
		UpdateShootingAttack();
		Vector3 movement = horizontalSpeed * GetMovementDirection() + verticalSpeed * Vector3.up;
		unityCharacterController.Move(movement * Time.deltaTime);
		OrientToTargetRotation(movement.SetY(0f));
		isGrounded = unityCharacterController.isGrounded;
		characterAnimator.UpdateState();
	}

	/// <summary>
	/// Checks if the player pressed the Movement buttons.
	/// </summary>
	/// <param name="_movementInput">Axis of the player.</param>
	public void SetMovementInput(Vector3 _movementInput)
	{
		bool _hasMovementInput = movementInput.sqrMagnitude > 0f;

		if (hasMovementInput && !_hasMovementInput)
		{
			lastMovementInput = movementInput;
		}

		movementInput = _movementInput;
		hasMovementInput = _hasMovementInput;
	}

	/// <summary>
	/// Checks if the player pressed the Jump button.
	/// </summary>
	/// <param name="_jumpInput">Player jumped.</param>
	public void SetJumpInput(bool _jumpInput, bool _doubleJumpInput)
	{
		if (!GameManagerMaster.instance.isMenuOn)
		{
			jumpInput = _jumpInput;
			doubleJumpInput = _doubleJumpInput;
		}
	}

	/// <summary>
	/// Checks if the player pressed any Attack buttons.
	/// </summary>
	/// <param name="_attackInput">Player attacked.</param>
	public void SetAttackInput(bool _attackInputB, bool _attackInputY, bool _attackInputX)
	{
		attackInputB = _attackInputB;
		attackInputY = _attackInputY;
		attackInputX = _attackInputX;
	}

	/// <summary>
	/// Returns the pitch and yaw of the controller.
	/// </summary>
	/// <returns>Pitch and yaw</returns>
	public Vector2 GetControlRotation()
	{
		return controlRotation;
	}

	/// <summary>
	/// Calculates the pitch and yaw of the controller.
	/// </summary>
	/// <param name="_controlRotation">Current rotation of the controller.</param>
	public void SetControlRotation(Vector2 _controlRotation)
	{
		float pitchAngle = _controlRotation.x;
		pitchAngle %= 360f;
		pitchAngle = Mathf.Clamp(pitchAngle, rotationSettings.minPitchAngle, rotationSettings.maxPitchAngle);
		float yawAngle = _controlRotation.y;
		yawAngle %= 360f;
		controlRotation = new Vector2(pitchAngle, yawAngle);
	}

	/// <summary>
	/// Updates the horizontal speed.
	/// </summary>
	private void UpdateHorizontalSpeed()
	{
		Vector3 _movementInput = movementInput;

		if (_movementInput.sqrMagnitude > 1f)
		{
			_movementInput.Normalize();
		}

		targetHorizontalSpeed = _movementInput.sqrMagnitude * movementSettings.maxHorizontalSpeed;
		float acceleration = hasMovementInput ? movementSettings.acceleration : movementSettings.disacceleration;
		horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, targetHorizontalSpeed, acceleration * Time.deltaTime);
	}

	/// <summary>
	/// Checks if the groundpound move has been initialized.
	/// </summary>
	private void UpdateGroundpoundAttack()
	{
		if (!isGrounded && attackInputB)
		{
			horizontalSpeed = 0f;
			verticalSpeed = 0f;
			verticalSpeed = -movementSettings.jumpAbortSpeed;
		}
	}

	private void UpdateSlimeState()
	{
		if (PlayerPrefs.GetInt(GameManagerMaster.instance.levelPower.ToString() + "_unlocked", 0) == 1)
		{
			if (attackInputX && GameManagerCollectables.instance.currentAtomCounter > 0)
			{
				isUsingPower = true;
				model.GetComponent<Renderer>().material = powerMat;
				GameManagerCollectables.instance.CollectAtom(-1 * Time.deltaTime);
			}
			else
			{
				isUsingPower = false;
				model.GetComponent<Renderer>().material = normalMat;
			}
		}
	}

	/// <summary>
	/// Checks if the Player is double jumping.
	/// </summary>
	private void UpdateDoubleJump()
	{
		if (canDoubleJump && doubleJumpInput && !isGrounded)
		{
			canDoubleJump = false;
			verticalSpeed = 0f;
			verticalSpeed = movementSettings.jumpSpeed;
			AudioManager.Instance.PlaySfx(jumpSfx);
		}
	}

	/// <summary>
	/// Checks if the Player has initialize the charging attack.
	/// </summary>
	private void UpdateShootingAttack()
	{
		if (currentBullet == null)
		{
			if (attackInputB && isGrounded)
			{
				isChargingGun = true;

				if (gunChargeLevel <= gunChargeMax)
				{
					gunChargeLevel += Time.deltaTime * gunChargeSpeed;
				}
				else
				{
					ReleaseChargedBullet(gunChargeLevel);
					gunChargeLevel = 0f;
					isChargingGun = false;
				}

			}
			else if (!attackInputB && isChargingGun)
			{
				ReleaseChargedBullet(gunChargeLevel);
				gunChargeLevel = 0f;
				isChargingGun = false;
			}
		}
	}

	private void ReleaseChargedBullet(float gunChargeLevel)
	{
		currentBullet = Instantiate(shootingAmmo, shootingZone.transform.position, shootingZone.transform.rotation, null);
		currentBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed,ForceMode.Impulse);
		Destroy(currentBullet, gunChargeLevel);
	}

	/// <summary>
	/// Updates the vertical speed.
	/// </summary>
	private void UpdateVerticalSpeed()
	{
		if (isGrounded)
		{
			verticalSpeed = -gravitySettings.groundedGravity;

			if (jumpInput)
			{
				verticalSpeed = movementSettings.jumpSpeed;
				AudioManager.Instance.PlaySfx(jumpSfx);
				isGrounded = false;
				canDoubleJump = true;
			}
		}
		else
		{
			UpdateDoubleJump();

			if (!jumpInput)
			{
				verticalSpeed = Mathf.MoveTowards(verticalSpeed, -gravitySettings.maxFallSpeed, movementSettings.jumpAbortSpeed * Time.deltaTime);
			}

			verticalSpeed = Mathf.MoveTowards(verticalSpeed, -gravitySettings.maxFallSpeed, gravitySettings.gravity * Time.deltaTime);
		}
	}

	/// <summary>
	/// Returns the direction of the movement.
	/// </summary>
	/// <returns>Movement direction.</returns>
	private Vector3 GetMovementDirection()
	{
		Vector3 moveDir = hasMovementInput ? movementInput : lastMovementInput;

		if (moveDir.sqrMagnitude > 1f)
		{
			moveDir.Normalize();
		}

		return moveDir;
	}

	/// <summary>
	/// Rotates player to a calculated rotation.
	/// </summary>
	/// <param name="horizontalMovement">Horizontal movement of the player.</param>
	private void OrientToTargetRotation(Vector3 horizontalMovement)
	{
		if (rotationSettings.OrientRotationToMovement && horizontalMovement.sqrMagnitude > 0f)
		{
			float rotationSpeed = Mathf.Lerp(rotationSettings.maxRotationSpeed, rotationSettings.minRotationSpeed, horizontalSpeed / targetHorizontalSpeed);
			Quaternion targetRotation = Quaternion.LookRotation(horizontalMovement, Vector3.up);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
		else if (rotationSettings.UseControlRotation)
		{
			Quaternion targetRotation = Quaternion.Euler(0f, controlRotation.y, 0f);
			transform.rotation = targetRotation;
		}
	}
	#endregion
}

[System.Serializable]
public class MovementSettings
{
	#region Variables
	public float acceleration = 25f;
	public float disacceleration = 25f;
	public float maxHorizontalSpeed = 8f;
	public float jumpSpeed = 10f;
	public float jumpAbortSpeed = 10f;
	#endregion
}

[System.Serializable]
public class GravitySettings
{
	#region Variables
	public float gravity = 20f;
	public float groundedGravity = 5f;
	public float maxFallSpeed = 40f;
	#endregion
}

[System.Serializable]
public class RotationSettings
{
	#region Variables
	[Header("Control Rotation")]
	public float minPitchAngle = -45f;
	public float maxPitchAngle = 75f;
	[Header("Character Orientation")]
	public float minRotationSpeed = 600f;
	public float maxRotationSpeed = 1200f;
	public bool UseControlRotation { get { return useControlRotation; } set { SetUseControlRotation(value); } }
	public bool OrientRotationToMovement { get { return orientRotationToMovement; } set { SetOrientRotationToMovement(value); } }

	[SerializeField] private bool useControlRotation = false;
	[SerializeField] private bool orientRotationToMovement = false;
	#endregion

	#region Custom Methods
	/// <summary>
	/// Enables the player to control the camera.
	/// </summary>
	/// <param name="_useControlRotation">Player is controlling camera.</param>
	private void SetUseControlRotation(bool _useControlRotation)
	{
		useControlRotation = _useControlRotation;
		orientRotationToMovement = !useControlRotation;
	}

	/// <summary>
	/// Disables controls of camera.
	/// </summary>
	/// <param name="_orientRotationToMovement">Player is controlling camera.</param>
	private void SetOrientRotationToMovement(bool _orientRotationToMovement)
	{
		orientRotationToMovement = _orientRotationToMovement;
		useControlRotation = !orientRotationToMovement;
	}
	#endregion
}

