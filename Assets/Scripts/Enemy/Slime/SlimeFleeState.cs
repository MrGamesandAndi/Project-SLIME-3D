using UnityEngine;
using UnityEngine.AI;

public class SlimeFleeState : IState
{
	#region Variables
	private readonly SlimeController _slimeController;
	private readonly PlayerDetector _playerDetector;
	private readonly NavMeshAgent _navMeshAgent;
	private Vector3 _lastPosition = Vector3.zero;
	private float initialSpeed;
	private const float ATTACK_SPEED = 6f;
	private const float ATTACK_DISTANCE = 5f;
	private Transform startTransform;
	#endregion

	#region Basic Methods
	public SlimeFleeState(SlimeController slimeController, NavMeshAgent navMeshAgent, PlayerDetector playerDetector)
	{
		_slimeController = slimeController;
		_navMeshAgent = navMeshAgent;
		_playerDetector = playerDetector;
	}
	#endregion

	#region Custom Methods
	public void OnEnter()
	{
		_navMeshAgent.enabled = true;
		initialSpeed = _navMeshAgent.speed;
		_navMeshAgent.speed = ATTACK_SPEED;
	}

	public void OnExit()
	{
		_navMeshAgent.speed = initialSpeed;
		_navMeshAgent.enabled = false;
	}

	public void Tick()
	{
		startTransform = _slimeController.transform;
		_slimeController.transform.rotation = Quaternion.LookRotation(_slimeController.transform.position - _playerDetector.transform.position);
		Vector3 runTo = _slimeController.transform.position + _slimeController.transform.forward * Random.Range(5, 10);
		NavMeshHit hit;
		NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));
		_slimeController.transform.position = startTransform.position;
		_slimeController.transform.rotation = startTransform.rotation;
		_navMeshAgent.SetDestination(hit.position);
	}
	#endregion
}
