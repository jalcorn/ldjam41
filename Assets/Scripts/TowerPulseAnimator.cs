using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPulseAnimator : MonoBehaviour
{
    private SpriteRenderer sprite;

    private float frameRate = 6f;
    private bool isOn = false;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        sprite.color = Color.white;
        if (!isOn) {
            sprite.enabled = false;
        } else {
            sprite.enabled = true;
            if (Time.time * frameRate % 2 < 1) {
                sprite.color -= new Color(0, 0, 0, .4f);
            } else {
                sprite.color -= new Color(0, 0, 0, .6f);
            }
        }
    }

    public void TurnOn(bool on) {
        isOn = on;
		this.gameObject.SetActive(on);
    }
}