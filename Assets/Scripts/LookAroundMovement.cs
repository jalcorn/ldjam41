using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundMovement : FollowPathMovement {
  
  // Use this for initialization
  public LookAroundMovement(GameObject gameObject, float speed, Queue<Vector2> path) : base(gameObject, speed, path) {}

  public override void Move() {
    base.Move();
  }
}
