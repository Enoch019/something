using System;
using UnityEngine;
using TMPro; 
using Photon.Pun; 

public class ConnectWithText : MonoBehaviourPunCallbacks
{
    private DistanceTracker _distanceTracker;
    private TextMeshProUGUI text;
    private int kilo = 0;
    private float met = 0; 

    private void Awake()
    {
        text = GameObject.Find("Distance").GetComponent<TextMeshProUGUI>(); 
        _distanceTracker = GetComponent<DistanceTracker>(); 
    }

    private void Update()
    {
        met = _distanceTracker.GetTotalDistance(); 

        if(photonView.IsMine)
            text.text = met.ToString("F1")+"m"; 
    }
}
