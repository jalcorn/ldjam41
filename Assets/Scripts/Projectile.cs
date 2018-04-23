using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	Transform target;
	new Rigidbody2D rigidbody;
	public float Velocity;
	public int damage;

	// Use this for initialization
	void Start () {
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			rigidbody.velocity = (target.position - transform.position).normalized * Velocity;
		}
	}

	public void SetTarget(Transform target) {
		this.target = target;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			EnemyHealth health = collision.gameObject.GetComponent<EnemyHealth>();
			health.AdjustHitpoints(-damage);
			target = null;
			Destroy(gameObject);
		}
  }
}
