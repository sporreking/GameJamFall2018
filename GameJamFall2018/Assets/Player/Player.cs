﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public PlayerInputSO PlayerInput;
    public int PlayerInputIndex;

    public int Health;
    public int Deaths;

    private GUIStyle guiStyle;

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

    public void Awake() {
        Deaths = 0;       
    }

    public void Start()
    {
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

        guiStyle = new GUIStyle();
        guiStyle.normal.textColor = Color.red;
        guiStyle.fontSize = 22;
    }

    public void FixedUpdate() {

        float move = 0;

        move += Speed * LeftHand.GetComponent<Hand>().Move() * Time.fixedDeltaTime;
        move += Speed * RightHand.GetComponent<Hand>().Move() * Time.fixedDeltaTime;

        transform.Translate(new Vector2(Mathf.Clamp(move, - Speed * Time.fixedDeltaTime, Speed * Time.fixedDeltaTime), 0));
    }

    public void Kill()
    {
        Health = 0;
    }

    public void ChangeHealth(int x)
    {
        Health += x;
    }

    public void Update() {
        
        if (Input.GetButtonDown(PlayerInput.InputJump[PlayerInputIndex]) && this.GroundCheck.GetComponent<GroundCheck>().Jumps > 0) {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpPower));
            this.GroundCheck.GetComponent<GroundCheck>().Jumps--;
            Debug.Log(PlayerInput.InputJump[PlayerInputIndex], this);
            Debug.Log(PlayerInputIndex, this);
            Debug.Log(Input.GetButtonDown(PlayerInput.InputJump[PlayerInputIndex]), this);
        }

        if (Input.GetButtonDown(PlayerInput.InputUseLeftWeapon[PlayerInputIndex]))
        {
            RightHand.GetComponent<Hand>().StartUse();
        };


        if (Input.GetButtonDown(PlayerInput.InputUseRightWeapon[PlayerInputIndex]))
        {
            LeftHand.GetComponent<Hand>().StartUse();

        };

        if (Input.GetButtonUp(PlayerInput.InputUseLeftWeapon[PlayerInputIndex]))
        {
            RightHand.GetComponent<Hand>().StopUse();
        };


        if (Input.GetButtonUp(PlayerInput.InputUseRightWeapon[PlayerInputIndex]))
        {
            LeftHand.GetComponent<Hand>().StopUse();

        };

        if (Input.GetButtonDown(PlayerInput.InputDropLeftWeapon[PlayerInputIndex]))
        {
            RightHand.GetComponent<Hand>().Drop();
        };

        if (Input.GetButtonDown(PlayerInput.InputDropRightWeapon[PlayerInputIndex]))
        {
            LeftHand.GetComponent<Hand>().Drop();
        };
        
    }

    public void OnGUI()
    {
        Transform t = this.transform.Find("HealthBar").transform;
        Vector3 d = Camera.main.WorldToScreenPoint(t.position);
        d.y = Screen.height - d.y;
        Rect r = new Rect(d, new Vector2(0, 0));
        GUI.Label(r, Health+"%", guiStyle);
    }

}
