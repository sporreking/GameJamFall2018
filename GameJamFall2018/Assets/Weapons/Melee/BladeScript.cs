using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour {

    public GameObject Axe;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Axe.GetComponent<Melee>().hand)
        {
            if (Axe.GetComponent<Melee>().hand.GetComponentInParent<Player>() != collision.gameObject.GetComponentInParent<Player>())
            {
                collision.gameObject.GetComponentInParent<Player>().Health -= Axe.GetComponent<Melee>().Damage;
            }
        }
    }

}
