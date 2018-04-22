using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour {
	public GameObject finalLocation;

	public Queue<Vector2> GetPathPositions() {
		Queue<Vector2> positions = new Queue<Vector2>();

		Transform[] childrenTransforms = GetComponentsInChildren<Transform>();
		foreach (Transform childTransform in childrenTransforms) {
      if ("Path".Equals(childTransform.tag)) {
        positions.Enqueue(childTransform.position);
			}
		}
		positions.Enqueue(finalLocation.transform.position);

		return positions;
	}
}
