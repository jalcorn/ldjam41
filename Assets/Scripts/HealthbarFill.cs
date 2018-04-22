using UnityEngine;

public class HealthbarFill : MonoBehaviour {
	private Character character;

	void Start() {
		character = FindObjectOfType<Character>();

		character.OnHealthChange += OnHealthChange;
	}

	private void OnHealthChange(int prev, int next) {
		float healthPercent = (float) next / character.maxHealth;
		this.gameObject.transform.localScale = new Vector3(healthPercent, 1, 1);
	}
}
