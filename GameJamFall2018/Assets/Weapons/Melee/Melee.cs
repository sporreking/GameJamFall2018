using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {
    public MeleeSO MeleeDesc;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hand")
        {
            transform.eulerAngles = new Vector3(0, 0, collision.gameObject.transform.eulerAngles.z);
            transform.Rotate(new Vector3(0, 0, -90));

            GetComponent<FrictionJoint2D>().enabled = true;
            GetComponent<FrictionJoint2D>().connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            GetComponent<FrictionJoint2D>().anchor = MeleeDesc.GrabPosition;
            GetComponent<FrictionJoint2D>().connectedAnchor = new Vector2(0, 0);


            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().mass = 0;
        }

        if (collision.gameObject.tag == "Player")
        {
            
            
        }
    }
}
