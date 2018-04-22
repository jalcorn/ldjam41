using UnityEngine;

public class PopupText : MonoBehaviour {
	public LevelState.State state;
	public LevelResult.Result result;

	private LevelManager levelManager;

	void Start() {
		levelManager = FindObjectOfType<LevelManager>();

		levelManager.levelState.OnStateChange += OnLevelStateChange;
		levelManager.levelResult.OnResultChange += OnResultStateChange;

		UpdateActiveState();
	}

	private void OnLevelStateChange(LevelState.State prev, LevelState.State next) {
		if (prev != next) {
			UpdateActiveState();
		}
	}

	private void OnResultStateChange(LevelResult.Result prev, LevelResult.Result next) {
		if (prev != next) {
			UpdateActiveState();
		}
	}

	private void UpdateActiveState() {
		this.gameObject.SetActive(levelManager.levelResult.Cur == result && levelManager.levelState.Cur == state);
	}
}
