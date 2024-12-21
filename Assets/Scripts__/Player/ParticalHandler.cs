using UnityEngine;

public class ParticalHandler : MonoBehaviour
{
    private float particleEmissionRate = 0; 
    private CarController _carController;

    private ParticleSystem _particleSystem;
    private ParticleSystem.EmissionModule particalSystemEmissionModule; 
    
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>(); 
        _carController = GetComponentInParent<CarController>();

        particalSystemEmissionModule = _particleSystem.emission;

        particalSystemEmissionModule.rateOverTime = 0; 
    }

    private void Update()
    {
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particalSystemEmissionModule.rateOverTime = particleEmissionRate; 
        
        if (_carController.IsTireScreeching(out float lateralVelocity, out bool isBreaking))
            particalSystemEmissionModule.rateOverTime = 30;
        else
            particalSystemEmissionModule.rateOverTime = Mathf.Abs(lateralVelocity) * 2; 
    }
}
