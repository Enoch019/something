using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Scene GameScene; 
    public void ConnectToPhoton()
    {
        SceneManager.LoadScene(1);  
    }
}
