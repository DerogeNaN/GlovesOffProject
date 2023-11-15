using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    private PlayerInput[] players = new PlayerInput[2];
    public PlayerInput[] _players { get { return players; } }
    [SerializeField] Transform[] spawnPoints = new Transform[2];
    public GameObject[] playerPrefabs = new GameObject[2];

    public GameManager gameManager;

    PlayerInputManager curPlayerInputManager;
    void Start()
    {
        curPlayerInputManager = GetComponent<PlayerInputManager>();
        curPlayerInputManager.onPlayerJoined += OnPlayerJoined;
    }

    
    void Update()
    {
        
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        curPlayerInputManager.playerPrefab = playerPrefabs[1];
        Debug.Log(playerInput.playerIndex);
        players[playerInput.playerIndex] = playerInput;

        playerInput.transform.position = spawnPoints[playerInput.playerIndex].position;
        //players[playerInput.playerIndex].gameObject = playerPrefabs[playerInput.playerIndex];
        playerInput.name = "Player " + (playerInput.playerIndex + 1).ToString();

        if (players[1] != null)
        {
            gameManager.EnableGameManager();
        }
    }

}
