using System;
using UnityEngine;

public class CheckpointEvent : MonoBehaviour
{
	#region Variables
	public int currentCheckpoint = 0;
	public static CheckpointEvent current;

	public CheckpointTriggerArea[] checkpoints;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		current = this;
	}
	#endregion

	#region Custom Methods
	public event Action<int> OnCheckpointTriggerEnter;

	public void CheckpointTriggerEnter(int id)
	{
		OnCheckpointTriggerEnter?.Invoke(id);
	}
	#endregion
}
