using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRnak : MonoBehaviourPunCallbacks
{
    private NetworkManagerInGame _networkManagerInGame;
    private bool isMoving = false;
    private List<GameObject> player = new List<GameObject>();
    private Dictionary<int, Transform> RnakPlace = new Dictionary<int, Transform>(); 

    private void Awake()
    {
        _networkManagerInGame = GameObject.Find("NetworkManager2").GetComponent<NetworkManagerInGame>();
        player = _networkManagerInGame.players;

        int a = 0; 
        foreach (var THING in player)
        {
            
        }
    }

    private void Update()
    {
        MovePlayer();
        Shorting();
    }

    private void MovePlayer()
    {
        
    }

    private void Shorting()
    {
        
    }
}
