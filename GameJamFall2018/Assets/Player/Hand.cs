using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public Player Parent;

    public float RecoilFactor;
    public float MaxSpeed;

    public string InputX;
    public string InputY;

    public GameObject ForcePoint;

    private Rigidbody2D body;
    private SpringJoint2D Grope;

    public GameObject weapon;

    

    public void Awake() {
        body = GetComponent<Rigidbody2D>();
        Grope = GetComponent<SpringJoint2D>();
    }

    public void FixedUpdate()
    {

        
        if (!GrabDetect() && Grope.enabled)
        {
            Grope.enabled = false;
        }
    }

    public void Triggered(Collider2D collision)
    {
        if (GrabDetect() && (collision.gameObject.transform.parent != transform.parent || collision.gameObject != transform.parent))
        {
            if (!Grope.enabled && weapon == null)
            {
                Debug.Log("Grabbing "+collision.tag);
                if (collision.GetComponents<Rigidbody2D>().Length != 0)
                {
                    Grope.connectedBody = collision.GetComponent<Rigidbody2D>();
                }

                Grope.enabled = true;
            }
        }
    }

    private bool GrabDetect()
    {
        if (Parent.transform.Find("LeftHand").gameObject == this.gameObject)
        {
            return Input.GetAxis(Parent.PlayerInput.InputLeftGrab[Parent.PlayerInputIndex]) == 1F;
        }
        else
        {
            return Input.GetAxis(Parent.PlayerInput.InputRightGrab[Parent.PlayerInputIndex]) == 1F;
        }
        
    }

    private float diff(float a, float b) {
        if (a < b)
            return b - a;

        return a - b;
    }

    public void StartUse() {
        if (weapon) {
            GunScript g = weapon.GetComponent<GunScript>();
            if (g)
            {
                g.StartShooting();
            }

        }
    }

    public void StopUse() {
        if (weapon) {
            GunScript g = weapon.GetComponent<GunScript>();
            if (g) {
                g.StopShooting();
            }
        }

    }
    public void Drop()
    {
        if (weapon)
        {
            GunScript g = weapon.GetComponent<GunScript>();
            if (g)
            {
                g.release();
            }
        }

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
        
        
        body.AddForceAtPosition(new Vector2(MaxSpeed * m.x * diff(m.x, Mathf.Pow(vel.x / MaxSpeed, 3)) * Time.fixedDeltaTime,
            MaxSpeed * m.y * diff(m.y, Mathf.Pow(vel.y / MaxSpeed, 3)) * Time.fixedDeltaTime),
            new Vector2(ForcePoint.transform.position.x, ForcePoint.transform.position.y));

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
