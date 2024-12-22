using Photon.Realtime;
using TMPro;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    private Vector3 _lastPosition; // 마지막 위치
    private float _totalDistance;  // 이동 거리\
    public TextMeshProUGUI text; 

    void Start()
    {
        text = GameObject.Find("AddSCore").GetComponent<TextMeshProUGUI>(); 
        // 시작 시 현재 위치를 저장
        _lastPosition = transform.position;
        _totalDistance = 0f;
    }

    void Update()
    {
        // 현재 위치와 이전 위치 사이의 거리 계산
        float distance = Vector3.Distance(transform.position, _lastPosition);
        
        // 총 이동 거리에 추가
        _totalDistance += distance;

        // 현재 위치를 마지막 위치로 업데이트
        _lastPosition = transform.position;

        // 디버그용 로그 (옵션)
        //Debug.Log("총 이동 거리: " + _totalDistance.ToString("F1") + " meters");
    }

    public void AddScore(float amount)
    {
        text.text = "";
        _totalDistance += amount;

        // DelayedUpdate를 Invoke로 호출
        Invoke(nameof(DelayedUpdate), 0.6f);

        // 내부에서 필요 변수 사용을 위해 amount를 임시 저장
        tempAmount = amount;
    }

// 클래스 메서드로 변경
    private float tempAmount;

    private void DelayedUpdate()
    {
        text.text = $"+{tempAmount}";
        Invoke(nameof(Remove), 1.3f);
    }

    private void Remove()
    {
        text.text = "";
    }


    public float GetTotalDistance()
    {
        return _totalDistance; // 다른 스크립트에서 이동 거리 조회
    }
}