using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    private Vector3 _lastPosition; // 마지막 위치
    private float _totalDistance;  // 이동 거리

    void Start()
    {
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

    public float GetTotalDistance()
    {
        return _totalDistance; // 다른 스크립트에서 이동 거리 조회
    }
}