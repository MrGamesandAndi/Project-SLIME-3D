using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
	#region Variables
	private Animator animator;
	private CharacterController3D characterController;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		characterController = GetComponent<CharacterController3D>();
	}
	#endregion

	#region Custom Methods
	/// <summary>
	/// Updates the animation states
	/// </summary>
	public void UpdateState()
	{
		float normHorizontalSpeed = characterController.horizontalVelocity.magnitude / characterController.movementSettings.maxHorizontalSpeed;
		animator.SetFloat(CharacterAnimatorParamId.horizontalSpeed, normHorizontalSpeed);
		float jumpSpeed = characterController.movementSettings.jumpSpeed;
		float normVerticalSpeed = characterController.verticalVelocity.y.Remap(-jumpSpeed, jumpSpeed, -1.0f, 1.0f);
		animator.SetFloat(CharacterAnimatorParamId.verticalSpeed, normVerticalSpeed);
		animator.SetBool(CharacterAnimatorParamId.isGrounded, characterController.isGrounded);
		animator.SetBool(CharacterAnimatorParamId.doubleJump, characterController.doubleJumpInput);
		animator.SetBool(CharacterAnimatorParamId.attackY, characterController.attackInputY);
		animator.SetBool(CharacterAnimatorParamId.attackB, characterController.attackInputB);
		animator.SetBool(CharacterAnimatorParamId.death, characterController.health.isDead);
	}
	#endregion
}

public static class CharacterAnimatorParamId
{
	#region Variables
	public static readonly int horizontalSpeed = Animator.StringToHash("horizontalSpeed");
	public static readonly int verticalSpeed = Animator.StringToHash("verticalSpeed");
	public static readonly int isGrounded = Animator.StringToHash("isGrounded");
	public static readonly int attackY = Animator.StringToHash("attackY");
	public static readonly int attackB = Animator.StringToHash("attackB");
	public static readonly int doubleJump = Animator.StringToHash("doubleJump");
	public static readonly int death = Animator.StringToHash("death");
	#endregion
}

