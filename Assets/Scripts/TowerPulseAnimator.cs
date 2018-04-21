using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPulseAnimator : MonoBehaviour
{
    private SpriteRenderer sprite;

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
        sprite.color -= new Color(0, 0, 0, 1f / 20f);
    }

    public void Attack()
    {
        sprite.color = Color.white;
    }
}