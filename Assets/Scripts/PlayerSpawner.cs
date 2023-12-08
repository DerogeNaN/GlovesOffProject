using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    GameObject[] players = new GameObject[2];
    private PlayerInput[] playerInputs;
    public GameObject[] _players { get { return players; } }
    public GameObject[] playerPrefabs = new GameObject[2];
    [SerializeField] Transform[] spawnPoints = new Transform[2];

    public GameManager gameManager;
    [SerializeField] AnimationHandler animationHandler;
    Canvas inGameOverlay;
    PlayerInputManager curPlayerInputManager;

    public bool[] playersReady = {false, false};

    RoundUI roundUI;
    int playerNumber = 0;
    CinemachineTargetGroup targetGroup;

    Singleplayer singleplayer;

    void Start()
    {
        inGameOverlay = GameObject.FindObjectOfType<Canvas>();
        curPlayerInputManager = GetComponent<PlayerInputManager>();
        animationHandler = GetComponent<AnimationHandler>();
        curPlayerInputManager.onPlayerJoined += OnPlayerJoined;
        roundUI = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RoundUI>();
        targetGroup = GameObject.Find("TargetGroup1").GetComponent<CinemachineTargetGroup>();
        singleplayer = GameObject.FindGameObjectWithTag("Singleplayer").GetComponent<Singleplayer>();
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        if (players[0] != null && players[1] != null)
        {
            Destroy(playerInput.gameObject);
            return;
        }
        playerNumber += 1;
        roundUI.OnJoin(playerNumber);
        playerInput.GetComponent<PlayerController>().controlSchemeKeyboard = playerInput.GetDevice<Keyboard>() != null;
        curPlayerInputManager.playerPrefab = playerPrefabs[playerInput.playerIndex];
        players[playerInput.playerIndex] = playerInput.gameObject;

        playerInput.transform.position = spawnPoints[playerInput.playerIndex].position;
        playerInput.transform.rotation = spawnPoints[playerInput.playerIndex].rotation;
        playerInput.name = "Player " + (playerInput.playerIndex + 1).ToString();

        if (singleplayer.singleplayer == true)
        {
            OnBotJoined();
        }
    }

    void OnBotJoined()
    {
        players[1] = Instantiate(playerPrefabs[1]);
        roundUI.OnJoin(2);
        Debug.Log(1);
        curPlayerInputManager.playerPrefab = playerPrefabs[1];

        players[1].transform.position = spawnPoints[1].position;
        players[1].transform.rotation = spawnPoints[1].rotation;
        players[1].name = "Player " + (1 + 1).ToString();
        ReadyUp(1);

        players[1].GetComponent<PlayerInput>().DeactivateInput();
    }

    public void ReadyUp(int playerIndex)
    {
        playersReady[playerIndex] = true;
        Debug.Log(playerIndex + " is ready!");
        roundUI.OnReady(playerIndex);

        if (playersReady[0] && playersReady[1]) 
        {
            gameManager.EnableGameManager();
            inGameOverlay.gameObject.SetActive(true);
            targetGroup.m_Targets[0].target = players[0].transform.Find("JNT_Root");
            targetGroup.m_Targets[1].target = players[1].transform.Find("JNT_Root");
        }
    }
}
