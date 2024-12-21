using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using Photon.Realtime;
using Random = UnityEngine.Random;
using Photon.Pun; 

public class Road : MonoBehaviourPunCallbacks
{
    [field: SerializeField] public UnityEvent roadObject { get; set; }
    private RoadMamager _roadMamager;
    private SpriteRenderer spriteRenderer;
    private float yTop;
    [SerializeField] private List<GameObject> roadGameObjects = new List<GameObject>();
    [SerializeField] private Transform _objectPool; 
    private int a { get; set; }
    
    private void Awake()
    {
        _roadMamager = GameObject.FindGameObjectWithTag("RoadManager").GetComponent<RoadMamager>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
        {
            // 오브젝트의 중심 위치와 크기 가져오기
            Vector3 position = transform.position;
            Vector3 size = spriteRenderer.bounds.size;

            // Y 좌표의 끝 계산 (위쪽)
            yTop = position.y + (size.y / 2);
        }
        
        roadObject.AddListener(Spawn);
        roadObject.AddListener(StartSpawnS);
        
        //StartSpawnS();
    }

    public void StartSpawnS()
    {
        WaitAndExecuteAsync(Random.Range(_roadMamager.RandomRAngeTimeMin , _roadMamager.RandomRAngeTime)); //랜덤한 시간 출력
    }

    async void WaitAndExecuteAsync(float waitTime)
    {
        // 밀리초 단위로 변환 후 대기
        await Task.Delay((int)(waitTime * 1000));
        
        roadObject.Invoke();
    }

    public void Spawn()
    {
        float randomValue = Random.Range(0, _roadMamager.RandomRangeSpawn); // 0 ~ 99 사이의 값

        if (1 <= randomValue)
        {
            // 오브젝트 생성
            Vector3 vec = new Vector3(transform.position.x, yTop, transform.position.z);
            GameObject ac = PhotonNetwork.Instantiate(
                roadGameObjects[Random.Range(0, roadGameObjects.Count)].name,
                vec,
                Quaternion.identity
            );
            ac.transform.parent = _objectPool;

            Debug.Log($"Spawned object! RandomValue: {randomValue}");
        }
        else
        {
            Debug.Log($"No spawn. RandomValue: {randomValue}");
        }
    }

}
