using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class EnemyBehavior : MonoBehaviour {
	private static float MIN_DISTANCE = 0.1f;

	public float speed;
	public Pathing pathing;

  private Queue<Vector3> positions;

  public float visionDistance = 2f;
  public GameObject visionObject;
  EnemyVision vision;
  Player player = null;

	void Start() {
    positions = pathing.GetPathPositions();
    visionObject.transform.localScale = new Vector3(visionDistance, visionDistance);
    vision = visionObject.GetComponent<EnemyVision>();
    vision.watchForPlayer(playerSighted);
	}

	void Update() {
    if (player != null) {
      MoveToPosition(player.transform.position);
    } else if (positions.Count > 0) {
      MoveToPosition(positions.Peek());
    }
  }

  void MoveToPosition(Vector3 targetPosition) {

    Vector3 position = this.transform.position;

    if (Vector3.Distance(position, targetPosition) < MIN_DISTANCE) {
      positions.Dequeue();
    }

    float step = speed * Time.deltaTime;
    this.transform.position = Vector3.MoveTowards(position, targetPosition, step);
    Vector3 vectorToTarget = targetPosition - position;
    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
    visionObject.transform.rotation = Quaternion.RotateTowards(visionObject.transform.rotation, q, Mathf.PI);
  }

  void playerSighted(Player player) {
    this.player = player;
  }
}
