using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTower : Tower {
	public GameObject bulletPrefab;

	internal override void Attack() {
		if (charactersInRange.Count > 0) {
			EnemyHealth target = charactersInRange[0];
			GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
			Projectile projectile = bullet.GetComponent<Projectile>();
			projectile.SetTarget(target.transform);
		}
	}
}
