using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	void OnMouseDown() {
		LoadFirstScene();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
			LoadFirstScene();
		}
	}

	private void LoadFirstScene() {
		SceneManager.LoadScene("level1");
	}
}
