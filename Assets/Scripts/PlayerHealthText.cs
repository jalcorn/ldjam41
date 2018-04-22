using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerHealthText : MonoBehaviour {
	public String prefix;
	public Character character;

	void Update() {
		Text text = this.GetComponent<Text>();
		text.text = prefix + character.hitPoints;
	}
}
