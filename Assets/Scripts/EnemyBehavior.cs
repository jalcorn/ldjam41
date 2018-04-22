using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class EnemyBehavior : MonoBehaviour {

  public float speed;
  public Pathing pathing;
  public MovementType movementType;

  public float visionDistance = 2f;
  public GameObject visionObject;
  EnemyVision vision;
  public Player sightedPlayer = null;
  FollowPathMovement movementLogic;

  void Start() {
    visionObject.transform.localScale = new Vector3(visionDistance, visionDistance);
    vision = visionObject.GetComponent<EnemyVision>();
    vision.watchForPlayer(playerSighted);
    switch (movementType) {
      case MovementType.LookAroundPath:
        movementLogic = new LookAroundMovement(gameObject, speed, pathing.GetPathPositions());
        break;
      case MovementType.SimplePath:
        movementLogic = new FollowPathMovement(gameObject, speed, pathing.GetPathPositions());
        break;
    }
  }

  void Update() {
    movementLogic.Move();
    Vector3 forwardVector = movementLogic.GetForwardVector();
    float angle = Mathf.Atan2(forwardVector.y, forwardVector.x) * Mathf.Rad2Deg;
    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
    visionObject.transform.rotation = Quaternion.RotateTowards(visionObject.transform.rotation, q, Mathf.PI);
  }

  void playerSighted(Player player) {
    if (sightedPlayer == null) {
      movementLogic.SetHighPriorityTarget(player.transform);
      EnemyBehavior[] list = FindObjectsOfType<EnemyBehavior>();
      foreach (EnemyBehavior enemy in list) {
        enemy.sightedPlayer = player;
      }
    }
  }

  public enum MovementType {
    SimplePath,
    LookAroundPath
  }
}
