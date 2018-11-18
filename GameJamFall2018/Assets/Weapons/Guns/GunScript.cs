using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {
 
    
    public GunSO gun;
    private GameObject barell;
   
    
	// Use this for initialization
	void Start () {
        name = "Glock";
        GetComponent<SpriteRenderer>().sprite = gun.sprite;
        barell =transform.GetChild(0).gameObject;
        barell.transform.localPosition = gun.BarrelPosition;
        InvokeRepeating("shoot", 0, 1 / gun.BulletsPerSecond);
      

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) {
            gun.Shooting = !gun.Shooting;

        }

    }
    void shoot()
    {
        if (gun.Shooting)
        {
            float angleRad = transform.rotation.eulerAngles.z * Mathf.PI / 180;
            GameObject p = Instantiate(gun.Projectile, barell.transform.position, transform.rotation);
            p.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angleRad) * gun.ProjectileVelocity, Mathf.Sin(angleRad) * gun.ProjectileVelocity);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Hand") {

            transform.eulerAngles = new Vector3(0, 0, collision.gameObject.transform.eulerAngles.z);
            transform.Rotate(new Vector3(0, 0, -90));

            GetComponent<FixedJoint2D>().enabled = true;
            GetComponent<FixedJoint2D>().connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            GetComponent<FixedJoint2D>().anchor = gun.GrabPosition;//grip.transform.localPosition;
            GetComponent<FixedJoint2D>().connectedAnchor = new Vector2(0,0);
           

            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().mass = 0;
        }
    }

}