using UnityEngine;

public class AddHp : MonoBehaviour
{
    private PlayerHp _playerHp; 
    public float Amount = 0;
    public bool Destroiable = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHp>().FromNoneNetwork(Amount);
            
            if (Destroiable)
            {
                Destroy(gameObject);
            }
        }
    }
}
