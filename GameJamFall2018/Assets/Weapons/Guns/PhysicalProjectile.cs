using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : MonoBehaviour {
    // Use this for initialization

    public Player self;

    public ProjectileSO projectile;
	void Start () {
        GetComponent<SpriteRenderer>().sprite = projectile.image;

    }
	
	// Update is called once per frame
	void Update () {
        
       

	}
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player" )
        {
            
            Player p = collision.gameObject.GetComponentInParent<Player>();
            Debug.Log("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE " + p+ " Self: "+self,this);
            if (p && p!=self) {
               
                p.ChangeHealth(-projectile.damage);
            }
            

        }
        Object.Destroy(this.gameObject);
    }
}
