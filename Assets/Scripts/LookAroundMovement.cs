using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundMovement : FollowPathMovement {

	private const float MAX_TURN_DEGREES = 50f;

	//degrees per second
	private const float SPEED = 50f;
  
  public LookAroundMovement(GameObject gameObject, float speed, Queue<Vector2> path) : base(gameObject, speed, path) {}

  private float currentAngle;
	private TurningDirection direction = TurningDirection.left;

  public override void Move() {
    base.Move();
		if (currentAngle >= MAX_TURN_DEGREES) {
			direction = TurningDirection.right;
		} else if (currentAngle <= -MAX_TURN_DEGREES) {
			direction = TurningDirection.left;
		}
		currentAngle += Time.deltaTime * SPEED * (float) direction;
  }

  public override Vector3 GetForwardVector() {
		return Quaternion.Euler(0, 0, currentAngle) * base.GetForwardVector();
	}

	public override void SetHighPriorityTarget(Transform target) {
		base.SetHighPriorityTarget(target);
		direction = TurningDirection.pause;
		currentAngle = 0;
	}

  private enum TurningDirection {
    left = 1,
		pause = 0,
    right = -1
  }
}
