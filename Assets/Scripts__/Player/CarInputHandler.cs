using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun; 

public class CarInputHandler : MonoBehaviourPunCallbacks
{
    private Vector2 inputVector = Vector2.zero;
    private CarController _carController;

    // 수정: InputSystem_Actions로 클래스명 변경
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        _carController = GetComponent<CarController>();
        
        // Input Actions 인스턴스 생성
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        // Input Action 활성화
        inputActions.Player.Move.Enable();
    }

    private void OnDisable()
    {
        // Input Action 비활성화
        inputActions.Player.Move.Disable();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            // 입력값 읽기
            inputVector = inputActions.Player.Move.ReadValue<Vector2>();

            // 입력값 전달
            _carController.SetInputVector(inputVector);
            //Debug.Log(inputVector.x +" / "+ inputVector.y);
        }
    }
}