using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

  public int hitPoints = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if (hitPoints <= 0) {
      Die();
    }
	}

  public void AdjustHitpoints (int damage) {
    hitPoints += damage;
    gameObject.SendMessage("HitpointsChanged", SendMessageOptions.DontRequireReceiver);
  }

  public void Die() {
    Destroy(gameObject);
  }
}
