using UnityEngine;

public class TurretTower : Tower {
	public GameObject bulletPrefab;

	internal override void Attack() {
		if (charactersInRange.Count > 0) {
			EnemyHealth target = charactersInRange[0];
			GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
			Projectile projectile = bullet.GetComponent<Projectile>();
			if (target != null) {
				projectile.SetTarget(target.transform);
			}
		}
	}
}
