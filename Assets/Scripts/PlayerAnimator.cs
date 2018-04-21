using System.Collections;
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
        standRight
    }

    public playerMoveState state = playerMoveState.standDown;

    private float lastChanged;
    private playerMoveState previousState = playerMoveState.standDown;
    private SpriteRenderer spriteComp;

    public Sprite[] allSprites = new Sprite[12];

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
                currentSprites[3] = allSprites[8];
                currentSprites[0] = allSprites[9];
                currentSprites[1] = allSprites[10];
                currentSprites[2] = allSprites[11];
                animationLength = 4;
                break;
            case playerMoveState.walkDown:
                currentSprites[3] = allSprites[0];
                currentSprites[0] = allSprites[1];
                currentSprites[1] = allSprites[2];
                currentSprites[2] = allSprites[3];
                animationLength = 4;
                break;
            case playerMoveState.walkLeft:
            case playerMoveState.walkRight:
                currentSprites[3] = allSprites[4];
                currentSprites[0] = allSprites[5];
                currentSprites[1] = allSprites[6];
                currentSprites[2] = allSprites[7];
                animationLength = 4;
                flipX = state == playerMoveState.walkRight;
                break;
            case playerMoveState.standUp:
                currentSprites[0] = allSprites[8];
                animationLength = 1;
                break;
            case playerMoveState.standDown:
                currentSprites[0] = allSprites[0];
                animationLength = 1;
                break;
            case playerMoveState.standLeft:
            case playerMoveState.standRight:
                currentSprites[0] = allSprites[4];
                animationLength = 1;
                flipX = state == playerMoveState.standRight;
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
