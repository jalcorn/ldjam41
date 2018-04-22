using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathMovement {

  private static float MIN_DISTANCE = 0.1f;

  GameObject gameObject;
  float speed;
  Queue<Vector2> path;
  Transform overriddenTarget;
  Vector2 currentTargetPosition;
  Vector2 forwardVector;

  // Use this for initialization
  public FollowPathMovement(GameObject gameObject, float speed, Queue<Vector2> path) {
    this.gameObject = gameObject;
    this.speed = speed;
    this.path = path;
    currentTargetPosition = path.Dequeue();
  }

  //returns vector for gameObject's forward direction
  public virtual void Move() {

    Vector2 position = gameObject.transform.position;

    if (Vector2.Distance(position, currentTargetPosition) < MIN_DISTANCE) {
      currentTargetPosition = path.Dequeue();
    }
    Vector2 targetPosition = overriddenTarget != null ? (Vector2)overriddenTarget.transform.position : currentTargetPosition;

    float step = speed * Time.deltaTime;
    gameObject.transform.position = Vector2.MoveTowards(position, targetPosition, step);
    forwardVector = targetPosition - position;
  }

  public virtual Vector3 GetForwardVector() {
    return forwardVector;
  }

  public virtual void SetHighPriorityTarget(Transform target) {
    overriddenTarget = target;
  }
}
