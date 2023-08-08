public class SlimeIdleState : IState
{
	#region Variables
	private readonly SlimeController _slimeController;
	private readonly PlayerDetector _playerDetector;
	#endregion

	#region Basic Methods
	public SlimeIdleState(SlimeController slimeController, PlayerDetector playerDetector)
	{
		_slimeController = slimeController;
		_playerDetector = playerDetector;
	}
	#endregion

	#region Custom Methods
	public void Tick()
	{
		_slimeController.Target = _playerDetector.detectedPlayer;
	}

	public void OnEnter()
	{

	}

	public void OnExit()
	{

	}
	#endregion
}
