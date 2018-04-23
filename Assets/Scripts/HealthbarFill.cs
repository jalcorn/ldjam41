using UnityEngine;

public class HealthbarFill : MonoBehaviour {

    float fillPercent;

	public void SetHealth(float current, float max) {
		float healthPercent = current / max;
		this.gameObject.transform.localScale = new Vector3(healthPercent, 1, 1);
	}
}
