using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public List<GameObject> levels;

	// Use this for initialization
	void Start () {
		Instantiate(levels[0], this.transform);
	}

	// Update is called once per frame
	void Update () {

	}
}