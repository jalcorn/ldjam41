using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public int delay;
	public int repeatRate;
	public Pathing pathing;
	public LevelManager levelManager;

	void Start() {
		levelManager.levelState.OnStateChange += OnLevelStateChange;
	}

	private void OnLevelStateChange(LevelState.State prev, LevelState.State next) {
		if (prev != next) {
			switch(next) {
				case LevelState.State.Playing:
					InvokeRepeating("Spawn", delay, repeatRate);
					break;
				default:
					CancelInvoke("Spawn");
					break;
			}
		}
	}

	public void Spawn() {
		GameObject enemy = Instantiate(enemyPrefab, this.transform);
		enemy.transform.position = this.transform.position;
		EnemyBehavior em = enemy.GetComponent<EnemyBehavior>();
		em.pathing = pathing;
	}
}
