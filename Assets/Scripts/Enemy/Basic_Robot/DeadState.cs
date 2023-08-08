public class DeadState : IState
{
	#region Variables
	private readonly BasicRobotController _basicRobotController;
	#endregion

	#region Basic Methods
	public DeadState(BasicRobotController basicRobotController)
	{
		_basicRobotController = basicRobotController;
	}
	#endregion

	#region Custom Methods
	public void OnEnter()
	{
		
	}

	public void OnExit()
	{
	}

	public void Tick()
	{
			_basicRobotController.Die();
	}
	#endregion
}
