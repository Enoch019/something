using System;
using UnityEngine;
using Photon.Pun; 
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviourPunCallbacks
{
    [Header("Car Settings")] 
    public float driftFactor = 0.95f; 
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;
    
    private float accelerationInput = 0;
    private float steeringInput = 0;

    private float rotationAngle = 0;
    private float velocityVsUp = 0; 

    private Rigidbody2D carRigidbody2D;
    private NetworkManagerInGame _networkManagerInGame;
    public bool a = false; 
    
    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        _networkManagerInGame = GameObject.Find("NetworkManager2").GetComponent<NetworkManagerInGame>(); 
        _networkManagerInGame.players.Add(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (_networkManagerInGame.AllReady || a)
        {
            ApplyEngineFoce();
        
            KillOrthogonalVolecity(); 
        
            ApplaySteering();   
        }
    }

    void ApplyEngineFoce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.linearVelocity);
        
        if(velocityVsUp > maxSpeed && accelerationInput > 0)
            return;
        
        if(velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;
        
        if(carRigidbody2D.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;
        
        if (accelerationInput == 0)
            carRigidbody2D.linearDamping = Mathf.Lerp(carRigidbody2D.linearDamping, 2.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.linearDamping = 0; 
        
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor; 
        carRigidbody2D.AddForce(engineForceVector , ForceMode2D.Force);
    }
    void ApplaySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.linearVelocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor); 
        
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor; 
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVolecity()
    {
        Vector2 fowardVelocity = transform.up * Vector2.Dot(carRigidbody2D.linearVelocity, transform.up); 
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.linearVelocity, transform.right);

        carRigidbody2D.linearVelocity = fowardVelocity + rightVelocity * driftFactor; 
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y; 
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBreaking)
    {
        lateralVelocity = GetLateralVelocity();
        isBreaking = false;

        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBreaking = true;
            return true; 
        }

        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
            return true;

        return false; 

    }

    public float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carRigidbody2D.linearVelocity); 
    }
    
    public void AddPower(float time , float amount , bool isSpeedUpPoint)
    {
        maxSpeed += amount;
        if (!isSpeedUpPoint)
        {
            WaitAndExecuteAsync(time , amount);   
        }
    }
    
    async void WaitAndExecuteAsync(float waitTime , float amount)
    {
        // 밀리초 단위로 변환 후 대기
        await Task.Delay((int)(waitTime * 1000));
        
        maxSpeed -= amount; 
    }
}