﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public enum playerMoveState
    {
        walkUp,
        walkDown,
        walkLeft,
        walkRight,
        standUp,
        standDown,
        standLeft,
        standRight,
        fixLeft,
        fixRight,
        death,
    }

    public playerMoveState state = playerMoveState.standDown;

    private float lastChanged;
    private playerMoveState previousState = playerMoveState.standDown;
    private SpriteRenderer spriteComp;

    public Sprite[] movementSprites = new Sprite[12];

    public Sprite[] fixTowerSprites = new Sprite[2];

    public Sprite deathSprite;

    private Sprite[] currentSprites = new Sprite[12];
    private int animationLength = 1;

    private bool flipX = false;

    public float frameRate = 6.0f;

    // Use this for initialization
    void Start()
    {
        spriteComp = GetComponent<SpriteRenderer>();

        lastChanged = Time.time;
        refreshFrames();

        spriteComp.flipX = flipX;

        previousState = state;
    }

    // Update is called once per frame
    void Update()
    {
        if (previousState != state)
        {
            previousState = state;

            lastChanged = Time.time;

            refreshFrames();

            spriteComp.flipX = flipX;
        }

        spriteComp.sprite = currentSprites[getCurrentFrame()];
    }

    private void refreshFrames()
    {
        flipX = false;
        switch (state)
        {
            case playerMoveState.walkUp:
                currentSprites[3] = movementSprites[8];
                currentSprites[0] = movementSprites[9];
                currentSprites[1] = movementSprites[10];
                currentSprites[2] = movementSprites[11];
                animationLength = 4;
                break;
            case playerMoveState.walkDown:
                currentSprites[3] = movementSprites[0];
                currentSprites[0] = movementSprites[1];
                currentSprites[1] = movementSprites[2];
                currentSprites[2] = movementSprites[3];
                animationLength = 4;
                break;
            case playerMoveState.walkLeft:
            case playerMoveState.walkRight:
                currentSprites[3] = movementSprites[4];
                currentSprites[0] = movementSprites[5];
                currentSprites[1] = movementSprites[6];
                currentSprites[2] = movementSprites[7];
                animationLength = 4;
                flipX = state == playerMoveState.walkRight;
                break;
            case playerMoveState.standUp:
                currentSprites[0] = movementSprites[8];
                animationLength = 1;
                break;
            case playerMoveState.standDown:
                currentSprites[0] = movementSprites[0];
                animationLength = 1;
                break;
            case playerMoveState.standLeft:
            case playerMoveState.standRight:
                currentSprites[0] = movementSprites[4];
                animationLength = 1;
                flipX = state == playerMoveState.standRight;
                break;
            case playerMoveState.fixLeft:
            case playerMoveState.fixRight:
                currentSprites[0] = fixTowerSprites[0];
                currentSprites[1] = fixTowerSprites[1];
                animationLength = 2;
                flipX = state == playerMoveState.fixRight;
                break;
            case playerMoveState.death:
                currentSprites[0] = deathSprite;
                animationLength = 1;
                break;
        }
    }

    private int getCurrentFrame()
    {
        if (animationLength <= 1)
        {
            return 0;
        }

        var secondsSinceChanged = Time.time - lastChanged;
        return Mathf.FloorToInt(secondsSinceChanged * frameRate) % animationLength;
    }
}
