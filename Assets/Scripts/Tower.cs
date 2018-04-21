using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

  public float delayToStart = 5.0f;
  public float secondsBetweenAttack = 1.0f;
  public int damagePerAttack = -1;
  public float damageRadius = 2f;
  CircleCollider2D trigger;
  TowerAnimation attackAnimation;

  List<Character> charactersInRange = new List<Character>();

	// Use this for initialization
	void Start () {
    var rigidbody = gameObject.AddComponent<Rigidbody2D>();
    rigidbody.gravityScale = 0f;
    trigger = gameObject.AddComponent<CircleCollider2D>();
    trigger.isTrigger = true;
    trigger.radius = damageRadius;
    InvokeRepeating("Attack", delayToStart, secondsBetweenAttack);
    attackAnimation = gameObject.GetComponentInChildren<TowerAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void Attack () {
    attackAnimation.Play();
    foreach (Character character in charactersInRange) {
      character.AdjustHitpoints(damagePerAttack);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    Character character = collision.gameObject.GetComponent<Character>();
    if (character != null) {
      charactersInRange.Add(character);
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    Character character = collision.gameObject.GetComponent<Character>();
    if (character != null) {
      charactersInRange.Remove(character);
    }
  }
}
