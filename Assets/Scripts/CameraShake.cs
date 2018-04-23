using UnityEngine;

public class CameraShake : MonoBehaviour {
	private bool isShaking = false;
	private int shakeCount;
	private float shakeIntensity;
	private float shakeSpeed;
	private Vector3 nextShakePosition;
	private Vector3 originalPosition;

	void Awake() {
		originalPosition = this.transform.position;
	}

	void Update() {
		if (isShaking) {
			this.transform.position = Vector3.MoveTowards(this.transform.position, nextShakePosition, Time.deltaTime * shakeSpeed);

			if (Vector2.Distance(this.transform.position, nextShakePosition) < shakeIntensity / 5f) {
				shakeCount--;

				if (shakeCount <= 0) {
					isShaking = false;
					this.transform.position = originalPosition;
				} else if (shakeCount <= 1) {
					nextShakePosition = originalPosition;
				} else {
					DetermineNextShakePosition();
				}
			}
		}
	}

	public void Shake(float intensity = 0.3f, int shakes = 5, float speed = 8) {
		isShaking = true;
		shakeCount = shakes;
		shakeIntensity = intensity;
		shakeSpeed = speed;

		DetermineNextShakePosition();
	}

	private void DetermineNextShakePosition() {
		nextShakePosition = new Vector3(Random.Range(-shakeIntensity, shakeIntensity), Random.Range(-shakeIntensity, shakeIntensity), this.transform.localPosition.z);
	}
}