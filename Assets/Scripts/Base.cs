using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Enemy") {
			// TODO: lose life or lose the game
			Destroy(col.gameObject);
		}
	}
}
