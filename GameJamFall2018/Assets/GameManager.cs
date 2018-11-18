using System.Collections;
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
    private GameObject[] Players;
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
        //NumberOfPlayers = 3;
        NumberOfAlivePlayers = NumberOfPlayers;

        if (NumberOfPlayers == 0)
        {
            NumberOfPlayers = 1; // Debug player for when no controllers connected
        }

        // Instatiate players

        Players = new GameObject[NumberOfPlayers];
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            SpawnPlayer(i);    
        }

    }
	
	// Update is called once per frame
	void Update () {
        Vector2 averagePostition = new Vector2(0, 0);
        Vector2 maxPos = new Vector2(0, 0);
        Vector2 minPos = new Vector2(0, 0);

        // Player loop
        for (int i = 0; i<Players.Length; i++)
        {
            GameObject playerObj = Players[i];
            Player p = playerObj.GetComponent<Player>();
            Vector2 playerPosition = playerObj.transform.position;
            averagePostition += playerPosition / NumberOfPlayers;
            maxPos = Vector2.Max(maxPos, (Vector2) playerObj.transform.position);
            minPos = Vector2.Min(minPos, (Vector2) playerObj.transform.position);

            if (p.Health <= 0)
            {
                NumberOfAlivePlayers -= 1;
                int id = p.PlayerInputIndex;

                int deaths = Players[i].GetComponent<Player>().Deaths + 1; // This doesn't and I have no idea why.

                Destroy(playerObj); // Don't use p from here
                Players[i] = SpawnPlayer(id);
                Players[i].GetComponent<Player>().Deaths = deaths;

                Debug.Log("PLAYER " + id + " EXPERIENCED DEATH NUMBER "+ deaths + ". YEEEAAHH!");
            }


        }

        // Position camera
        Vector3 camPos = MainCamera.transform.position + 10F * CameraSpeed * Time.deltaTime;
        MainCamera.transform.position = new Vector3(camPos.x, camPos.y, -10f);
        MainCamera.orthographicSize += CameraSpeed.z * Time.deltaTime;

        Vector2 posAcc =  averagePostition - (Vector2) camPos;
        float diff = ((maxPos - minPos).magnitude*0.5F + 2F - MainCamera.orthographicSize) *15;

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

    private GameObject SpawnPlayer(int id)
    {
        Transform spawn = GameObject.Find("PlayerSpawnpoints").transform.GetChild(id % GameObject.Find("PlayerSpawnpoints").transform.childCount);
        GameObject playerObj = Instantiate(PlayerPrefab, spawn);
        playerObj.GetComponent<Player>().PlayerGraphics = spawn.GetComponent<SpawnPoint>().Texture;

        playerObj.GetComponent<Player>().PlayerInputIndex = id;

        Players[id] = playerObj;
        return playerObj;

    }
}
