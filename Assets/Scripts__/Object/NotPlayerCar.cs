using System;
using UnityEngine;
using Photon.Pun; 

public class NotPlayerCar : MonoBehaviourPunCallbacks
{
    private Explosion _explosion;
    
    [Header("When Explosin")]
    public Sprite WhenYouWantExplosin;
    public SpriteRenderer SpriteRenderer; 
    
    
    private void Awake()
    {
        _explosion = GetComponent<Explosion>(); 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            photonView.RPC("Explode" , RpcTarget.All);
        }
    }
}
