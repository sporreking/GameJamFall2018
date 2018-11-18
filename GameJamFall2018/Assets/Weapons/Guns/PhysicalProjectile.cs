using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : MonoBehaviour {
    // Use this for initialization

    public ProjectileSO projectile;
	void Start () {
        GetComponent<SpriteRenderer>().sprite = projectile.image;

    }
	
	// Update is called once per frame
	void Update () {
        
       

	}
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.ChangeHealth(-projectile.damage);
        }
    }
}
