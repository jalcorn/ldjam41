using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public int delay;
	public int repeatRate;
	public Pathing pathing;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", delay, repeatRate);
	}

	public void Spawn() {
		GameObject enemy = Instantiate(enemyPrefab, this.transform);
		enemy.transform.position = this.transform.position;
		EnemyMovement em = enemy.GetComponent<EnemyMovement>();
		em.pathing = pathing;
	}
}
