using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomFollower : MonoBehaviour
{
	#region Variables
	private Transform target;
	private Vector3 velocity = Vector3.zero;

	public float minModifier = 7;
	public float maxModifier = 11;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	#endregion

	#region Custom Methods
	private void Update()
	{
		transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
	}
	#endregion
}
