using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundMovement : FollowPathMovement {

  private const float MAX_TURN_DEGREE = 45f;
	private const float SPEED = 5f;
  
  public LookAroundMovement(GameObject gameObject, float speed, Queue<Vector2> path) : base(gameObject, speed, path) {}

  private float currentAngle;
	private TurningDirection direction = TurningDirection.left;

  public override void Move() {
    base.Move();
		if (Mathf.Abs(currentAngle) >= MAX_TURN_DEGREE) {
			direction = (TurningDirection)((int)direction * -1);
      Debug.LogWarning("Turning " + direction);
		}
		currentAngle += Time.deltaTime * SPEED * (float) direction;
    Debug.LogWarning("Angle " + currentAngle + " - deltaTime " + Time.deltaTime + " - step " + Time.deltaTime * SPEED + " - direction " + direction);
  }

  public override Vector3 GetForwardVector() {
		return base.GetForwardVector() + new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle));
  }

  private enum TurningDirection {
    left = 1,
		pause = 0,
    right = -1
  }
}
