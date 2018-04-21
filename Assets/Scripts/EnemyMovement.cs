using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class EnemyMovement : MonoBehaviour {
	private static float MIN_DISTANCE = 0.1f;

	public float speed;
	public Pathing pathing;

	private Queue<Vector3> positions;

	void Start() {
		positions = pathing.GetPathPositions();
	}

	void Update() {
		if (positions.Count > 0) {
			Vector3 targetPosition = positions.Peek();
			Vector3 position = this.transform.position;

			if (Vector3.Distance(position, targetPosition) < MIN_DISTANCE) {
				positions.Dequeue();
			}

			float step = speed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards(position, targetPosition, step);
		}
	}
}
