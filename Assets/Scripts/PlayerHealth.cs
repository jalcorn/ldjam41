using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth = 3;
    int health = 0;
    public PlayerHealthText text;
    private LevelManager levelManager;

	private Cooldown invincibilityCooldown;

    void Start() {
        health = maxHealth;
        text = FindObjectOfType<PlayerHealthText>();
        levelManager = FindObjectOfType<LevelManager>();

		invincibilityCooldown = new Cooldown(200, true);
    }

    public void AdjustHitpoints(int delta) {
        health += delta;
        if (health <= 0 && levelManager.levelState.Cur == LevelState.State.Playing) {
            levelManager.EndLevel(false);
        }
        text.SetHealthText(health);
    }

    public int GetHealth() {
        return health;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy" && invincibilityCooldown.IsReady()) {
			invincibilityCooldown.Trigger();
            EnemyHealth eh = collision.gameObject.GetComponent<EnemyHealth>();
            eh.AdjustHitpoints(-9999);
            AdjustHitpoints(-1);
        }
    }
}
