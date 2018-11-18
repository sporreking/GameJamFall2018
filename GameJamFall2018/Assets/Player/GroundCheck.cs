using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public int Jumps;

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "StaticTerrain") {
            Jumps = 2;
        }
    }

    /*public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "StaticTerrain")
        {

        }
    }*/
}
