using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class RoomListItem : MonoBehaviour
{
    public TextMeshProUGUI roomNameText;
    public Button joinButton;

    public void SetRoomInfo(string roomName, UnityEngine.Events.UnityAction joinAction)
    {
        roomNameText.text = roomName;
        joinButton.onClick.AddListener(joinAction);
    }
}