using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {
    public MeleeSO MeleeDesc;

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            //collision.gameObject.applyDamage(damage);
            // Play sound.
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
