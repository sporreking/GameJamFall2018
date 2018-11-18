using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColliderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay2D(Collider2D collision)
    {
        transform.parent.GetComponent<Hand>().Triggered(collision);
    }
}
