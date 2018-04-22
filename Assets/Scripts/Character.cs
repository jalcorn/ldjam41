using UnityEngine;

public class Character : MonoBehaviour {
	public int hitPoints = 1;

	public delegate void HealthChange(int prev, int next);

	public event HealthChange OnHealthChange;

	public void AdjustHitpoints(int damage) {
		int prevHealth = hitPoints;
		hitPoints += damage;
		OnHealthChange.Invoke(prevHealth, hitPoints);
	}
}
