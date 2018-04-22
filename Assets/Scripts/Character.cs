using UnityEngine;

public class Character : MonoBehaviour {
	public int hitPoints = 1;

	void Update() {
		if (hitPoints <= 0) {
			if (IsPlayer()) {
				LevelManager levelManager = FindObjectOfType<LevelManager>();
				if (levelManager.levelState.Cur == LevelState.State.Playing) {
					levelManager.EndLevel(false);
				}
			} else {
				Die();
			}
		}
	}

	public bool IsPlayer() {
		return this.gameObject.tag == "Player";
	}

	public void AdjustHitpoints(int damage) {
		hitPoints += damage;
		gameObject.SendMessage("HitpointsChanged", SendMessageOptions.DontRequireReceiver);
	}

	public void Die() {
		Destroy(gameObject);
	}
}
