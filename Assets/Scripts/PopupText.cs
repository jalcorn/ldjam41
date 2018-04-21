using UnityEngine;

public class PopupText : MonoBehaviour {
	public LevelState.State state;
	public LevelManager levelManager;

	void Start() {
		levelManager.levelState.OnStateChange += OnLevelStateChange;
	}

	private void OnLevelStateChange(LevelState.State prev, LevelState.State next) {
		if (prev != next) {
			this.gameObject.SetActive(next == state);
		}
	}
}
