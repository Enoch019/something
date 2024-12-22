using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public int Coins = 0;
    [SerializeField] private TextMeshProUGUI CoinTexts; 
    public void AddCoin()
    {
        //CoinTexts.text = $"{Coins}"; 
        Coins++; 
    }
}
