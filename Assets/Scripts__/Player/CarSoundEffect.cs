using System;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSoundEffect : MonoBehaviour
{
    [Header("Audio sources")] 
    public AudioSource tireS;
    public AudioSource engineS;
    public AudioSource carHitS;

    private float desiredEnginePitch = 0.5f;
    private float tireDriftPitch = 0.5f; 
    private CarController _carController;

    private void Awake()
    {
        _carController = GetComponent<CarController>(); 
    }

    private void Update()
    {
        
    }

    void UpdateEngineSFX()
    {
        float vm = _carController.GetLateralVelocity();

        float dev = vm * 0.05f;

        dev = Mathf.Clamp(dev , 0.2f , 1.0f);

        engineS.volume = Mathf.Lerp(engineS.volume, dev, Time.deltaTime * 10);

        desiredEnginePitch = vm * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);

        engineS.pitch = Mathf.Lerp(engineS.pitch, desiredEnginePitch, Time.deltaTime * 1.5f); 
        
        
    }

    void UpdateDriftSFX()
    {
        if (_carController.IsTireScreeching(out float lateralVelocity, out bool isBreaking))
        {
            if (isBreaking)
            {
                tireS.volume = Mathf.Lerp(tireS.volume, 1.0f, Time.deltaTime * 10);
                tireDriftPitch = Mathf.Lerp(tireDriftPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                tireS.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                tireDriftPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }

        else tireS.volume = Mathf.Lerp(tireS.volume, 0, Time.deltaTime * 10); 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        float relativeVelocity = other.relativeVelocity.magnitude;
        float volume = relativeVelocity * 0.1f;

        carHitS.pitch = Random.Range(0.95f, 1.05f); 
        carHitS.volume = volume; 
        
        if(!carHitS.isPlaying)
            carHitS.Play();
    }
}
