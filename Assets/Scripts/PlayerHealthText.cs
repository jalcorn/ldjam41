using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerHealthText : MonoBehaviour {
	public String prefix;

	void Start() {

	}

    public void SetHealthText( int amount ) {
        Text text = this.GetComponent<Text>();
        text.text = prefix + amount;
    }
}
