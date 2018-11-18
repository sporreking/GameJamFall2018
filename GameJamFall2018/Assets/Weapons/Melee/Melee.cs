using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {
    public MeleeSO MeleeDesc;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Hand>())
        {
            Debug.Log("Pickup", this);
            transform.eulerAngles = new Vector3(0, 0, collision.gameObject.transform.eulerAngles.z);
            transform.Rotate(new Vector3(0, 0, -90));

            GetComponent<FixedJoint2D>().enabled = true;
            GetComponent<FixedJoint2D>().connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            GetComponent<FixedJoint2D>().anchor = MeleeDesc.GrabPosition;
            GetComponent<FixedJoint2D>().connectedAnchor = new Vector2(0, 0);

            GetComponent<BoxCollider2D>().enabled = false; // Disable handle
            GetComponent<EdgeCollider2D>().enabled = true; // Enable blade

            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().mass = 0;
        }

        if (!GetComponent<EdgeCollider2D>().enabled) // Axe is not stuck to player.
        {
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
        }
    }
}
