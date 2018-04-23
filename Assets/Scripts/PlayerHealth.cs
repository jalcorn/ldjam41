using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public int maxHealth = 3;
	int health = 0;
	public PlayerHealthText text;
	private LevelManager levelManager;

	private AudioSource audioSource;
	private CameraShake shake;
	private SpriteRenderer spriteRenderer;

	void Start() {
		health = maxHealth;
		text = FindObjectOfType<PlayerHealthText>();
		text.SetHealthText(health);

		levelManager = FindObjectOfType<LevelManager>();

		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
		shake = FindObjectOfType<CameraShake>();
	}

	public void AdjustHitpoints(int delta) {
		audioSource.Play();
		shake.Shake();

		health += delta;
		health = Math.Max(0, health);

		float tint = 0.5f + 0.5f * ((float) health / maxHealth);
		spriteRenderer.color = new Color(1f, tint, tint, 1f);
		if (health <= 0 && levelManager.levelState.Cur == LevelState.State.Playing) {
			levelManager.EndLevel(false);
			GetComponent<PlayerInput>().recoilFromHit(4f);
		} else if (delta < 0) {
			GetComponent<PlayerInput>().recoilFromHit(.5f);
		}
		text.SetHealthText(health);
	}

	public int GetHealth() {
		return health;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.enabled && collision.gameObject.tag == "Enemy") {
			collision.collider.enabled = false;
			EnemyHealth eh = collision.gameObject.GetComponent<EnemyHealth>();
			eh.AdjustHitpoints(-9999);
			AdjustHitpoints(-1);
		}
	}
}
