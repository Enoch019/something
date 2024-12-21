using System;
using UnityEngine;

public class AddSpeed : MonoBehaviour
{
    //private PlayerHp _playerHp; 
    private CarController _carController;
    public float Amount = 0;
    public float Time = 0;
    public bool Destroiable = false;
    public bool SpeedUpPoint = false; 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>(); 
            _carController.AddPower(Time , Amount , SpeedUpPoint);

            if (Destroiable)
            {
                Destroy(gameObject);
            }
        }
    }
}
