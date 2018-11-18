using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {
 
    
    public GunSO gun;
    private GameObject barell;
    private int bulletsLeft;
    private bool shooting;
    private float gravityscale;
    private float mass;
    private Hand hand;

    private float timer = 0;
    public float DropTimer;
    

	// Use this for initialization
	void Start () {
        name = "Glock";
        GetComponent<SpriteRenderer>().sprite = gun.sprite;
        barell =transform.GetChild(0).gameObject;
        barell.transform.localPosition = gun.BarrelPosition;
        InvokeRepeating("shoot", 0, 1 / gun.BulletsPerSecond);
        bulletsLeft = gun.AmmoCount;
        shooting = gun.Shooting;
        gravityscale = GetComponent<Rigidbody2D>().gravityScale;
        mass = GetComponent<Rigidbody2D>().mass;


    }

    // Update is called once per frame
    void Update(){

        if (timer > 0)
            timer -= Time.deltaTime;
        
        if (bulletsLeft<= 0) {
            Object.Destroy(this.gameObject);
     

        }

    }
    void shoot()
    {
        Debug.Log("Hello", this);
        if (shooting && hand)
        {
            float angleRad = transform.rotation.eulerAngles.z * Mathf.PI / 180;
            GameObject p = Instantiate(gun.Projectile, barell.transform.position, transform.rotation);
            p.GetComponent<PhysicalProjectile>().self = hand.GetComponentInParent<Player>();
            p.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angleRad) * gun.ProjectileVelocity, Mathf.Sin(angleRad) * gun.ProjectileVelocity);
            bulletsLeft--;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        
        if (timer <= 0 && collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Hand>())
        {
            
            if (!hand) {
                Hand h = collision.gameObject.GetComponent<Hand>();
                if (h.weapon) {
                    return;
                }
                hand = h;
                transform.eulerAngles = new Vector3(0, 0, collision.gameObject.transform.eulerAngles.z);
                transform.Rotate(new Vector3(0, 0, -90));
                GetComponent<FixedJoint2D>().enabled = true;
                GetComponent<FixedJoint2D>().connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
                GetComponent<FixedJoint2D>().anchor = gun.GrabPosition;//grip.transform.localPosition;
                GetComponent<FixedJoint2D>().connectedAnchor = new Vector2(0,0);
                GetComponent<PolygonCollider2D>().isTrigger = true;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().mass = 0;
                hand.weapon = this.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!hand && collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Hand>())
            GetComponent<PolygonCollider2D>().isTrigger = false;
    }

    public void release() {
        if (hand && hand.weapon)
        {
            Debug.Log("release");
            GetComponent<FixedJoint2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = gravityscale;
            GetComponent<Rigidbody2D>().mass = mass;
            shooting = false;
            timer = DropTimer;
            hand.weapon = null;
            hand = null;
        }
        

    }
    public void StartShooting()
    {
        shooting = true;
    }

    public void StopShooting() {

        shooting = false;
    }
}