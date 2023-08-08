using System;
using UnityEngine;

public class TimerEvent : MonoBehaviour
{
	#region Variables
	public static TimerEvent current;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		current = this;
	}
	#endregion

	#region Custom Methods
	public event Action OnTimerTriggerEnter;
	
	public void TimerTriggerEnter()
	{
		OnTimerTriggerEnter?.Invoke();
	}
	#endregion
}
