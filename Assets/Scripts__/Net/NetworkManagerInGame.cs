using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;


public class NetworkManagerInGame : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI ReadyText;
    private int readyNum; 
    private bool isClick = false;
    public bool AllReady = false; 
    private InputSystem_Actions inputActions;
    public List<GameObject> players = new List<GameObject>(); 
    
    private void Awake()
    {
        // Input Actions 인스턴스 생성
        inputActions = new InputSystem_Actions();
    }

    private void Update()
    {
        if (inputActions.Player.Button.triggered && !isClick && !PhotonNetwork.IsMasterClient)
        {
            isClick = true; 
            photonView.RPC("AddReadyNum" , RpcTarget.All);
        }
    }

    private void OnEnable()
    {
        // Input Action 활성화
        inputActions.Player.Button.Enable();
    }

    private void OnDisable()
    {
        // Input Action 비활성화
        inputActions.Player.Button.Disable();
    }

    [PunRPC]
    public void AddReadyNum()
    {
        readyNum++;
        ReadyText.text = $"{readyNum} / 4";

        if (PhotonNetwork.IsMasterClient && readyNum == 1)
        {
            photonView.RPC("AllReadyM", RpcTarget.All);
        }
    }

    [PunRPC]
    public void AllReadyM()
    {
        GetComponent<StartProcess>().Process();
    }
}
