using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteZAdjust : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;

        targetPos.z = myPos.y * .001f;

        target.transform.position = targetPos;
	}
}
