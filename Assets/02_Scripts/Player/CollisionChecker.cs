using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    [Header("충돌 설정")]
    public LayerMask obstacleLayer = 1;
    public float checkDistance = 0.1f;
    public float checkRadius = 0.3f;
    public int maxCheckRays = 8;
    
    [Header("디버그 설정")]
    public bool showDebugRays = false;
    public Color debugRayColor = Color.red;
    public Color validRayColor = Color.green;
    
    [Header("컴포넌트")]
    public CharacterController characterController;
    
    // 개인 변수들
    private Vector3 lastValidPosition;
    private Vector3 lastValidDirection;
    
    private void Awake()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
            
        lastValidPosition = transform.position;
    }
    
    private void Update()
    {
        // 충돌하지 않을 때 마지막 유효 위치 업데이트
        if (IsValidPosition(transform.position))
        {
            lastValidPosition = transform.position;
        }
    }
    
    public bool CheckCollision(Vector3 direction, float distance)
    {
        if (direction.magnitude < 0.1f)
            return false;
            
        // 방향 정규화
        direction = direction.normalized;
        
        // 더 나은 충돌 감지를 위해 여러 레이 체크
        for (int i = 0; i < maxCheckRays; i++)
        {
            float angle = (360f / maxCheckRays) * i;
            Vector3 rayDirection = Quaternion.AngleAxis(angle, Vector3.up) * direction;
            
            if (Physics.Raycast(transform.position, rayDirection, distance, obstacleLayer))
            {
                if (showDebugRays)
                {
                    Debug.DrawRay(transform.position, rayDirection * distance, debugRayColor, 0.1f);
                }
                return true;
            }
        }
        
        // 구체 캐스트를 사용한 추가 체크
        if (Physics.SphereCast(transform.position, checkRadius, direction, out RaycastHit hit, distance, obstacleLayer))
        {
            if (showDebugRays)
            {
                Debug.DrawRay(transform.position, direction * hit.distance, debugRayColor, 0.1f);
            }
            return true;
        }
        
        if (showDebugRays)
        {
            Debug.DrawRay(transform.position, direction * distance, validRayColor, 0.1f);
        }
        
        return false;
    }
    
    public Vector3 GetValidMoveDirection(Vector3 desiredDirection)
    {
        if (desiredDirection.magnitude < 0.1f)
            return Vector3.zero;
            
        desiredDirection = desiredDirection.normalized;
        
        // 원하는 방향이 유효한지 체크
        if (!CheckCollision(desiredDirection, checkDistance))
        {
            lastValidDirection = desiredDirection;
            return desiredDirection;
        }
        
        // 대안 방향 찾기 시도
        Vector3[] alternativeDirections = GetAlternativeDirections(desiredDirection);
        
        foreach (Vector3 altDirection in alternativeDirections)
        {
            if (!CheckCollision(altDirection, checkDistance))
            {
                lastValidDirection = altDirection;
                return altDirection;
            }
        }
        
        // 유효한 방향을 찾지 못하면 제로 반환
        return Vector3.zero;
    }
    
    private Vector3[] GetAlternativeDirections(Vector3 originalDirection)
    {
        Vector3[] alternatives = new Vector3[8];
        
        // 원본 주변에 8개의 대안 방향 생성
        for (int i = 0; i < 8; i++)
        {
            float angle = 45f * i;
            alternatives[i] = Quaternion.AngleAxis(angle, Vector3.up) * originalDirection;
        }
        
        return alternatives;
    }
    
    public bool IsValidPosition(Vector3 position)
    {
        // 구체 오버랩을 사용하여 위치가 유효한지 체크
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius, obstacleLayer);
        
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject && !collider.isTrigger)
            {
                return false;
            }
        }
        
        return true;
    }
    
    public bool CanMoveTo(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);
        
        return !CheckCollision(direction, distance);
    }
    
    public Vector3 GetNearestValidPosition(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);
        
        // 레이캐스트를 사용하여 가장 가까운 유효 위치 찾기
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance, obstacleLayer))
        {
            return hit.point - direction * checkRadius;
        }
        
        return targetPosition;
    }
    
    public void PreventWallSticking()
    {
        // 플레이어가 벽에 붙어있는지 체크
        if (CheckCollision(transform.forward, checkDistance))
        {
            // 벽에서 벗어나려고 시도
            Vector3 escapeDirection = -transform.forward;
            Vector3 validDirection = GetValidMoveDirection(escapeDirection);
            
            if (validDirection != Vector3.zero)
            {
                transform.position += validDirection * checkDistance;
            }
        }
    }
    
    public void HandleSlopeMovement(Vector3 moveDirection)
    {
        // 경사면을 체크하고 그에 따라 이동 조정
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
        {
            Vector3 slopeDirection = Vector3.Cross(Vector3.Cross(hit.normal, Vector3.up), hit.normal);
            moveDirection = Vector3.ProjectOnPlane(moveDirection, hit.normal);
        }
    }
    
    // 디버그 메서드들
    private void OnDrawGizmosSelected()
    {
        if (showDebugRays)
        {
            Gizmos.color = debugRayColor;
            Gizmos.DrawWireSphere(transform.position, checkRadius);
            
            // 체크 레이 그리기
            for (int i = 0; i < maxCheckRays; i++)
            {
                float angle = (360f / maxCheckRays) * i;
                Vector3 rayDirection = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
                Gizmos.DrawRay(transform.position, rayDirection * checkDistance);
            }
        }
    }
    
    // 공개 getter 메서드들
    public Vector3 GetLastValidPosition()
    {
        return lastValidPosition;
    }
    
    public Vector3 GetLastValidDirection()
    {
        return lastValidDirection;
    }
    
    public bool IsColliding()
    {
        return CheckCollision(transform.forward, checkDistance);
    }
    
    public Collider GetNearestObstacle()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, obstacleLayer);
        
        Collider nearest = null;
        float nearestDistance = float.MaxValue;
        
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject && !collider.isTrigger)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = collider;
                }
            }
        }
        
        return nearest;
    }
}
