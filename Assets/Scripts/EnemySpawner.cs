using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public Pathing pathing;
	public LevelManager levelManager;
	public List<Wave> waves;

	private Cooldown spawnCooldown;
	private int waveIndex = 0;
	private int spawnIndex = 0;

	public void Spawn(EnemyType type) {
		// TODO: spawn enemy based on type
		Debug.Log("Spawn");
		GameObject enemy = Instantiate(enemyPrefab, this.transform);
		enemy.transform.position = this.transform.position;
		EnemyBehavior em = enemy.GetComponent<EnemyBehavior>();
		em.pathing = pathing;
	}

	void Update() {
		if (levelManager.levelState.Cur == LevelState.State.Playing) {
			if (waveIndex < waves.Count) {
				Wave wave = waves[waveIndex];

				// Wave completed
				if (spawnIndex < wave.spawns.Length) {
					Spawn spawn = wave.spawns[spawnIndex];
					if (spawnCooldown == null) {
						spawnCooldown = new Cooldown(spawn.delay, false);
						spawnCooldown.Trigger();
					} else if (spawnCooldown.IsReady()) {
						Spawn(spawn.type);
						spawnCooldown = null;
						spawnIndex++;
					}
				} else if (!IsEnemyAlive()) {
					Debug.Log("Wave Complete");
					spawnIndex = 0;
					waveIndex++;
				}
			} else if (!IsEnemyAlive()) {
				Debug.Log("Level Complete");
				levelManager.EndLevel(true);
			}
		}
	}

	private bool IsEnemyAlive() {
		return GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
	}
}
