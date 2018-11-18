using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {
    public MeleeSO MeleeDesc;

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.ChangeHealth(damage);
            // Play sound.
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
