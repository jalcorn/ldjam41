using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimation : MonoBehaviour {

  new SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
    renderer = GetComponent<SpriteRenderer>();
    Hide();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void Play() {
    Show();
    Invoke("Hide", 0.15f);
  }

  void Show() {
    renderer.enabled = true;
  }

  void Hide() {
    renderer.enabled = false;
  }
}
