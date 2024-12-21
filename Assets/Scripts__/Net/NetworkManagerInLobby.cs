using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;
using TMPro; 
using UnityEngine.SceneManagement;


public class NetworkManagerInLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private string gameVersion = "1";
    [SerializeField] private byte maxPlayersPerRoom = 4;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private string roomName;
    public GameObject roomListItemPrefab; // 방 목록 아이템 프리팹
    public Transform roomListContent; // 방 목록 컨텐트 영역
    public TMP_InputField roomNameInputField;
    private bool isSceneLoaded = false;

    //public GameObject[] somethingToHide;
    //public GameObject[] somethingToActivate;
    
    public void Awake()
    {
        ConnectToPhoton();
    }

    public void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby(); // 로비에 참여
        }
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        
        // 로비에 참여 후 방 목록 가져오기
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // 방 목록 업데이트
        Debug.Log("Room list updated");

        // 방 목록 UI 업데이트
        UpdateRoomListUI(roomList);
    }

    private void UpdateRoomListUI(List<RoomInfo> roomList)
    {
        // 기존 방 목록 아이템 제거
        foreach (Transform child in roomListContent)
        {
            Destroy(child.gameObject);
        }

        // 새로운 방 목록 아이템 생성
        foreach (var roomInfo in roomList)
        {
            GameObject roomItem = Instantiate(roomListItemPrefab, roomListContent);
            roomItem.transform.SetParent(roomListContent, false);

        
            int playerCount = roomInfo.PlayerCount;
            int maxPlayers = roomInfo.MaxPlayers;
            string hostName = (string)roomInfo.CustomProperties["HostName"];

            roomItem.GetComponentInChildren<TextMeshProUGUI>().text = $"Romm Name: {roomInfo.Name}  - -  {playerCount}/{maxPlayers}  - -  Host: {hostName}  - -  Join";

            roomItem.GetComponentInChildren<Button>().onClick.AddListener(() => JoinSelectedRoom(roomInfo.Name));
        }
    }
    
    public void JoinSelectedRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    
    public void OnCreateRoomButtonClick()
    {
        if (PhotonNetwork.IsConnected)
        {
            RoomOptions roomOptions = new RoomOptions
            {
                MaxPlayers = maxPlayersPerRoom,
                IsOpen = true,
                IsVisible = true,
                BroadcastPropsChangeToAll = false, 
                CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
                {
                    { "HostName", PhotonNetwork.NickName }
                    
                },
                CustomRoomPropertiesForLobby = new string[] { "HostName" }
            };

            roomName = roomNameInputField.text;
        
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
    }
    
    [SerializeField] private GameObject _car;
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Flag1");
        if (PhotonNetwork.IsMasterClient)
        {
            //AssignTeam(newPlayer);
        }
        
        if (photonView.IsMine)
        {
            //PhotonNetwork.Instantiate(_car.name , Vector3.zero , Quaternion.identity);
            //GetComponent<PhotonView>().RPC("GiveHimMessage" , RpcTarget.All , PhotonNetwork.NickName + " Joined the room.");
        }
    }
    



    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel(2);
        Debug.Log("Flag1");
        if (PhotonNetwork.IsMasterClient)
        {
            //AssignTeam(PhotonNetwork.LocalPlayer);
        }
        else
        {
            //photonView.RPC("ToMasterClient" , RpcTarget.MasterClient , PhotonNetwork.LocalPlayer);
        }
    }

    [PunRPC]
    private void ToMasterClient(Player player)
    {
        Debug.Log("flag0");
        //AssignTeam(player); 
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PhotonNetwork.InRoom && !isSceneLoaded)
        {
            isSceneLoaded = true;
            if(!PhotonNetwork.IsMasterClient)
                SpawnThing();
        }
    }

    private void SpawnPlayer()
    {
        // 팀 태그 설정
        //string teamTag = (PhotonNetwork.CurrentRoom.PlayerCount % 2 == 0) ? "Team1" : "Team2";

        //Debug.Log(teamTag);
        
            Debug.Log("do something now ");
            Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
            GameObject playerObj = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
            if (playerObj.GetComponent<PhotonView>().IsMine)
            {
                playerObj.name = "Player1"; 
            }
        
        
        // 플레이어 스폰

        // TeamTag 컴포넌트를 플레이어 오브젝트에 추가하고 팀 태그 설정
        //TeamTag teamTagComponent = playerObj.GetComponent<TeamTag>();
        //if (teamTagComponent != null)
        //{
            //Debug.Log("It is not null");
            //teamTagComponent.SetTeam(teamTag);
        //}
    }

    public void SpawnThing()
    {
        //GetComponent<PhotonView>().RPC("GiveHimMessage" , RpcTarget.All , PhotonNetwork.NickName + " Joined the room.");
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0,0,0), Quaternion.identity);
        PhotonNetwork.Instantiate(_car.name , Vector3.zero , Quaternion.identity);
    }
    
    [PunRPC]
    void GiveHimMessage(string message)
    {
        Debug.Log(message);
    }
    
    [PunRPC]
    private void NotifyTeamAssignment(int playerID, string team)
    {
        Debug.Log($"NotifyTeamAssignment called for player {playerID} to {team}");
        Player player = PhotonNetwork.CurrentRoom.GetPlayer(playerID);
        if (player != null)
        {
            Debug.Log(player.NickName);
            //string team = DetermineTeam();
            ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable { { "Team", team } };
            player.SetCustomProperties(propertiesToSet); 
        }
    }

    [PunRPC]
    private void ConfirmTeamAssignment(string assignedTeam)
    {
        //myTeam = (string)(PhotonNetwork.LocalPlayer.CustomProperties["Team"]);
        //Debug.Log(myTeam);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to join room: " + message);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning("Disconnected from Photon: " + cause.ToString());
    }
}
