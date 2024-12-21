using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class EnhancedNetworkStatsDisplay : MonoBehaviourPunCallbacks
{
    public Text statsText; // UI 텍스트 컴포넌트 참조
    private float deltaTime = 0.0f;

    void Update()
    {
        // FPS 계산
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        // Photon 네트워크 관련 정보
        int ping = PhotonNetwork.GetPing(); // 핑
        ClientState connectionState = PhotonNetwork.NetworkClientState; // 연결 상태
        int sentRate = PhotonNetwork.SendRate; // 초당 전송 메시지 수
        int sendRateOnSerialize = PhotonNetwork.SerializationRate; // Serialize 전송 빈도
        int incomingPacketCount = 0; // 수신 패킷
        int outgoingPacketCount = 0; // 송신 패킷
        bool inRoom = PhotonNetwork.InRoom; // 방에 있는지 확인

        // 빌드 정보
        string buildVersion = Application.version;
        string platform = Application.platform.ToString();
        string roomName = inRoom ? PhotonNetwork.CurrentRoom.Name : "N/A"; // 현재 방 이름

        // 정보 업데이트
        statsText.text = string.Format(
            "FPS: {0:0.} \n" +
            "Ping: {1} ms\n" +
            "Connection State: {2}\n" +
            "Room: {3}\n" +
            "Send Rate: {4} msgs/sec\n" +
            "Serialize Rate: {5} msgs/sec\n" +
            "Incoming Packets: {6}\n" +
            "Outgoing Packets: {7}\n" +
            "Build Version: {8}\n" +
            "Platform: {9}",
            fps, ping, connectionState, roomName, sentRate, sendRateOnSerialize,
            incomingPacketCount, outgoingPacketCount, buildVersion, platform
        );
    }
}