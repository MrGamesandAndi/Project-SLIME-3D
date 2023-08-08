public class IdleState : IState
{
	#region Variables
	private readonly BasicRobotController _basicRobotController;
	private readonly PlayerDetector _playerDetector;
	#endregion

	#region Basic Methods
	public IdleState(BasicRobotController basicRobotController, PlayerDetector playerDetector)
	{
		_basicRobotController = basicRobotController;
		_playerDetector = playerDetector;
	}
	#endregion

	#region Custom Methods
	public void Tick()
	{
		_basicRobotController.Target = _playerDetector.detectedPlayer;
	}

	public void OnEnter()
	{

	}

	public void OnExit()
	{

	}
	#endregion
}
