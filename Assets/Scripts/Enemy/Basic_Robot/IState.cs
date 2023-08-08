public interface IState
{
	#region Custom Methods
	void Tick();

	void OnEnter();

	void OnExit();
	#endregion
}
