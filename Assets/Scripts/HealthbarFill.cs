using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarFill : MonoBehaviour {
    
    private float healthPercent = 1f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.localScale = new Vector3(healthPercent,1,1);
    }
}
