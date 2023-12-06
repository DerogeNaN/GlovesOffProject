using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    private PlayerInput[] players = new PlayerInput[2];
    public PlayerInput[] _players { get { return players; } }
    public GameObject[] playerPrefabs = new GameObject[2];
    [SerializeField] Transform[] spawnPoints = new Transform[2];

    public GameManager gameManager;
    [SerializeField] AnimationHandler animationHandler;
    Canvas inGameOverlay;
    PlayerInputManager curPlayerInputManager;

    public bool[] playersReady = {false, false};

    RoundUI roundUI;
    int playerNumber = 0;

    void Start()
    {
        inGameOverlay = GameObject.FindObjectOfType<Canvas>();
        curPlayerInputManager = GetComponent<PlayerInputManager>();
        animationHandler = GetComponent<AnimationHandler>();
        curPlayerInputManager.onPlayerJoined += OnPlayerJoined;
        roundUI = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RoundUI>();
    }

    void Update()
    {
        
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        playerNumber += 1;
        roundUI.OnJoin(playerInput, playerNumber);
        playerInput.GetComponent<PlayerController>().controlSchemeKeyboard = playerInput.GetDevice<Keyboard>() != null;
        Debug.Log(playerInput.playerIndex);
        curPlayerInputManager.playerPrefab = playerPrefabs[playerInput.playerIndex];
        players[playerInput.playerIndex] = playerInput;

        playerInput.transform.position = spawnPoints[playerInput.playerIndex].position;
        playerInput.transform.rotation = spawnPoints[playerInput.playerIndex].rotation;
        playerInput.name = "Player " + (playerInput.playerIndex + 1).ToString();

        //if (players[playerInput.playerIndex] != null)
        //{
        //    players[playerInput.playerIndex].actions.FindAction("HatSelection").Enable();
        //}

        
    }

    public void ReadyUp(PlayerInput playerInput)
    {
        playersReady[playerInput.playerIndex] = true;
        Debug.Log(playerInput.playerIndex + " is ready!");
        roundUI.OnReady(playerInput.playerIndex);

        if (playersReady[0] && playersReady[1])
        {
            gameManager.EnableGameManager();
            inGameOverlay.gameObject.SetActive(true);

        }
    }
}
