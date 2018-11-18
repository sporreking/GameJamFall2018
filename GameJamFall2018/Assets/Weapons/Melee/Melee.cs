using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    public MeleeSO MeleeDesc;

    public GameObject Grip;
    public GameObject Blade;

    public Hand hand;

    public int Damage;

    private float mass;

    private float timer = 0;
    public float DropTimer;

    

    private void Start()
    {
        mass = GetComponent<Rigidbody2D>().mass;
    }

    public void Pickup(Collider2D collision)
    {
        // Puts weapon in hand
        if (timer <= 0)
        {
            //Debug.Log("Pickup", this);
            if (!hand)
            {
                Hand h = collision.gameObject.GetComponent<Hand>();
                if (h.weapon)
                {
                    return;
                }
                hand = h;
                transform.eulerAngles = new Vector3(0, 0, collision.gameObject.transform.eulerAngles.z);
                transform.Rotate(new Vector3(0, 0, -90));
                GetComponent<FixedJoint2D>().enabled = true;
                GetComponent<FixedJoint2D>().connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
                GetComponent<FixedJoint2D>().anchor = Grip.transform.localPosition;
                GetComponent<FixedJoint2D>().connectedAnchor = new Vector2(0, 0);
                GetComponent<BoxCollider2D>().isTrigger = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().mass = 0;
                hand.weapon = this.gameObject;
                Blade.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        

        // 
        /*if (!GetComponent<EdgeCollider2D>().enabled) // Axe is not stuck to player.
        {
            // Put weapon on enemy
            Debug.Log("Not stuck.", this);
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Stick to enemy", this);
                transform.eulerAngles = new Vector3(0, 0, collision.gameObject.transform.eulerAngles.z);
                transform.Rotate(new Vector3(0, 0, -90));
                GetComponent<FrictionJoint2D>().enabled = true;
                GetComponent<FrictionJoint2D>().connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
                GetComponent<FrictionJoint2D>().anchor = collision.GetContact(0).point; // Connect axe to point of contact.
                GetComponent<FrictionJoint2D>().connectedAnchor = new Vector2(0, 0);

                GetComponent<EdgeCollider2D>().enabled = false; // Disable blade
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().mass = 0;
            }
        }*/
    }
}
