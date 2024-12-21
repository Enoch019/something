using System;
using Unity.Cinemachine;
using UnityEngine;
using Photon.Pun; 

public class CamCode : MonoBehaviourPunCallbacks
{
    private bool isFound = false;

    public CinemachineCamera _cinemachineCamera;
    public NetworkManagerInGame networkManagerInGame; 

    private void Awake()
    {
        _cinemachineCamera = GetComponent<CinemachineCamera>();
        networkManagerInGame = GameObject.Find("NetworkManager2").GetComponent<NetworkManagerInGame>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFound && !PhotonNetwork.IsMasterClient)
        {
            Transform transform = GameObject.FindGameObjectWithTag("Player").transform;
            if (transform != null)
            {
                _cinemachineCamera.Follow = transform; 
                isFound = true; 
            }
        }
        else if(PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //_cinemachineCamera.Follow = networkManagerInGame.players[]; 
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                
            }
        }
    }
}
