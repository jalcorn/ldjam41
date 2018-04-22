using UnityEngine;

public class Player : MonoBehaviour {
	public float speed;
	public PlayerAnimator playerAnimator;

	private LevelManager levelManager;
	private Character character;

	private PlayerAnimator.playerMoveState lastInputState = PlayerAnimator.playerMoveState.standDown;

	private Tower closestTower;
	private bool canCharge = false;
	private bool isCharging = false;

	private void Start() {
		levelManager = FindObjectOfType<LevelManager>();
		character = FindObjectOfType<Character>();
	}

	void Update() {
		if (canCharge && Input.GetKey(KeyCode.Space)) {
			if ( closestTower.transform.position.x < this.transform.position.x) {
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
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
				lastInputState = PlayerAnimator.playerMoveState.walkUp;
			} else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
				lastInputState = PlayerAnimator.playerMoveState.walkDown;
			} else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
				lastInputState = PlayerAnimator.playerMoveState.walkLeft;
			} else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
				lastInputState = PlayerAnimator.playerMoveState.walkRight;
			}

			if (lastInputState == PlayerAnimator.playerMoveState.walkUp && isUpPressed()) {
				moveUp();
			} else if (lastInputState == PlayerAnimator.playerMoveState.walkDown && isDownPressed()) {
				moveDown();
			} else if (lastInputState == PlayerAnimator.playerMoveState.walkLeft && isLeftPressed()) {
				moveLeft();
			} else if (lastInputState == PlayerAnimator.playerMoveState.walkRight && isRightPressed()) {
				moveRight();
			} else if (isUpPressed()) {
				moveUp();
			} else if (isDownPressed()) {
				moveDown();
			} else if (isLeftPressed()) {
				moveLeft();
			} else if (isRightPressed()) {
				moveRight();
			} else {
				if (playerAnimator.state < PlayerAnimator.playerMoveState.standUp) {
					//Didn't move but is in walking animation
					playerAnimator.state += 4;//Move to "standing" animation
				}
			}

		}
	}

	private void FixedUpdate() {
		if (isCharging && closestTower != null) {
			closestTower.GetCharged();
		}
	}

	private void moveUp() {
		Vector3 position = this.transform.position;
		position.y += speed;
		playerAnimator.state = PlayerAnimator.playerMoveState.walkUp;
		this.transform.position = position;
	}

	private void moveDown() {
		Vector3 position = this.transform.position;
		position.y -= speed;
		playerAnimator.state = PlayerAnimator.playerMoveState.walkDown;
		this.transform.position = position;
	}

	private void moveLeft() {
		Vector3 position = this.transform.position;
		position.x -= speed;
		playerAnimator.state = PlayerAnimator.playerMoveState.walkLeft;
		this.transform.position = position;
	}

	private void moveRight() {
		Vector3 position = this.transform.position;
		position.x += speed;
		playerAnimator.state = PlayerAnimator.playerMoveState.walkRight;
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

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			Destroy(collision.gameObject);

			character.AdjustHitpoints(-1);
		}
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
}
