using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun1", menuName = "Weapons/Gun")]
public class GunSO : ScriptableObject {
    public new string name;
    public bool Shooting;
    public GameObject Projectile;
    public float ProjectileVelocity;
    public float Angle;
    public Vector2 BarrelPosition;
    public float BulletsPerSecond;
    public Sprite sprite;
    public Vector2 GrabPosition;

    


}
