using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SlimeController : MonoBehaviour
{
	#region Variables
	private StateMachine stateMachine;

	public CharacterController3D Target { get; set; }
	public bool isDead = false;
	public bool cyUses = false;
	public bool drops = false;
	public GameObject dropObject;
	public AudioClip deathSfx;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		var navMeshAgent = GetComponent<NavMeshAgent>();
		var playerDetector = gameObject.AddComponent<PlayerDetector>();
		stateMachine = new StateMachine();

		var idle = new SlimeIdleState(this, playerDetector);
		var flee = new SlimeFleeState(this, navMeshAgent, playerDetector);

		At(idle, flee, HasTarget());

		stateMachine.AddAnyTransition(flee, () => playerDetector.PlayerInRange);
		At(flee, idle, () => playerDetector.PlayerInRange == false);

		stateMachine.SetState(idle);

		void At(IState to, IState from, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);
		Func<bool> HasTarget() => () => Target != null;
	}

	private void Start()
	{
		if (cyUses)
		{
			if(PlayerPrefs.GetInt(gameObject.name, 0) == 1)
			{
				gameObject.SetActive(false);
			}
		}
	}

	private void Update() => stateMachine.Tick();

	#endregion

	#region Custom Methods
	public void Die()
	{
		if (drops)
		{
			Instantiate(dropObject, transform.position, new Quaternion(0, 0, 0, 0));
			gameObject.transform.position = new Vector3(0, -50, 0);
		}
		
		Destroy(gameObject);
		AudioManager.Instance.PlaySfx(deathSfx);

		if (cyUses)
		{
			PlayerPrefs.SetInt(gameObject.name, 1);
		}
	}
	#endregion
}
