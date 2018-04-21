public class LevelState {
	public enum State {
		Created,
		Starting,
		Playing,
		Ending,
		Ended,
	}

	public delegate void StateChange(State prev, State next);

	public event StateChange OnStateChange;

	public State Cur = State.Created;

	public void SetCurrentState(State state) {
		var prev = Cur;
		Cur = state;
		if (OnStateChange != null)
			OnStateChange.Invoke(prev, state);
	}
}