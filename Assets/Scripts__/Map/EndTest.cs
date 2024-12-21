using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndTest : MonoBehaviour
{
    public List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    private bool a = false;
    [SerializeField] private DistanceTracker _distanceTracker;
    [SerializeField] private Transform image; 
    
    
    void Start()
    {
        Invoke("Down" , 0.8f);
        _distanceTracker = GameObject.FindGameObjectWithTag("Player").GetComponent<DistanceTracker>();  
    }

    private void Down()
    {
        a = true; 
    }

    private void Update()
    {
        if (a)
        {
            // 현재 anchoredPosition 가져오기
            RectTransform rectTransform = texts[0].GetComponent<RectTransform>();
            Vector2 currentPosition = rectTransform.anchoredPosition;

            // y 값을 Lerp로 계산
            currentPosition.y = Mathf.Lerp(currentPosition.y, 410.0f, Time.deltaTime * 3.0f);

            // 수정된 값을 다시 anchoredPosition에 할당
            rectTransform.anchoredPosition = currentPosition;

            if (currentPosition.y >= 400.0f)
            {
                Invoke("True" , 0.3f);
                a = false; 
            }
        }
    }
    
    private void True()
    {
        texts[1].text = $"SCORE : {_distanceTracker.GetTotalDistance().ToString("F1")}"; 
        texts[1].gameObject.SetActive(true);
        texts[2].gameObject.SetActive(true);
        image.gameObject.SetActive(true);
    }
}
