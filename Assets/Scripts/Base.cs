using UnityEngine;

public class Base : MonoBehaviour {
	public LevelManager levelManager;
	public PlayerHealth player;

    void Start() {
        player = FindObjectOfType<PlayerHealth>();
        levelManager = FindObjectOfType<LevelManager>();
    }

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy") {
			Destroy(col.gameObject);
            player.AdjustHitpoints(-1);
            GetComponent<PlayerInput>().recoilFromHit(.5f);
        }
	}
}
