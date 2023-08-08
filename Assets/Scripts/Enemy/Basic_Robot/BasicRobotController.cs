using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BasicRobotController : MonoBehaviour
{
	#region Variables
	private StateMachine stateMachine;

	public CharacterController3D Target { get; set; }
	public bool isDead = false;
	public GameObject deadParticles;
	public List<GameObject> atom;
	public AudioClip deathSfx;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		var navMeshAgent = GetComponent<NavMeshAgent>();
		var animator = GetComponent<Animator>();
		var playerDetector = gameObject.AddComponent<PlayerDetector>();
		stateMachine = new StateMachine();

		var idle = new IdleState(this, playerDetector);
		var attack = new AttackState(this, navMeshAgent, animator, playerDetector);

		At(idle, attack, HasTarget());

		stateMachine.AddAnyTransition(attack, () => playerDetector.PlayerInRange);
		At(attack, idle, () => playerDetector.PlayerInRange == false);

		stateMachine.SetState(idle);

		void At(IState to, IState from, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);
		Func<bool> HasTarget() => () => Target != null;
	}

	private void Update() => stateMachine.Tick();

	#endregion

	#region Custom Methods
	public void Die()
	{
		if (deadParticles != null)
		{
			Instantiate(deadParticles, transform.position, new Quaternion(0, 0, 0, 0));
		}

		foreach (GameObject atom in atom)
		{
			Instantiate(atom, transform.position, new Quaternion(0, 0, 0, 0));
		}

		gameObject.transform.position = new Vector3(0, -50, 0);
		Destroy(gameObject);
		AudioManager.Instance.PlaySfx(deathSfx);
	}
	#endregion
}
