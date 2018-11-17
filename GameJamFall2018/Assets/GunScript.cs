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
        GetComponent<FixedJoint2D>().anchor = gun.GrabPosition;

	}

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, gun.Angle);

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
    
}