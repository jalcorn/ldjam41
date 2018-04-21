using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour {
	public Single speed;

    public PlayerAnimator playerAnimator;


	void Update() {
		Vector3 position = this.transform.position;
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			position.y += speed;
            playerAnimator.state = PlayerAnimator.playerMoveState.walkUp;
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			position.y -= speed;
            playerAnimator.state = PlayerAnimator.playerMoveState.walkDown;
        } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			position.x -= speed;
            playerAnimator.state = PlayerAnimator.playerMoveState.walkLeft;
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			position.x += speed;
            playerAnimator.state = PlayerAnimator.playerMoveState.walkRight;
        } else {
            if (playerAnimator.state < PlayerAnimator.playerMoveState.standUp) { //Didn't move but is in walking animation
                playerAnimator.state += 4;//Move to "standing" animation
            }
        }



		this.transform.position = position;
	}
}
