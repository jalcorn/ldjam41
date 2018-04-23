using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {


    //Health//

    public float maxHealth = 100;
    private float health = 1;
    private HealthbarFill healthbar;

    void Start() {
        health = maxHealth;
        healthbar = GetComponentInChildren<HealthbarFill>();
    }

    public void AdjustHitpoints(int delta) {
        health += delta;
        healthbar.SetHealth(health, maxHealth);
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
