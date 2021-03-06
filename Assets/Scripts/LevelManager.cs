using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public int readyDurationMillis = 2000;
	public int endingDurationMillis = 2000;

	public String nextScene = "main";

	public AudioClip winClip;
	public AudioClip loseClip;

	private AudioSource audioSource;

	private Cooldown startingCooldown;
	private Cooldown endingCooldown;

	private bool victory = false;

	[HideInInspector]
	public LevelState levelState;

	[HideInInspector]
	public LevelResult levelResult;

	void Awake() {
		audioSource = GetComponent<AudioSource>();

		startingCooldown = new Cooldown(readyDurationMillis, false);
		endingCooldown = new Cooldown(endingDurationMillis, false);
		levelState = new LevelState();
		levelResult = new LevelResult();

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
				SceneManager.LoadScene(victory ? nextScene : "main");
				return;
		}
	}

	public void EndLevel(bool victory = false) {
		this.victory = victory;
		levelState.SetCurrentState(LevelState.State.Ending);
		levelResult.SetCurrentResult(victory ? LevelResult.Result.Win : LevelResult.Result.Lose);
		audioSource.PlayOneShot(victory?winClip:loseClip);
	}
}