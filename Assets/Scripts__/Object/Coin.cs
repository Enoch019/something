using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource coinSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //int a = Random.Range(-10 , 10);
            //coinSound.pitch += a;
            coinSound.Play();
            other.gameObject.GetComponent<CoinCounter>().AddCoin();
            other.gameObject.GetComponent<DistanceTracker>().AddScore(100.0f);
            //Destroy(gameObject);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
            Invoke("Delete" , 1.0f);
        }
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
