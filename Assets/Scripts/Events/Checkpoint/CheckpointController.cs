using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
	#region Variables
	public int checkpointId;
	#endregion

	#region Basic Methods
	private void Start()
	{
		CheckpointEvent.current.OnCheckpointTriggerEnter += OnCheckpointEnter;
	}

	private void OnDestroy()
	{
		CheckpointEvent.current.OnCheckpointTriggerEnter -= OnCheckpointEnter;
	}
	#endregion

	#region Custom Methods
	private void OnCheckpointEnter(int id)
	{
		if (id == checkpointId)
		{
			ResetCheckpoints();
			PlayerPrefs.SetInt("Checkpoint", checkpointId);
		}
	}

	private void ResetCheckpoints()
	{
		foreach (CheckpointTriggerArea checkpoint in CheckpointEvent.current.checkpoints)
		{
			checkpoint.ResetCheckpoint(true);
		}
	}
	#endregion
}
