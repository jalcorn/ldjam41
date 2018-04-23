using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour {
	//Attacks
	public float delayToStart = 5.0f;
	public float secondsBetweenAttack = 0.1f;
	CircleCollider2D trigger;

	//Powerlevel
	public float powerLevel = 0;
	float maxPowerLevel = 1;
	float minPowerLevel = 0;
	float powerLostPerSec = 1f / 15f;//15 seconds from 100% to 0%
	float powerGainPerSec = 4f / 15f;

	public TowerAnimation towerSprite;
	public BatteryAnimator batterySprite;

	public AudioClip chargingClip;
	public AudioClip fullyChargedClip;

	internal List<EnemyHealth> charactersInRange = new List<EnemyHealth>();

	private bool shouldSoundOnChanged = false;
	private AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource>();
		trigger = gameObject.GetComponent<CircleCollider2D>();
		InvokeRepeating("TryAttack", delayToStart, secondsBetweenAttack);
	}

	private void FixedUpdate() {
		powerLevel -= Time.fixedDeltaTime * powerLostPerSec;

		powerLevel = Mathf.Max(minPowerLevel, powerLevel);
		powerLevel = Mathf.Min(maxPowerLevel, powerLevel);

		batterySprite.powerLevel = powerLevel;
		towerSprite.framerate = Mathf.FloorToInt(powerLevel * 4) + 3;

		if (powerLevel > minPowerLevel) {
			PowerOn();
		} else {
			PowerOff();
		}

		if (powerLevel < 0.9f * maxPowerLevel) {
			shouldSoundOnChanged = true;
		}
	}

	private void TryAttack() {
		if (powerLevel > minPowerLevel) {
            List<EnemyHealth> charactersToDelete = new List<EnemyHealth>();

            foreach (EnemyHealth character in charactersInRange) {
                if( character == null ) {
                    charactersToDelete.Add(character);
                }
            }

            foreach (EnemyHealth character in charactersToDelete) {
                charactersInRange.Remove(character);
            }

            Attack();
		}
	}

	internal virtual void PowerOn() {
		towerSprite.state = TowerAnimation.towerSpriteState.on;
	}

	internal virtual void PowerOff() {
		towerSprite.state = TowerAnimation.towerSpriteState.off;
	}

	internal abstract void Attack();

	public void GetCharged() {
		powerLevel += Time.fixedDeltaTime * powerGainPerSec;
		if (shouldSoundOnChanged && powerLevel >= maxPowerLevel * 0.98f) {
			shouldSoundOnChanged = false;
			audioSource.PlayOneShot(fullyChargedClip, Random.Range(0.8f, 1f));
		} else if (!audioSource.isPlaying) {
			audioSource.PlayOneShot(chargingClip, Random.Range(0.5f, .8f));
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
        EnemyHealth character = collision.gameObject.GetComponent<EnemyHealth>();
		if (character != null) {
			charactersInRange.Add(character);
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
        EnemyHealth character = collision.gameObject.GetComponent<EnemyHealth>();
		if (character != null) {
			charactersInRange.Remove(character);
		}
	}
}
