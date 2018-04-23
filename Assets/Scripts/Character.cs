using UnityEngine;

public class Character : MonoBehaviour {
	public int maxHealth = 1;

	private int hitPoints = 1;

	public delegate void HealthChange(int prev, int next);

	public event HealthChange OnHealthChange;

	void Start() {
		hitPoints = maxHealth;
	}

	public void AdjustHitpoints(int delta) {
		if (hitPoints <= 0) {
			return;
		}

		int prevHealth = hitPoints;
		hitPoints += delta;
		OnHealthChange.Invoke(prevHealth, hitPoints);
	}
}
