using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Weapon", menuName = "Weapon/Melee")]
public class MeleeSO : ScriptableObject {
    public Sprite Image;
    public Vector2 GrabPosition; // Where hand grips object.
    public int Uses;                // Amount of uses/ammo/charge.
    public int Damage;
}
