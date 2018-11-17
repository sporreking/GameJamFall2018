using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject PlayerPrefab;

    private int NumberOfPlayers;
    private List<GameObject> Players;

    private string[] JoystickNames;

    // Use this for initialization
    void Start () {

        JoystickNames = Input.GetJoystickNames();
        
        NumberOfPlayers = JoystickNames.Length;

        // Instatiate players

        Players = new List<GameObject>();
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            GameObject playerObj = Instantiate(PlayerPrefab);

            playerObj.GetComponent<Player>().PlayerInputIndex = i;

            Players.Add(playerObj);
    
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
