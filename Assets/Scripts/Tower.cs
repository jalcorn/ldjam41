using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
	//Attacks
	public float delayToStart = 5.0f;
	public float secondsBetweenAttack = 0.1f;
	public int damagePerAttack = -1;
	CircleCollider2D trigger;

	//Powerlevel
	public float powerLevel = 0;
	float maxPowerLevel = 1;
	float minPowerLevel = 0;
	float powerLostPerSec = 1f / 15f;//15 seconds from 100% to 0%
	float powerGainPerSec = 4f / 15f;

	public TowerAnimation towerSprite;
	public TowerPulseAnimator fieldSprite;
	public BatteryAnimator batterySprite;

	List<Character> charactersInRange = new List<Character>();
	List<Character> currentCharacterInRange = new List<Character>();

	// Use this for initialization
	void Start() {
		trigger = gameObject.GetComponent<CircleCollider2D>();
		InvokeRepeating("Attack", delayToStart, secondsBetweenAttack);
	}

	private void FixedUpdate() {
		powerLevel -= Time.fixedDeltaTime * powerLostPerSec;

		powerLevel = Mathf.Max(minPowerLevel, powerLevel);
		powerLevel = Mathf.Min(maxPowerLevel, powerLevel);

		batterySprite.powerLevel = powerLevel;
		towerSprite.framerate = Mathf.FloorToInt(powerLevel * 4) + 3;

		if (powerLevel > minPowerLevel) {
			towerSprite.state = TowerAnimation.towerSpriteState.on;
			fieldSprite.TurnOn(true);
		} else {
			towerSprite.state = TowerAnimation.towerSpriteState.off;
			fieldSprite.TurnOn(false);
		}
	}

	void Attack() {
		if (powerLevel > minPowerLevel) {
			// Avoid concurrent modification exception
			currentCharacterInRange.Clear();
			currentCharacterInRange.AddRange(charactersInRange);
			foreach (Character character in currentCharacterInRange) {
				character.AdjustHitpoints(damagePerAttack);
			}
		}
	}

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
