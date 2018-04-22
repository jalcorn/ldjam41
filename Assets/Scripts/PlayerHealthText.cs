using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerHealthText : MonoBehaviour {
	public String prefix;

	private Character character;

	void Start() {
		character = FindObjectOfType<Character>();

		character.OnHealthChange += OnHealthChange;
	}

	private void OnHealthChange(int prev, int next) {
		Text text = this.GetComponent<Text>();
		text.text = prefix + next;
	}
}
