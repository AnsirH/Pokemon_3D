using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("입력 설정")]
    public float inputDeadzone = 0.1f;
    
    // 입력 값들
    private Vector2 movementInput;
    private bool runInput;

    private Vector2 ApplyDeadzone(Vector2 input)
    {
        if (input.magnitude < inputDeadzone)
        {
            return Vector2.zero;
        }
        
        // 데드존 후 입력 정규화
        return input.normalized * ((input.magnitude - inputDeadzone) / (1f - inputDeadzone));
    }
    
    // 공개 getter 메서드들
    public Vector3 GetMovementInput()
    {        
        return new Vector3(movementInput.x, 0.0f, movementInput.y);
    }
    
    public bool GetRunInput()
    {
        return runInput;
    }
    
    
    public bool IsMoving()
    {
        return movementInput.magnitude > inputDeadzone;
    }
    
    // 입력 시스템 이벤트 메서드
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.action.ReadValue<Vector2>();
        movementInput = ApplyDeadzone(rawInput);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            runInput = true;
            Debug.Log("달리기 시작");
        }
        else if (context.canceled)
        {
            runInput = false;
            Debug.Log("달리기 종료");
        }
    }
}
