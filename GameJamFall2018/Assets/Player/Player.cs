﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public PlayerInputSO PlayerInput;
    public int PlayerInputIndex;

    public PlayerGraphicsSO PlayerGraphics;

    public float Speed;

    public GameObject LeftHand;
    public GameObject RightHand;

    public float HandRadius;

    public float MoveBorder;

    public GameObject GroundCheck;
    public float JumpPower;
    
    public GameObject Head;

    public GameObject Neck;

    public GameObject Torso;
    
    public GameObject RUArm;
    public GameObject RLArm;
    public GameObject RHand;
    
    public GameObject LUArm;
    public GameObject LLArm;
    public GameObject LHand;
    
    public GameObject RULeg;
    public GameObject RLLeg;
    public GameObject RFoot;

    public GameObject LULeg;
    public GameObject LLLeg;
    public GameObject LFoot;

    public void Start() {
        Debug.Log("Creating player: " + PlayerInputIndex);

        // Load values from player input SO
        LeftHand.GetComponent<Hand>().InputX = PlayerInput.InputLeftX[PlayerInputIndex];
        LeftHand.GetComponent<Hand>().InputY = PlayerInput.InputLeftY[PlayerInputIndex];
        RightHand.GetComponent<Hand>().InputX = PlayerInput.InputRightX[PlayerInputIndex];
        RightHand.GetComponent<Hand>().InputY = PlayerInput.InputRightY[PlayerInputIndex];

        // Load graphics from player graphics SO
        Head.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.Head;
        Neck.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.Neck;
        Torso.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.Torso;
        RUArm.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.RUArm;
        RLArm.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.RLArm;
        RHand.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.RHand;
        LUArm.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.LUArm;
        LLArm.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.LLArm;
        LHand.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.LHand;
        RULeg.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.RULeg;
        RLLeg.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.RLLeg;
        RFoot.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.RFoot;
        LULeg.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.LULeg;
        LLLeg.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.LLLeg;
        LFoot.GetComponent<SpriteRenderer>().sprite = PlayerGraphics.LFoot;
        
    }

    public void FixedUpdate() {

        float move = 0;

        move += Speed * LeftHand.GetComponent<Hand>().Move() * Time.fixedDeltaTime;
        move += Speed * RightHand.GetComponent<Hand>().Move() * Time.fixedDeltaTime;

        transform.Translate(new Vector2(Mathf.Clamp(move, - Speed * Time.fixedDeltaTime, Speed * Time.fixedDeltaTime), 0));
    }

    public void Update() {
        if (Input.GetButtonDown(PlayerInput.InputJump[PlayerInputIndex]) && GroundCheck.GetComponent<GroundCheck>().Jumps > 0) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpPower));
            GroundCheck.GetComponent<GroundCheck>().Jumps--;
        }
    }

}