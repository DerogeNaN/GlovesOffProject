using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    private PlayerInput[] players = new PlayerInput[2];
    public PlayerInput[] _players { get { return players; } }
    [SerializeField] Transform[] spawnPoints = new Transform[2];

    public GameManager gameManager;

    void Start()
    {
        PlayerInputManager curPlayerInputManager = GetComponent<PlayerInputManager>();
        curPlayerInputManager.onPlayerJoined += OnPlayerJoined;
    }

    
    void Update()
    {
        
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log(playerInput.playerIndex);
        players[playerInput.playerIndex] = playerInput;

        playerInput.transform.position = spawnPoints[playerInput.playerIndex].position;
        playerInput.transform.rotation = spawnPoints[playerInput.playerIndex].rotation;
        playerInput.name = "Player " + (playerInput.playerIndex + 1).ToString();

        if (players[1] != null)
        {
            gameManager.EnableGameManager();
        }
    }

}
