using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
	#region Variables
	private readonly BasicRobotController _basicRobotController;
	private readonly PlayerDetector _playerDetector;
	private readonly NavMeshAgent _navMeshAgent;
	private readonly Animator _animator;
	private static readonly int isAttacking = Animator.StringToHash("isAttacking");
	private Vector3 _lastPosition = Vector3.zero;
	private float initialSpeed;
	private const float ATTACK_SPEED = 6f;
	private const float ATTACK_DISTANCE = 5f;
	#endregion

	#region Basic Methods
	public AttackState (BasicRobotController basicRobotController, NavMeshAgent navMeshAgent, Animator animator, PlayerDetector playerDetector)
	{
		_basicRobotController = basicRobotController;
		_navMeshAgent = navMeshAgent;
		_animator = animator;
		_playerDetector = playerDetector;
	}
	#endregion

	#region Custom Methods
	public void OnEnter()
	{
		_navMeshAgent.enabled = true;
		initialSpeed = _navMeshAgent.speed;
		_navMeshAgent.speed = ATTACK_SPEED;
		_animator.SetBool("isAttacking", true);
	}

	public void OnExit()
	{
		_navMeshAgent.speed = initialSpeed;
		_navMeshAgent.enabled = false;
		if (_animator != null)
		{
			_animator.SetBool("isAttacking", false);
		}
	}

	public void Tick()
	{
		_navMeshAgent.SetDestination(_playerDetector.detectedPlayer.transform.position);

		if (Vector3.Distance(_navMeshAgent.transform.position, _playerDetector.detectedPlayer.transform.position) < 1)
		{
			_playerDetector.playerHealth.TakeDamage(10f);
		}
	}
	#endregion

}
