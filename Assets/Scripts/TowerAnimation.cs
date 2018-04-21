using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimation : MonoBehaviour
{

    private SpriteRenderer sprite;

    public Sprite[] allSprites = new Sprite[3];

    public float framerate;
    public towerSpriteState state;


    public enum towerSpriteState
    {
        on,
        off
    }

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        framerate = 6f;
    }

    // Update is called once per frame
    void Update()
    {

        if (state == towerSpriteState.off)
        {
            sprite.sprite = allSprites[0];
        }
        else
        {
            if (Mathf.FloorToInt(Time.time * framerate) % 2 == 0)
            {
                sprite.sprite = allSprites[1];
            }
            else
            {
                sprite.sprite = allSprites[2];
            }
        }
    }

}
