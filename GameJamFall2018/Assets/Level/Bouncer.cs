using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour {

    public float BounceForce;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(- new Vector2(Mathf.Cos(transform.eulerAngles.z / 180 * Mathf.PI),
                Mathf.Sin(transform.eulerAngles.z / 180 * Mathf.PI)) * BounceForce);
        }
    }

}
