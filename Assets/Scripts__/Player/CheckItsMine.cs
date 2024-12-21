using System;
using UnityEngine;
using Photon.Pun; 

public class CheckItsMine : MonoBehaviourPunCallbacks
{
    private void Awake() { if (photonView.IsMine) { gameObject.tag = "Player"; } }
}
