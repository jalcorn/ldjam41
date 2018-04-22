using UnityEngine;

public class LevelResult {
	public enum Result {
		Pending,
		Win,
		Lose,
	}

	public delegate void ResultChange(Result prev, Result next);

	public event ResultChange OnResultChange;

	public Result Cur = Result.Pending;

	public void SetCurrentResult(Result state) {
		Debug.Log("state: " + state);
		var prev = Cur;
		Cur = state;
		if (OnResultChange != null)
			OnResultChange.Invoke(prev, state);
	}
}