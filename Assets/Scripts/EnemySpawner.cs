using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject redEnemyPrefab;
	public GameObject blueEnemyPrefab;
	public GameObject yellowEnemyPrefab;
	public List<Wave> waves;

	private LevelManager levelManager;
	private Pathing pathing;
	private Cooldown spawnCooldown;
	private int waveIndex = 0;
	private int spawnIndex = 0;

	void Start() {
		levelManager = FindObjectOfType<LevelManager>();
		pathing = FindObjectOfType<Pathing>();
	}

	public void Spawn(EnemyType type) {
		GameObject enemyPrefab;
		switch (type) {
			case EnemyType.Yellow:
				enemyPrefab = yellowEnemyPrefab;
				break;
			case EnemyType.Blue:
				enemyPrefab = blueEnemyPrefab;
				break;
			case EnemyType.Red:
			default:
				enemyPrefab = redEnemyPrefab;
				break;
		}
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

	public int RemainingWaves() {
		return waves.Count - waveIndex;
	}

	private bool IsEnemyAlive() {
		return GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
	}
}
