using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {
	public GameObject game;
	public GameObject menu;

	void OnMouseDown(){
		Debug.Log("Clicked Start!");
		game.SetActive(true);
		menu.SetActive(false);
	}
}
