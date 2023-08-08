using System;
using System.Collections.Generic;
using UnityEngine;

public class ComputerEvent : MonoBehaviour
{
	#region Variables
	public static ComputerEvent current;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		current = this;
	}
	#endregion

	#region Custom Methods
	public event Action<int> OnComputerTriggerEnter;

	public void ComputerTriggerEnter(int id)
	{
		OnComputerTriggerEnter?.Invoke(id);
	}
	#endregion
}
