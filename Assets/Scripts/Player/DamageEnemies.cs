using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemies : MonoBehaviour
{
	#region Basic Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "EnemyRobotTrigger")
		{
			other.GetComponentInParent<BasicRobotController>().Die();
		}

		if (other.tag == "EnemySlimeTrigger")
		{
			other.GetComponentInParent<SlimeController>().Die();
		}
	}
	#endregion
}
