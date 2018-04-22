using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Attacks
    public float delayToStart = 5.0f;
    public float secondsBetweenAttack = 1.0f;
    public int damagePerAttack = -1;
    CircleCollider2D trigger;


    //Powerlevel
    public float powerLevel = 1;
    float maxPowerLevel = 1;
    float minPowerLevel = 0;
    float powerLostPerSec = 1f / 15f;//15 seconds from 100% to 0%


    public TowerAnimation towerSprite;
    public TowerPulseAnimator fieldSprite;
    public BatteryAnimator batterySprite;

    List<Character> charactersInRange = new List<Character>();

    // Use this for initialization
    void Start()
    {
        trigger = gameObject.GetComponent<CircleCollider2D>();
        InvokeRepeating("Attack", delayToStart, secondsBetweenAttack);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        powerLevel -= Time.fixedDeltaTime * powerLostPerSec;


        powerLevel = Mathf.Max(minPowerLevel, powerLevel);
        powerLevel = Mathf.Min(maxPowerLevel, powerLevel);


        batterySprite.powerLevel = powerLevel;
        towerSprite.framerate = Mathf.FloorToInt(powerLevel * 4) + 3;

        if (powerLevel > minPowerLevel)
        {
            towerSprite.state = TowerAnimation.towerSpriteState.on;
        }
        else
        {
            towerSprite.state = TowerAnimation.towerSpriteState.off;
        }
    }

    void Attack()
    {
        if (powerLevel > minPowerLevel)
        {

            fieldSprite.Attack();

            foreach (Character character in charactersInRange)
            {
                character.AdjustHitpoints(damagePerAttack);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null)
        {
            charactersInRange.Add(character);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null)
        {
            charactersInRange.Remove(character);
        }
    }
}
