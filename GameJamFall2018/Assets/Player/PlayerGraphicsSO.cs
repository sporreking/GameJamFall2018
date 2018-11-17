using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGraphics1", menuName = "Player/Player Graphics" )]
public class PlayerGraphicsSO : ScriptableObject {
    
    [Header("Head")]
    public Sprite Head;
    [Header("Neck")]
    public Sprite Neck;
    [Header("Torso")]
    public Sprite Torso;

    [Header("Right Arm")]
    public Sprite RUArm;
    public Sprite RLArm;
    public Sprite RHand;

    [Header("Left Arm")]
    public Sprite LUArm;
    public Sprite LLArm;
    public Sprite LHand;

    [Header("Right Leg")]
    public Sprite RULeg;
    public Sprite RLLeg;
    public Sprite RFoot;

    [Header("Left Leg")]
    public Sprite LULeg;
    public Sprite LLLeg;
    public Sprite LFoot;
}
