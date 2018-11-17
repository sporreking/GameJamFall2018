using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Vector2 InitialVelocity;
    public float InitalAngularVelocity;

    private Rigidbody2D rb;
    private float timeDistortion;
    private List<Timewarper> timewarpers;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = InitialVelocity;
        rb.angularVelocity = InitalAngularVelocity;
        rb.gravityScale = 0.3F;

        timewarpers = new List<Timewarper>();

        timeDistortion = 0.0F;
        foreach (GameObject ob in GameObject.FindGameObjectsWithTag("Timewarper"))
        {
            timewarpers.Add(ob.GetComponent<Timewarper>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float newTimeDistortion = 0;
        foreach (Timewarper tw in timewarpers)
        {
            Vector2 toPoint = (Vector2)tw.transform.position - rb.position;
            newTimeDistortion += TimeDistortion(toPoint.magnitude, tw.getWarpDistance()) * tw.getWarpFactor();
        }
        
        float deltaTimeDistortion = newTimeDistortion - timeDistortion;
        timeDistortion = newTimeDistortion;

        rb.AddForce(-rb.velocity * rb.mass * deltaTimeDistortion * Time.fixedDeltaTime * 200F);
        rb.AddTorque(-Equalizer(rb.angularVelocity) * deltaTimeDistortion * Time.fixedDeltaTime * 1F);
        rb.gravityScale = deltaTimeDistortion + 1F;

    }

    private float TimeDistortion(float distance, float warpDistance)
    {
        return warpDistance / (distance + 0.5F);

    }

    private float Equalizer(float x)
    {
        if (x <= -1 || 1 <= x)
        {
            return x;
        }
        else
        {
            return Mathf.Pow(x, 5);
        }
    }
}
