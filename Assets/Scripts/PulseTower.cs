using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTower : Tower {

	public TowerPulseAnimator fieldSprite;

	public int damagePerAttack = 1;

	List<EnemyHealth> currentCharacterInRange = new List<EnemyHealth>();

	internal override void PowerOn() {
		base.PowerOn();
		fieldSprite.TurnOn(true);
	}

	internal override void PowerOff() {
		base.PowerOff();
		fieldSprite.TurnOn(false);
	}

	internal override void Attack() {
		// Avoid concurrent modification exception
		currentCharacterInRange.Clear();
		currentCharacterInRange.AddRange(charactersInRange);
		foreach (EnemyHealth character in currentCharacterInRange) {
			character.AdjustHitpoints(-damagePerAttack);
		}
	}
}
