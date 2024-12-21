using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using Photon.Realtime; 

public class EndPoint : MonoBehaviourPunCallbacks
{
    private InputSystem_Actions inputActions;

    [Header("Players")]
    [SerializeField] private List<string> players = new List<string>();

    public int myPlace;

    [Header("UI")]
    [SerializeField] private Transform _EndCredit;
    [SerializeField] private NetworkManagerInGame _networkManagerInGame;

    private bool isEnd = false;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            photonView.RPC("End", RpcTarget.Others, other.gameObject.GetComponent<PhotonView>().name);
            _EndCredit.gameObject.SetActive(true);
            _networkManagerInGame.AllReady = false;
            players.Add(other.gameObject.GetComponent<PhotonView>().name);
            myPlace = players.Count;
            isEnd = true;
            
            Invoke("EndUp" , 11.0f);
        }
    }

    [PunRPC]
    private void End(string go)
    {
        players.Add(go);
    }

    private void OnEnable()
    {
        inputActions.Player.Button.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Button.Disable();
    }

    private void Update()
    {
        if (inputActions.Player.Button.triggered && isEnd && photonView.IsMine)
        {
            EndUp();
        }
    }

    private void EndUp()
    { 
        Debug.Log("Disconnecting from Photon.");
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);  
    }

    public void OnPlayerLeftRoom(Player player)
    {
       
        // 첫 번째 씬 (혹은 원하는 씬 번호)로 돌아감
    }
}