using System;
using UnityEngine;
using Photon.Pun; 

public class HitTrigger : MonoBehaviourPunCallbacks
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (photonView.IsMine && other.gameObject.GetComponent<SomethingCanHitPlayer>()?.CanHit == true)
        {
            photonView.RPC("AddOrMinusHp" , RpcTarget.All , -1.0f);
        }
    }
}
