using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAreaScript : MonoBehaviour {

    public Melee melee;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Hand>())
        {
            melee.Pickup(collision);
        }
    }
}
