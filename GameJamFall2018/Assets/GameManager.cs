﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject PlayerPrefab;
    public GameObject WeaponPrefab;

    public Camera MainCamera;
    private Vector3 CameraSpeed;
    private Vector3 CameraAcceleration;
    private Vector3 CameraMins;
    private Vector3 CameraMaxes;

    public float WeaponSpawnInterval;
    private float LastWeaponSpawn;

    private int NumberOfPlayers;
    private int NumberOfAlivePlayers;
    private List<GameObject> Players;
    private List<GameObject> Weapons;

    private List<string> JoystickNames;

    // Use this for initialization
    void Awake () {

        CameraSpeed = new Vector3(0, 0, 0);
        CameraAcceleration = new Vector3(0, 0, 0);

        LastWeaponSpawn = Time.time;
        Weapons = new List<GameObject>();

        // Player handling

        JoystickNames = new List<string>();
        

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
        NumberOfAlivePlayers = JoystickNames.Count;

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
        Vector2 averagePostition = new Vector2(0, 0);
        Vector2 maxPos = new Vector2(0, 0);
        Vector2 minPos = new Vector2(0, 0);

        // Player loop
        foreach (GameObject playerObj in Players)
        {
            Player p = playerObj.GetComponent<Player>();
            Vector2 playerPosition = playerObj.transform.position;
            averagePostition += playerPosition / NumberOfPlayers;
            maxPos = Vector2.Max(maxPos, (Vector2) playerObj.transform.position);
            minPos = Vector2.Min(minPos, (Vector2) playerObj.transform.position);

            if (p.Health <= 0)
            {
                NumberOfAlivePlayers -= 1;
                Debug.Log("PLAYER "+p.PlayerInputIndex+" DIED. YEEEAAHH!");
            }


        }

        // Position camera
        Vector3 camPos = MainCamera.transform.position + 10F * CameraSpeed * Time.deltaTime;
        MainCamera.transform.position = new Vector3(camPos.x, camPos.y, -10f);
        MainCamera.orthographicSize += CameraSpeed.z * Time.deltaTime;

        Vector2 posAcc =  averagePostition - (Vector2) camPos;
        float diff = ((maxPos - minPos).magnitude*0.3F + 2F - MainCamera.orthographicSize) *5;

        CameraAcceleration = new Vector3(posAcc.x, posAcc.y, diff);

        CameraSpeed += CameraAcceleration * Time.deltaTime * 0.2F;
        CameraSpeed *= Mathf.Pow(0.0075F, Time.deltaTime);

        // Weapon spawning
        if (LastWeaponSpawn + WeaponSpawnInterval < Time.time)
        {
            LastWeaponSpawn = Time.time;

            if (GameObject.Find("WeaponSpawnpoints").transform.childCount != 0)
            {
                int index = Random.Range(0, GameObject.Find("WeaponSpawnpoints").transform.childCount);
                Debug.Log("Spawn weapon at point: " + index);
                Transform spawn = GameObject.Find("WeaponSpawnpoints").transform.GetChild(index);
                Weapons.Add(Instantiate(WeaponPrefab, spawn));
            }        
        }
    }
}
