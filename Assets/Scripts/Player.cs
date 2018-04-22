using UnityEngine;

public class Player : MonoBehaviour {
	public float speed;
	public PlayerAnimator playerAnimator;
  LevelManager levelManager;

  private PlayerAnimator.playerMoveState lastInputState = PlayerAnimator.playerMoveState.standDown;

  private void Start() {
    levelManager = FindObjectOfType<LevelManager>();
  }

	void Update() {
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
      //Player died
      levelManager.EndLevel();
    }
  }
}
