using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyVision : MonoBehaviour {

  public Color neutralColor;
	public Color almostDetectedColor;
  public Color detectedColor;

  public SpriteRenderer indicator;

  public delegate void PlayerSighted(GameObject player);

  PlayerSighted playerSightedHandler;

	Collider2D detectedTrigger;
	Collider2D almostDetectedTrigger;
	ContactFilter2D filter = new ContactFilter2D().NoFilter();

	DetectedState state = DetectedState.neutral;

	// Use this for initialization
	void Start () {
    indicator.color = neutralColor;
		Collider2D[] colliders = GetComponents<Collider2D>();
		detectedTrigger = colliders[0];
		almostDetectedTrigger = colliders[1];
	}
	
	// Update is called once per frame
	void Update () {
		
  }

  private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			Collider2D[] colliders = new Collider2D[20];
			other.OverlapCollider(filter, colliders);
			if (state == DetectedState.neutral && Array.Find(colliders, trigger => trigger == almostDetectedTrigger) != null) {				
				indicator.color = almostDetectedColor;
				state = DetectedState.almost;
			} else if (state != DetectedState.detected && Array.Find(colliders, trigger => trigger == detectedTrigger) != null) {
				playerSightedHandler(other.gameObject);
				indicator.color = detectedColor;
				state = DetectedState.detected;
			}
    }
  }

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) {
			Collider2D[] colliders = new Collider2D[10];
			other.OverlapCollider(filter, colliders);
			if (state == DetectedState.almost && Array.Find(colliders, trigger => trigger == almostDetectedTrigger) == null) {
				indicator.color = neutralColor;
				state = DetectedState.neutral;
			}
		}
	}

	public void watchForPlayer(PlayerSighted handler) {
    playerSightedHandler = handler;
  }

	enum DetectedState {
		neutral,
		almost,
		detected
	}
}
