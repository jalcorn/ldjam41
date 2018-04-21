using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {

  public delegate void PlayerSighted(Player player);

  PlayerSighted playerSightedHandler;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.CompareTag("Player")) {
      playerSightedHandler(collision.gameObject.GetComponent<Player>());
    }
  }

  public void watchForPlayer(PlayerSighted handler) {
    playerSightedHandler = handler;
  }
}
