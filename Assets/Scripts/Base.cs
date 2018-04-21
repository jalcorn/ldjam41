using UnityEngine;

public class Base : MonoBehaviour {
	public LevelManager levelManager;

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy") {
			Destroy(col.gameObject);
			levelManager.levelState.SetCurrentState(LevelState.State.Ending);
		}
	}
}
