using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSoundTrigger : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerBox")
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
