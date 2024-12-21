using UnityEngine;
using Photon.Pun;
using TMPro;

public class ReadyManager : MonoBehaviourPunCallbacks
{
    private bool isClick = false;
    public int Ready = 0;
    [SerializeField] private GameObject _car;
    public TextMeshProUGUI text; 
    [SerializeField] private GameObject playerPrefab;
    
    public void ClickThis()
    {
        if (!isClick)
        {
            photonView.RPC("AddReadyNum" , RpcTarget.All);
        }
    }

    [PunRPC]
    public void AddReadyNum()
    {
        Ready++;
        text.text = $"{Ready} / 4"; 

        if (PhotonNetwork.IsMasterClient && Ready == 4)
        {
            
        }
    }
    
    public void SpawnThing()
    {
        //GetComponent<PhotonView>().RPC("GiveHimMessage" , RpcTarget.All , PhotonNetwork.NickName + " Joined the room.");
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0,0,0), Quaternion.identity);
        PhotonNetwork.Instantiate(_car.name , Vector3.zero , Quaternion.identity);
    }
    
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
}
