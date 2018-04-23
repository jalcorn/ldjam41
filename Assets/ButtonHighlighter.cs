using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHighlighter : MonoBehaviour {

    private SpriteRenderer sprite;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver() {
        sprite.color = Color.gray;
    }
    private void OnMouseExit() {
        sprite.color = Color.white;
    }
}
