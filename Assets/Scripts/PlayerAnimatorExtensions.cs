using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimatorExtensions {

	public static PlayerAnimator.playerMoveState ToStanding(this PlayerAnimator.playerMoveState currentState) {
		switch (currentState) {
			case PlayerAnimator.playerMoveState.walkUp:
				return PlayerAnimator.playerMoveState.standUp;
			case PlayerAnimator.playerMoveState.walkDown:
				return PlayerAnimator.playerMoveState.standDown;
			case PlayerAnimator.playerMoveState.walkLeft:
			case PlayerAnimator.playerMoveState.fixLeft:
				return PlayerAnimator.playerMoveState.standLeft;
			case PlayerAnimator.playerMoveState.walkRight:
			case PlayerAnimator.playerMoveState.fixRight:
				return PlayerAnimator.playerMoveState.standRight;
			default:
				return currentState;
		}
	}
}
