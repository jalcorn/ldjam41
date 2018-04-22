using UnityEngine;

public class Base : MonoBehaviour {
	public LevelManager levelManager;
	public Character playerCharacter;

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy") {
			Destroy(col.gameObject);
			playerCharacter.AdjustHitpoints(-1);
		}
	}
}
