using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
	MOVE_TOWARDS,
	LERP_TOWARDS
}

public class FollowPath : MonoBehaviour
{
	#region Variables
	public MovementType movementType = MovementType.MOVE_TOWARDS;
	public MovementPath movementPath;
	public float speed = 1f;
	public float maxDistanceToGoal = 0.1f;
	public float distance = 100f;
	public bool looksAtTarget = true;

	private IEnumerator<Transform> pointInPath;
	#endregion

	#region Basic Methods
	private void Start()
	{
		if (movementPath == null)
		{
			return;
		}

		pointInPath = movementPath.GetNextPathPoint();
		pointInPath.MoveNext();

		if (pointInPath.Current == null)
		{
			return;
		}

		transform.position = new Vector3(pointInPath.Current.position.x, transform.position.y, pointInPath.Current.position.z);
	}

	private void Update()
	{
		if(pointInPath==null || pointInPath.Current == null)
		{
			return;
		}

		if (movementType == MovementType.MOVE_TOWARDS)
		{
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(pointInPath.Current.position.x, pointInPath.Current.position.y, pointInPath.Current.position.z), Time.deltaTime * speed);
		}
		else if (movementType == MovementType.LERP_TOWARDS)
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(pointInPath.Current.position.x, pointInPath.Current.position.y, pointInPath.Current.position.z), Time.deltaTime * speed);
		}

		var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;

		if (distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
		{
			pointInPath.MoveNext();

			if (looksAtTarget)
			{
				transform.LookAt(pointInPath.Current);
			}
		}
	}
	#endregion

	public Vector3 ReturnGround()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, Vector3.down, out hit, distance))
		{
			Vector3 targetLocation = hit.point;
			targetLocation += new Vector3(0, transform.localScale.y / 2, 0);
			transform.position = targetLocation;
			return targetLocation;
		}

		return Vector3.zero;
	}
}
