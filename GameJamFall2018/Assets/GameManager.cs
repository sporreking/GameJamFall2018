using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject PlayerPrefab;

    private string[] JoystickNames;

    // Use this for initialization
    void Start () {
        JoystickNames = Input.GetJoystickNames();


        for (int i = 0; i < JoystickNames.Length; i++)
        {
            GameObject playerObj = Instantiate(PlayerPrefab);
            GameObject leftHand = playerObj.transform.Find("LeftHand").gameObject;
            GameObject rightHand = playerObj.transform.Find("RightHand").gameObject;


            playerObj.GetComponent<Player>().LeftHand = leftHand;
            playerObj.GetComponent<Player>().RightHand = rightHand;
            playerObj.GetComponent<Player>().PlayerInputIndex = i;

            leftHand.GetComponent<Hand>().Parent = playerObj.GetComponent<Player>();
            rightHand.GetComponent<Hand>().Parent = playerObj.GetComponent<Player>();


        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
