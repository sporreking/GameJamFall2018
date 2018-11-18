using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public float Speed;

    public GameObject Upper;
    public GameObject Lower;

    private bool up = false;

    public void FixedUpdate()
    {
        if (up) {
            if (transform.position.y < Upper.transform.position.y)
            {
                transform.Translate(0, Speed * Time.fixedDeltaTime, 0);

                if (transform.position.y >= Upper.transform.position.y)
                    up = false;
            }
        }
        else
        {
            if (transform.position.y > Lower.transform.position.y)
            {
                transform.Translate(0, - Speed * Time.fixedDeltaTime, 0);

                if (transform.position.y <= Lower.transform.position.y)
                    up = true;
            }
        }
    }


}
