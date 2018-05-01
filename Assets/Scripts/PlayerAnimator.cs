using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

	public struct PlayerMoveState : IEqualityComparer<PlayerMoveState> {

		public PlayerMoveState(ActionState action, DirectionState direction) {
			this.action = action;
			this.direction = direction;
		}

		public ActionState action;
		public DirectionState direction;

		public bool Equals(PlayerMoveState x, PlayerMoveState y) {
			return x.action == y.action && x.direction == y.direction;
		}

		public int GetHashCode(PlayerMoveState obj) {
			return 10 * (int)obj.action + (int)obj.direction;
		}

		public static bool operator ==(PlayerMoveState lhs, PlayerMoveState rhs) {
			// Check for null on left side.
			if (Object.ReferenceEquals(lhs, null)) {
				if (Object.ReferenceEquals(rhs, null)) {
					// null == null = true.
					return true;
				}

				// Only the left side is null.
				return false;
			}
			// Equals handles case of null on right side.
			return lhs.Equals(rhs);
		}

		public static bool operator !=(PlayerMoveState lhs, PlayerMoveState rhs) {
			return !(lhs == rhs);
		}

		public static PlayerMoveState walkUp = new PlayerMoveState(ActionState.walk, DirectionState.up);
		public static PlayerMoveState walkDown = new PlayerMoveState(ActionState.walk, DirectionState.down);
		public static PlayerMoveState walkLeft = new PlayerMoveState(ActionState.walk, DirectionState.left);
		public static PlayerMoveState walkRight = new PlayerMoveState(ActionState.walk, DirectionState.right);
	}

	public PlayerMoveState state = new PlayerMoveState(ActionState.stand, DirectionState.down);

	private float lastChanged;
	private PlayerMoveState previousState = new PlayerMoveState(ActionState.stand, DirectionState.down);
	private SpriteRenderer spriteComp;

	public Sprite[] movementSprites = new Sprite[12];

	public Sprite[] fixTowerSprites = new Sprite[2];

	public Sprite deathSprite;

	private Sprite[] currentSprites = new Sprite[12];
	private int animationLength = 1;

	private bool flipX = false;

	public float frameRate = 6.0f;

	// Use this for initialization
	void Start() {
		spriteComp = GetComponent<SpriteRenderer>();

		lastChanged = Time.time;
		refreshFrames();

		spriteComp.flipX = flipX;

		previousState = state;
	}

	// Update is called once per frame
	void Update() {
		if (previousState != state) {
			previousState = state;

			lastChanged = Time.time;

			refreshFrames();

			spriteComp.flipX = flipX;
		}

		spriteComp.sprite = currentSprites[getCurrentFrame()];
	}

	private void refreshFrames() {
		int directionIndex = state.direction.AnimationIndex() * 4;
		flipX = state.direction == DirectionState.right;
		switch ((state.action)) {
			case ActionState.walk:
				currentSprites[3] = movementSprites[directionIndex];
				currentSprites[0] = movementSprites[directionIndex + 1];
				currentSprites[1] = movementSprites[directionIndex + 2];
				currentSprites[2] = movementSprites[directionIndex + 3];
				animationLength = 4;
				break;
			case ActionState.stand:
				currentSprites[0] = movementSprites[directionIndex];
				animationLength = 1;
				break;
			case ActionState.fix:
				currentSprites[0] = fixTowerSprites[0];
				currentSprites[1] = fixTowerSprites[1];
				animationLength = 2;
				break;
			case ActionState.death:
				currentSprites[0] = deathSprite;
				animationLength = 1;
				break;
		}
	}

	private int getCurrentFrame() {
		if (animationLength <= 1) {
			return 0;
		}

		var secondsSinceChanged = Time.time - lastChanged;
		return Mathf.FloorToInt(secondsSinceChanged * frameRate) % animationLength;
	}
}

public enum ActionState {
	walk = 0,
	stand,
	fix,
	death
}

public enum DirectionState {
	left = 0,
	right,
	up,
	down
}
