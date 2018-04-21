using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {
	public GameObject game;
	public GameObject menu;

	void OnMouseDown(){
		game.SetActive(true);
		menu.SetActive(false);
	}
}
