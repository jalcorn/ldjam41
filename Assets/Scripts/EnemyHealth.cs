using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	//Health//

	public float maxHealth = 100;
	private float health = 1;
	private HealthbarFill healthbar;
	private SpriteRenderer renderer;

	public GameObject deathEffect;

	void Start() {
		health = maxHealth;
		healthbar = GetComponentInChildren<HealthbarFill>();
		renderer = GetComponentInChildren<SpriteRenderer>();
	}

	public void AdjustHitpoints(int delta) {
		health += delta;
		healthbar.SetHealth(health, maxHealth);
		float tint = (health / maxHealth) * .5f + .5f;
		renderer.color = new Color(tint, tint, tint, 1f);
		if (health <= 0) {
			Instantiate(deathEffect, this.transform.position, this.transform.rotation);
			Destroy(this.gameObject);
		}
	}
}
