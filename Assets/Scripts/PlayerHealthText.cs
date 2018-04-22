using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerHealthText : MonoBehaviour {
	public String prefix;

	private Character character;

	void Start() {
		character = FindObjectOfType<Character>();
	}

	void Update() {
		Text text = this.GetComponent<Text>();
		text.text = prefix + character.hitPoints;
	}
}
