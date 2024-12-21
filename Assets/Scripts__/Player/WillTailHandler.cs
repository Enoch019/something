using System;
using UnityEngine;

public class WillTailHandler : MonoBehaviour
{
    private CarController _carController;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _carController = GetComponentInParent<CarController>();

        _trailRenderer.emitting = false; 
    }

    private void Update()
    {
        if (_carController.IsTireScreeching(out float lateralVelocity, out bool isBreaking))
            _trailRenderer.emitting = true;
        else
            _trailRenderer.emitting = false; 
    }
}
