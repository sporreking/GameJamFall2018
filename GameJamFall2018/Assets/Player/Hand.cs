using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public Player Parent;

    public float RecoilFactor;
    public float MaxSpeed;

    public string InputX;
    public string InputY;

    private Rigidbody2D body;

    public void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    private float diff(float a, float b) {
        if (a < b)
            return b - a;

        return a - b;
    }

    // Returns the direction which the player should move in
    public float Move() {
        if (!body)
        {
            return 0;
        }

        if (body.velocity.sqrMagnitude > MaxSpeed * MaxSpeed) {
            Vector2 v = new Vector2(body.velocity.x, body.velocity.y);

            v.Normalize();

            body.velocity = new Vector3(v.x * MaxSpeed, v.y * MaxSpeed, 0);
        }

        Vector2 m = new Vector2(Input.GetAxis(InputX), Input.GetAxis(InputY));
        Vector3 vel = body.velocity;
        
        body.AddForce(new Vector2(MaxSpeed * m.x * diff(m.x, Mathf.Pow(vel.x / MaxSpeed, 3)) * Time.fixedDeltaTime,
            MaxSpeed * m.y * diff(m.y, Mathf.Pow(vel.y / MaxSpeed, 3)) * Time.fixedDeltaTime));

        if (transform.localPosition.sqrMagnitude > Parent.HandRadius * Parent.HandRadius) {

            Vector2 nVel = new Vector2(vel.x, vel.y);

            nVel.x -= transform.localPosition.x * RecoilFactor / Parent.HandRadius;
            nVel.y -= transform.localPosition.y * RecoilFactor / Parent.HandRadius;

            body.velocity = nVel;
        }

        if (m.x != 0 && transform.localPosition.sqrMagnitude > (Parent.HandRadius - Parent.MoveBorder) * (Parent.HandRadius - Parent.MoveBorder)) {
            return transform.localPosition.x / Parent.HandRadius;
        }

        return 0;
    }
}
