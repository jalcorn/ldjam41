using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public Vector3 velocity;

	void Update() {
		Vector3 position = this.transform.position;
		position += velocity;
		this.transform.position = position;
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Base") {
			// TODO: lose life or lose the game
			Destroy(this.gameObject);
		}
	}
}
