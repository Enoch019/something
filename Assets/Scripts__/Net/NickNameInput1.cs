using System;
using UnityEngine;
using TMPro;
using Photon.Pun; 

public class NickNameInput1 : MonoBehaviour
{
    public TMP_InputField nickNameInputField;
    public TextMeshProUGUI nickNameNum;
    public int possibleNum = 12; 

    private void Start()
    {
        nickNameInputField.characterLimit = possibleNum; 
    }

    private void Update()
    {
        int current = nickNameInputField.text.Length;
        nickNameNum.text = $"{current} / {possibleNum}";
    }

    public void ConnectToServer()
    {
        string nickName = nickNameInputField.text;

        if (!string.IsNullOrEmpty(nickName))
        {
            // PhotonNetwork에 닉네임 설정
            PhotonNetwork.NickName = nickName;

            // 네트워크 메니저의 ConnectToPhoton 메서드 호출
            FindObjectOfType<NetworkManager>().ConnectToPhoton();
        }
        else
        {
            Debug.LogWarning("닉네임을 입력하세요.");
        }
    }
}