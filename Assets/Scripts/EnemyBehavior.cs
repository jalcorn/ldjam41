using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
	public GameObject trailEffect;

	private EnemyAnimator animator;

	void Start() {
		Instantiate(trailEffect, this.transform);
		InitPathing();
		animator = GetComponentInChildren<EnemyAnimator>();
	}

	void Update() {
		movementLogic.Move();
		Vector3 forwardVector = movementLogic.GetForwardVector();
		float angle = Mathf.Atan2(forwardVector.y, forwardVector.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		visionObject.transform.rotation = Quaternion.RotateTowards(visionObject.transform.rotation, q, Mathf.PI);

		if (Mathf.Abs(angle) < 45) {
			animator.state = EnemyAnimator.enemyMoveState.walkRight;
		} else if (Mathf.Abs(angle) > 135) {
			animator.state = EnemyAnimator.enemyMoveState.walkLeft;
		} else if (angle > 0) {
			animator.state = EnemyAnimator.enemyMoveState.walkUp;
		} else {
			animator.state = EnemyAnimator.enemyMoveState.walkDown;
		}

	}

	//AI//

	public float speed;
	public MovementType movementType;
	public float visionDistance = 2f;
	public GameObject visionObject;

	[HideInInspector]
	public GameObject sightedPlayer = null;

	[HideInInspector]
	public Pathing pathing;

	private EnemyVision vision;
	private FollowPathMovement movementLogic;

	void InitPathing() {
		pathing = FindObjectOfType<Pathing>();

		visionObject.transform.localScale *= visionDistance;
		vision = visionObject.GetComponent<EnemyVision>();
		vision.watchForPlayer(playerSighted);
		switch (movementType) {
			case MovementType.LookAroundPath:
				movementLogic = new LookAroundMovement(gameObject, speed, pathing.GetPathPositions());
				break;
			case MovementType.SimplePath:
				movementLogic = new FollowPathMovement(gameObject, speed, pathing.GetPathPositions());
				break;
		}
	}

	void playerSighted(GameObject player) {
		if (sightedPlayer == null) {
			movementLogic.SetHighPriorityTarget(player.transform);
		}
	}

	public enum MovementType {
		SimplePath,
		LookAroundPath
	}
}
