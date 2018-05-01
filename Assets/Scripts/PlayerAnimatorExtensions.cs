using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimatorExtensions {

	public static int AnimationIndex(this DirectionState currentState) {
		switch(currentState) {				
			case DirectionState.left:
			case DirectionState.right:
				return 1;
			case DirectionState.up:
				return 2;
			case DirectionState.down:
			default:
				return 0;
		}
	}
}
