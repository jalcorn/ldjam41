using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	private Cooldown startingCooldown;
	private Cooldown endingCooldown;

	[HideInInspector]
	public LevelState levelState;

	void Awake() {
		startingCooldown = new Cooldown(2000, false);
		endingCooldown = new Cooldown(2000, false);
		levelState = new LevelState();

		levelState.OnStateChange += OnLevelStateChange;
	}

	private void OnLevelStateChange(LevelState.State prev, LevelState.State next) {
		if (prev != next) {
			switch (next) {
				case LevelState.State.Starting:
					startingCooldown.Trigger();
					break;
				case LevelState.State.Ending:
					endingCooldown.Trigger();
					break;
			}
		}
	}

	void Update() {
		switch (levelState.Cur) {
			case LevelState.State.Created:
				levelState.SetCurrentState(LevelState.State.Starting);
				break;
			case LevelState.State.Starting:
				if (startingCooldown.IsReady()) levelState.SetCurrentState(LevelState.State.Playing);
				break;
			case LevelState.State.Ending:
				if (endingCooldown.IsReady()) levelState.SetCurrentState(LevelState.State.Ended);
				break;
			case LevelState.State.Ended:
				SceneManager.LoadScene("main");
				return;
		}
	}
}