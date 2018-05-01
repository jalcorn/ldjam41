using UnityEngine;

#if MOBILE_INPUT
using UnityStandardAssets.CrossPlatformInput;
#endif

public class PlayerInput : MonoBehaviour {
	public float speed;
	public PlayerAnimator playerAnimator;

	private PlayerAnimator.PlayerMoveState lastInputState;

	private Tower closestTower;
	private bool canCharge = false;
	private bool isCharging = false;

	private bool isRecoiling = false;

	private void Start() {
	}

	void Update() {

		if (isRecoiling) {
			playerAnimator.state.action = ActionState.death;
		} else if (canCharge && isChargePresssed()) {
			playerAnimator.state.action = ActionState.fix;
			if (closestTower.transform.position.x < this.transform.position.x) {
				playerAnimator.state.direction = DirectionState.left;
			} else {
				playerAnimator.state.direction = DirectionState.right;
			}
			isCharging = true;
		} else {
			if (isCharging) {
				playerAnimator.state.action = ActionState.stand;
				playerAnimator.state.direction = DirectionState.down;
			}

			isCharging = false;
			// Save last pressed key to prioritize the last pressed key
#if MOBILE_INPUT
			float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
			float vertical = CrossPlatformInputManager.GetAxis("Vertical");
			if (Mathf.Abs(horizontal) >= float.Epsilon && Mathf.Abs(horizontal) > Mathf.Abs(vertical)) {
				playerAnimator.state.action = ActionState.walk;
				if (horizontal > float.Epsilon) {
					//right
					playerAnimator.state.direction = DirectionState.right;
				} else if (horizontal <= float.Epsilon) {
					//left
					playerAnimator.state.direction = DirectionState.left;
				}
			} else if (Mathf.Abs(vertical) >= float.Epsilon && Mathf.Abs(vertical) >= Mathf.Abs(horizontal)) {
				playerAnimator.state.action = ActionState.walk;
				if (vertical > float.Epsilon) {
					//up
					playerAnimator.state.direction = DirectionState.up;
				} else if (vertical <= float.Epsilon) {
					//down
					playerAnimator.state.direction = DirectionState.down;
				}
			} else {
				//none
				playerAnimator.state.action = ActionState.stand;
			}
#else
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
				lastInputState = new PlayerAnimator.PlayerMoveState(ActionState.walk, DirectionState.up);
			} else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
				lastInputState = new PlayerAnimator.PlayerMoveState(ActionState.walk, DirectionState.down);
			} else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
				lastInputState = new PlayerAnimator.PlayerMoveState(ActionState.walk, DirectionState.left);
			} else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
				lastInputState = new PlayerAnimator.PlayerMoveState(ActionState.walk, DirectionState.right);
			}

			if (lastInputState == PlayerAnimator.PlayerMoveState.walkUp && isUpPressed()) {
				playerAnimator.state = PlayerAnimator.PlayerMoveState.walkUp;
			} else if (lastInputState == PlayerAnimator.PlayerMoveState.walkDown && isDownPressed()) {
				playerAnimator.state = PlayerAnimator.PlayerMoveState.walkDown;
			} else if (lastInputState == PlayerAnimator.PlayerMoveState.walkLeft && isLeftPressed()) {
				playerAnimator.state = PlayerAnimator.PlayerMoveState.walkLeft;
			} else if (lastInputState == PlayerAnimator.PlayerMoveState.walkRight && isRightPressed()) {
				playerAnimator.state = PlayerAnimator.PlayerMoveState.walkRight;
			} else if (isUpPressed()) {
				playerAnimator.state = PlayerAnimator.PlayerMoveState.walkUp;
			} else if (isDownPressed()) {
				playerAnimator.state = PlayerAnimator.PlayerMoveState.walkDown;
			} else if (isLeftPressed()) {
				playerAnimator.state = PlayerAnimator.PlayerMoveState.walkLeft;
			} else if (isRightPressed()) {
				playerAnimator.state = PlayerAnimator.PlayerMoveState.walkRight;
			} else {
				if (playerAnimator.state.action == ActionState.walk) {
					//Didn't move but is in walking animation
					playerAnimator.state.action = ActionState.stand;
				}
			}
#endif
		}
		lastInputState = playerAnimator.state;
	}

	private void FixedUpdate() {
		if (isCharging && closestTower != null) {
			closestTower.Charge();
		}

		if (playerAnimator.state.action == ActionState.walk) {
			switch (playerAnimator.state.direction) {
				case DirectionState.up:
					moveUp();
					break;
				case DirectionState.down:
					moveDown();
					break;
				case DirectionState.left:
					moveLeft();
					break;
				case DirectionState.right:
					moveRight();
					break;
			}
		}
	}

	private void moveUp() {
		Vector3 position = this.transform.position;
		position.y += speed;
		this.transform.position = position;
	}

	private void moveDown() {
		Vector3 position = this.transform.position;
		position.y -= speed;
		this.transform.position = position;
	}

	private void moveLeft() {
		Vector3 position = this.transform.position;
		position.x -= speed;
		this.transform.position = position;
	}

	private void moveRight() {
		Vector3 position = this.transform.position;
		position.x += speed;
		this.transform.position = position;
	}

	private bool isUpPressed() {
		return Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
	}

	private bool isDownPressed() {
		return Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
	}

	private bool isLeftPressed() {
		return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
	}

	private bool isRightPressed() {
		return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
	}

	private bool isChargePresssed() {
#if MOBILE_INPUT
		return CrossPlatformInputManager.GetButton("Jump");
#else
		return Input.GetButton("Jump");
#endif
	}


	public void setClosestTower(Tower t) {
		closestTower = t;
		canCharge = true;
	}

	public void eraseClosestTower(Tower t) {
		if (closestTower == t) {
			closestTower = null;
		}
		canCharge = false;
	}

	public void recoilFromHit(float time) {
		isRecoiling = true;
		Invoke("StopRecoiling", time);
	}

	private void StopRecoiling() {
		isRecoiling = false;
		playerAnimator.state.action = ActionState.stand;
		playerAnimator.state.direction = DirectionState.down;
		lastInputState = playerAnimator.state;
	}
}
