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

	internal List<Character> charactersInRange = new List<Character>();

	// Use this for initialization
	void Start() {
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
	}

	private void TryAttack() {
		if (powerLevel > minPowerLevel) {
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
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Character character = collision.gameObject.GetComponent<Character>();
		if (character != null && collision.gameObject.tag != "Player") {
			charactersInRange.Add(character);
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		Character character = collision.gameObject.GetComponent<Character>();
		if (character != null&& collision.gameObject.tag != "Player") {
			charactersInRange.Remove(character);
		}
	}
}
