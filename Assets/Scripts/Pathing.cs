using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour {
	public GameObject finalLocation;

	public Queue<Vector3> GetPathPositions() {
		Queue<Vector3> positions = new Queue<Vector3>();

		Transform[] childrenTransforms = GetComponentsInChildren<Transform>();
		foreach (Transform transform in childrenTransforms) {
			positions.Enqueue(transform.position);
		}
		positions.Enqueue(finalLocation.transform.position);

		// First component is the parent position so remove it
		positions.Dequeue();

		return positions;
	}
}
