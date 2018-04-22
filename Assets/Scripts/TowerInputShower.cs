using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInputShower : MonoBehaviour {

    public SpriteRenderer spaceToChargeSprite;

    private Tower tower;

    // Use this for initialization
    void Start () {
        HideSpaceToCharge();
        tower = GetComponentInParent<Tower>();
    }
	
	// Update is called once per frame
	void Update () {
        if (spaceToChargeSprite.enabled)
        {
            if (Time.time % 1f < .5f)
            {
                spaceToChargeSprite.color = Color.white;
            }
            else
            {
                spaceToChargeSprite.color = Color.black;
            }
        }
    }
    
    public void ShowSpaceToCharge()
    {
        spaceToChargeSprite.enabled = true;
    }

    public void HideSpaceToCharge()
    {
        spaceToChargeSprite.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "Player")
        {
            ShowSpaceToCharge();

            Player p = collision.gameObject.GetComponent<Player>();
            if (p != null)
            {
                p.setClosestTower(tower);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HideSpaceToCharge();

            Player p = collision.gameObject.GetComponent<Player>();
            if( p != null)
            {
                p.eraseClosestTower(tower);
            }

        }
    }
}
