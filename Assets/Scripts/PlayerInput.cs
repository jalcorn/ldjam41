using UnityEngine;

#if MOBILE_INPUT
using UnityStandardAssets.CrossPlatformInput;
#endif

public class PlayerInput : MonoBehaviour {
	public float speed;
	public PlayerAnimator playerAnimator;

	private PlayerAnimator.playerMoveState lastInputState = PlayerAnimator.playerMoveState.standDown;

	private Tower closestTower;
	private bool canCharge = false;
	private bool isCharging = false;

	private bool isRecoiling = false;

	private void Start() {
	}

	void Update() {

		if (isRecoiling) {
			lastInputState = PlayerAnimator.playerMoveState.death;
			playerAnimator.state = PlayerAnimator.playerMoveState.death;
		} else if (canCharge && isChargePresssed()) {
			if (closestTower.transform.position.x < this.transform.position.x) {
				lastInputState = PlayerAnimator.playerMoveState.fixLeft;
				playerAnimator.state = PlayerAnimator.playerMoveState.fixLeft;
			} else {
				lastInputState = PlayerAnimator.playerMoveState.fixRight;
				playerAnimator.state = PlayerAnimator.playerMoveState.fixRight;
			}
			isCharging = true;
		} else {
			if (isCharging) {
				lastInputState = PlayerAnimator.playerMoveState.standDown;
				playerAnimator.state = PlayerAnimator.playerMoveState.standDown;
			}

			isCharging = false;
			// Save last pressed key to prioritize the last pressed key
#if MOBILE_INPUT
			lastInputState = playerAnimator.state;
			float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
			float vertical = CrossPlatformInputManager.GetAxis("Vertical");
			if (Mathf.Abs(horizontal) >= float.Epsilon && Mathf.Abs(horizontal) > Mathf.Abs(vertical)) {
				if(horizontal > float.Epsilon) {
					//right
					playerAnimator.state = PlayerAnimator.playerMoveState.walkRight;
				} else if (horizontal <= float.Epsilon) {
					//left
					playerAnimator.state = PlayerAnimator.playerMoveState.walkLeft;
				}
			} else if (Mathf.Abs(vertical) >= float.Epsilon && Mathf.Abs(vertical) >= Mathf.Abs(horizontal)) {
				if (vertical > float.Epsilon) {
					//up
					playerAnimator.state = PlayerAnimator.playerMoveState.walkUp;
				} else if (vertical <= float.Epsilon) {
					//down
					playerAnimator.state = PlayerAnimator.playerMoveState.walkDown;
				}
			} else {
				//none
				playerAnimator.state = lastInputState.ToStanding();
			}
#else
			if (Input.GetButtonDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
				lastInputState = PlayerAnimator.playerMoveState.walkUp;
			} else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
				lastInputState = PlayerAnimator.playerMoveState.walkDown;
			} else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
				lastInputState = PlayerAnimator.playerMoveState.walkLeft;
			} else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
				lastInputState = PlayerAnimator.playerMoveState.walkRight;
			}

			if (lastInputState == PlayerAnimator.playerMoveState.walkUp && isUpPressed()) {
				playerAnimator.state = PlayerAnimator.playerMoveState.walkUp;
			} else if (lastInputState == PlayerAnimator.playerMoveState.walkDown && isDownPressed()) {
				playerAnimator.state = PlayerAnimator.playerMoveState.walkDown;
			} else if (lastInputState == PlayerAnimator.playerMoveState.walkLeft && isLeftPressed()) {
				playerAnimator.state = PlayerAnimator.playerMoveState.walkLeft;
			} else if (lastInputState == PlayerAnimator.playerMoveState.walkRight && isRightPressed()) {
				playerAnimator.state = PlayerAnimator.playerMoveState.walkRight;
			} else if (isUpPressed()) {
				playerAnimator.state = PlayerAnimator.playerMoveState.walkUp;
			} else if (isDownPressed()) {
				playerAnimator.state = PlayerAnimator.playerMoveState.walkDown;
			} else if (isLeftPressed()) {
				playerAnimator.state = PlayerAnimator.playerMoveState.walkLeft;
			} else if (isRightPressed()) {
				playerAnimator.state = PlayerAnimator.playerMoveState.walkRight;
			} else {
				if (playerAnimator.state < PlayerAnimator.playerMoveState.standUp) {
					//Didn't move but is in walking animation
					playerAnimator.state += 4;//Move to "standing" animation
				}
			}
#endif
		}
	}

	private void FixedUpdate() {
		if (isCharging && closestTower != null) {
			closestTower.GetCharged();
		}

		switch (playerAnimator.state) {
			case PlayerAnimator.playerMoveState.walkUp:
				moveUp();
				break;
			case PlayerAnimator.playerMoveState.walkDown:
				moveDown();
				break;
			case PlayerAnimator.playerMoveState.walkLeft:
				moveLeft();
				break;
			case PlayerAnimator.playerMoveState.walkRight:
				moveRight();
				break;
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
		bool isPressed = CrossPlatformInputManager.GetButton("Jump");
		Debug.Log("Charging Pressed - " + isPressed);
		return isPressed;
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

    public void recoilFromHit( float time ) {
        isRecoiling = true;
        Invoke("StopRecoiling", time);
    }

    private void StopRecoiling() {
        isRecoiling = false;
        lastInputState = PlayerAnimator.playerMoveState.standDown;
        playerAnimator.state = PlayerAnimator.playerMoveState.standDown;
    }
}
