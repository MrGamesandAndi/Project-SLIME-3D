using System.Collections.Generic;
using UnityEngine;

public enum PathType
{
	LINEAR,
	LOOP
}


public class MovementPath : MonoBehaviour
{
	#region Variables
	public PathType pathType;
	public int movementDirection = 1;
	public int movingTo = 0;
	public Transform[] pathSequence;
	#endregion

	#region Basic Methods
	

	private void OnDrawGizmos()
	{
		if (pathSequence == null || pathSequence.Length < 2)
		{
			return;
		}

		for (int i = 1; i < pathSequence.Length; i++)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(pathSequence[i - 1].position, pathSequence[i].position);
		}

		if (pathType == PathType.LOOP)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(pathSequence[0].position, pathSequence[pathSequence.Length - 1].position);
		}
	}
	#endregion

	#region Custom Methods
	public IEnumerator<Transform> GetNextPathPoint()
	{
		if (pathSequence == null || pathSequence.Length < 1)
		{
			yield break;
		}

		while (true)
		{
			yield return pathSequence[movingTo];

			if (pathSequence.Length == 1)
			{
				continue;
			}

			if (pathType == PathType.LINEAR)
			{
				if (movingTo <= 0)
				{
					movementDirection = 1;
				}
				else if (movementDirection >= pathSequence.Length - 1)
				{
					movementDirection = -1;
				}
			}

			movingTo = movingTo + movementDirection;

			if (pathType == PathType.LOOP)
			{
				if (movingTo >= pathSequence.Length)
				{
					movingTo = 0;
				}

				if (movingTo < 0)
				{
					movingTo = pathSequence.Length - 1;
				}
			}
		}
	}
	#endregion
}
