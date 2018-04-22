using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class WavesText : MonoBehaviour {
	public String prefix;
	
	private EnemySpawner enemySpawner;

	void Start() {
		enemySpawner = FindObjectOfType<EnemySpawner>();
	}

	void Update() {
		Text text = this.GetComponent<Text>();
		text.text = prefix + enemySpawner.RemainingWaves();
	}
}
