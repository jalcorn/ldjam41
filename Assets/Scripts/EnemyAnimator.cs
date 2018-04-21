using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public enum enemyMoveState
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

    public enemyMoveState state = enemyMoveState.standDown;

    private float lastChanged;
    private enemyMoveState previousState = enemyMoveState.standDown;
    private SpriteRenderer spriteComp;

    public Sprite[] allSprites = new Sprite[6];

    private Sprite[] currentSprites = new Sprite[6];
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
            case enemyMoveState.walkUp:
                currentSprites[0] = allSprites[4];
                currentSprites[1] = allSprites[5];
                animationLength = 2;
                break;
            case enemyMoveState.walkDown:
                currentSprites[0] = allSprites[0];
                currentSprites[1] = allSprites[1];
                animationLength = 2;
                break;
            case enemyMoveState.walkLeft:
                currentSprites[0] = allSprites[2];
                currentSprites[1] = allSprites[3];
                animationLength = 2;
                break;
            case enemyMoveState.walkRight:
                currentSprites[0] = allSprites[2];
                currentSprites[1] = allSprites[3];
                animationLength = 2;
                flipX = true;
                break;
            case enemyMoveState.standUp:
                currentSprites[0] = allSprites[4];
                animationLength = 1;
                break;
            case enemyMoveState.standDown:
                currentSprites[0] = allSprites[0];
                animationLength = 1;
                break;
            case enemyMoveState.standLeft:
                currentSprites[0] = allSprites[2];
                animationLength = 1;
                break;
            case enemyMoveState.standRight:
                currentSprites[0] = allSprites[2];
                animationLength = 1;
                flipX = true;
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
