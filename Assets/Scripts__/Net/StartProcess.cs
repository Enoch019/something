using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartProcess : MonoBehaviour
{
    private NetworkManagerInGame _networkManagerInGame;
    private int CountToThree = 4;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private Transform _thingsToDestroy;
    [SerializeField] private Transform _frontGround; 
    
    
    public void Process()
    {
        Invoke("UpNumber" , 0.2f);
        _frontGround.gameObject.SetActive(true);
        _thingsToDestroy.gameObject.SetActive(false);
    }

    private void UpNumber()
    {
        CountToThree--;
        _countText.text = $"{CountToThree}"; 
        _countText.gameObject.SetActive(true);
        if(CountToThree > 0)
            Invoke("UpNumber" , 1.0f);
        else
        {
            int alph = 255; 
            for (int i = 0; i < 255; i++)
            {
                alph -= 1;
                _frontGround.GetComponent<Image>().color = new Color(0 , 0, 0 , alph);
            }

            _countText.text = "GO";
            GetComponent<NetworkManagerInGame>().AllReady = true; 
            Invoke("Destroty" , 0.4f);
        }
    }

    private void Destroty()
    {
        _frontGround.gameObject.SetActive(false);
        _countText.gameObject.SetActive(false);
    }
    
}