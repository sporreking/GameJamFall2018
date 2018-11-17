using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject PlayerPrefab;
    public GameObject WeaponPrefab;

    public float WeaponSpawnInterval;
    private float LastWeaponSpawn;

    private int NumberOfPlayers;
    private List<GameObject> Players;
    private List<GameObject> Weapons;

    private List<string> JoystickNames;

    // Use this for initialization
    void Start () {

        LastWeaponSpawn = Time.time;


        JoystickNames = new List<string>();
        Weapons = new List<GameObject>();

        string[] tempJoystickNames = Input.GetJoystickNames();
        foreach (string joyName in tempJoystickNames)
        {
            if (joyName != "")
            {
                JoystickNames.Add(joyName);
            }
        }
        
        Debug.Log("Connected controllers: "+JoystickNames.Count);
        
        NumberOfPlayers = JoystickNames.Count;
        if (NumberOfPlayers == 0)
        {
            NumberOfPlayers = 1; // Debug player for when no controllers connected
        }


        // Instatiate players

        Players = new List<GameObject>();
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            Transform spawn = GameObject.Find("PlayerSpawnpoints").transform.GetChild(i);
            GameObject playerObj = Instantiate(PlayerPrefab, spawn);
            playerObj.GetComponent<Player>().PlayerGraphics = spawn.GetComponent<SpawnPoint>().Texture;

            playerObj.GetComponent<Player>().PlayerInputIndex = i;

            Players.Add(playerObj);
    
        }

    }
	
	// Update is called once per frame
	void Update () {
        

        // Weapon spawning
		if (LastWeaponSpawn + WeaponSpawnInterval < Time.time)
        {
            
            LastWeaponSpawn = Time.time;
            int index = Random.Range(0, GameObject.Find("WeaponSpawnpoints").transform.childCount);
            Transform spawn = GameObject.Find("WeaponSpawnpoints").transform.GetChild(index);

            Weapons.Add(Instantiate(WeaponPrefab, spawn));

            Debug.Log("Spawn weapon at point: " + spawn.name + " (index " + index + ")");
        }
    }
}
