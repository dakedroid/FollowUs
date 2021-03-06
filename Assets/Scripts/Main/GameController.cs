﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Map mapController;
    public Vector2Int currentActiveRoom = new Vector2Int(0, 0);

    [SerializeField]
    private TextMeshProUGUI gameOverText = null;

    public int enemiesCount = 0;
    public bool runningOnPC;

    [SerializeField]
    private GameObject playerPrefab = null;

    private Room startRoom;

    [SerializeField]
    private Button startButton = null;

    [SerializeField]
    private GameObject[] collectables;

    [SerializeField]
    private PlayerController playerController = null; 

    private static GameController instance = null;

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            checkRunningPlatform();
            Application.targetFrameRate = 60;
        }
        else Destroy(this);


        //INIT GAME
        //GameUIManager.Instance.setState(GameUIStates.GAME_HAS_STARTED);
        setGame();
        Room start = mapController.map[mapController.startRoom.x, mapController.startRoom.y];
        playerController.transform.position = start.transform.position;
        /*if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                startButton.gameObject.SetActive(true);
                setGame();
                PhotonNetwork.Instantiate(playerPrefab.name, start.transform.position, Quaternion.identity);
            }
            else
            {
                PlayerComponents.Instance.mainCamera.gameObject.SetActive(true);
                PlayerComponents.Instance.mainCamera.transform.position = new Vector2(-10, 10);
                Invoke("instantiatePlayer", 1f);
            }
        }*/
    }

    /*private void instantiatePlayer()
    {
        Room start = mapController.map[mapController.startRoom.x, mapController.startRoom.y];
        start.isRoomActive = true;
        start.gameObject.SetActive(true);
        currentActiveRoom = start.mapLocation;
        PlayerComponents.Instance.mainCamera.gameObject.SetActive(false);

        Vector2 randStartPos = start.transform.position;
        randStartPos.x = (int)Random.Range(randStartPos.x - 1, randStartPos.x + 1);
        randStartPos.y = (int)Random.Range(randStartPos.y - 1, randStartPos.y - 1);

        PhotonNetwork.Instantiate(playerPrefab.name, randStartPos, Quaternion.identity);
    }*/

    private void setGame()
    {
        mapController.initMap();
        startRoom = mapController.map[mapController.startRoom.x, mapController.startRoom.y];
        startRoom.isRoomActive = true;
        startRoom.gameObject.SetActive(true);
        currentActiveRoom = mapController.startRoom;
        enemiesCount = 0;
        initUI();
    }

    public void initGame()
    {
        /*int playersCount = PhotonNetwork.CurrentRoom.PlayerCount;
        int badLeaderIndex = Random.Range(0, playersCount);
        int cont = 0;
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (cont == badLeaderIndex)
            {
                hash.Add(PlayerType.key, PlayerType.BadLeader);
            }
            else
            {
                hash.Add(PlayerType.key, PlayerType.Normal);
            }
            player.SetCustomProperties(hash);
            hash.Clear();
            cont++;
        }
        startRoom.GetComponent<PhotonView>().RPC("startMainRoom", RpcTarget.All);*/
        startButton.gameObject.SetActive(false);
    }


    private void checkRunningPlatform()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
            runningOnPC = true;
        #elif UNITY_IOS || UNITY_ANDROID
            runningOnPC = false;
        #endif
    }

    private void initUI()
    {
        gameOverText.gameObject.SetActive(false);
    }

    public void openDoor(int x, int y, int direction)
    {
        mapController.desactivateDoor(x, y, direction, currentActiveRoom);
        currentActiveRoom.x += x;
        currentActiveRoom.y += y;
    }

    public void setGameOverText(bool active, string text)
    {
        gameOverText.gameObject.SetActive(active);
        gameOverText.text = text;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(1);
        initGame();
    }

    public void updateCurrentActiveRoom(int x, int y)
    {
        currentActiveRoom.x += x;
        currentActiveRoom.y += y;
    }

    /*
    #region Pun Callbacks

    public override void OnJoinedRoom()
    {
        int mapSize = int.Parse(PhotonNetwork.CurrentRoom.CustomProperties[RoomProperty.MapSize].ToString());
        Util.mapSize = mapSize;
    }

    #endregion
    */
}
