using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {
	public Single speed;

	void Update() {
		Vector3 position = this.transform.position;
		if (Input.GetKey(KeyCode.W)) {
			position.y += speed;
		} else if (Input.GetKey(KeyCode.S)) {
			position.y -= speed;
		}
		if (Input.GetKey(KeyCode.A)) {
			position.x -= speed;
		} else if (Input.GetKey(KeyCode.D)) {
			position.x += speed;
		}
		this.transform.position = position;
	}
}
