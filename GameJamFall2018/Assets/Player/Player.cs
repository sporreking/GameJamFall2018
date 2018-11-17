using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public PlayerInputSO PlayerInput;
    public int PlayerInputIndex;

    public float Speed;

    public GameObject LeftHand;
    public GameObject RightHand;

    public float HandRadius;

    public float MoveBorder;

    public GameObject GroundCheck;

    public void Start() {
        Debug.Log("Creating player: "+PlayerInputIndex);

        // Load values from player input SO
        LeftHand.GetComponent<Hand>().InputX = PlayerInput.InputLeftX[PlayerInputIndex];
        LeftHand.GetComponent<Hand>().InputY = PlayerInput.InputLeftY[PlayerInputIndex];
        RightHand.GetComponent<Hand>().InputX = PlayerInput.InputRightX[PlayerInputIndex];
        RightHand.GetComponent<Hand>().InputY = PlayerInput.InputRightY[PlayerInputIndex];

    }

    public void FixedUpdate() {

        float move = 0;

        move += Speed * LeftHand.GetComponent<Hand>().Move() * Time.fixedDeltaTime;
        move += Speed * RightHand.GetComponent<Hand>().Move() * Time.fixedDeltaTime;

        transform.Translate(new Vector2(Mathf.Clamp(move, - Speed * Time.fixedDeltaTime, Speed * Time.fixedDeltaTime), 0));
    }

}
