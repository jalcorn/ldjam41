using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryAnimator : MonoBehaviour
{


    public float powerLevel = 1;

    private SpriteRenderer sprite;

    public Sprite[] allSprites = new Sprite[4];

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        powerLevel = Mathf.Max(0, powerLevel);
        powerLevel = Mathf.Min(.999f, powerLevel);

        if(powerLevel == 0)
        {
            sprite.sprite = allSprites[0];
        }
        else
        {
            sprite.sprite = allSprites[Mathf.FloorToInt(powerLevel * 3) + 1];
        }
    }
}
