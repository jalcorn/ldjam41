using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	//Health//

	public float maxHealth = 100;
	private float health = 1;
	private HealthbarFill healthbar;

	public GameObject deathEffect;

	void Start() {
		health = maxHealth;
		healthbar = GetComponentInChildren<HealthbarFill>();
	}

	public void AdjustHitpoints(int delta) {
		health += delta;
		healthbar.SetHealth(health, maxHealth);
		if (health <= 0) {
			Instantiate(deathEffect, this.transform.position, this.transform.rotation);
			Destroy(this.gameObject);
		}
	}
}
