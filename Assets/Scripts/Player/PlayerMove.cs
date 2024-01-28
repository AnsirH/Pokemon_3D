using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public event Action OnEncountered;

    // 플레이어 이동속도
    public float speed = 10.0f;

    // 지면 체크 레이 길이
    public float rayDistance = 1.2f;

    // NavMeshAgent
    NavMeshAgent nav;

    // Rigidbody
    Rigidbody rb;

    public Animator anim;

    // 플레이어의 상태 목록
    public enum PlayerState
    {
        Move,
        Jump,
        Flying
    }

    // 현재 상태
    public PlayerState currentState = PlayerState.Move;

    // 점프 후 Flying 상태로 넘어가기 전 딜레이 시간
    public float jumpWaitTime = 0.5f;

    // 이동거리
    float travelDistance = 0f;

    // 회전 속도
    public float rotSpeed = 10.0f;

    // 야생포켓몬 출현 확률 구현할 때 사용할 코드
    //enum RareDegree
    //{
    //    VeryCommon,
    //    Common,
    //    SemiRare,
    //    Rare,
    //    VeryRare
    //}

    //float encounterPer = 10f + 8.5f + 6.75f + 3.33f + 1.25f
    
    Vector3 camDir;

    Quaternion targetRot;

    bool onLongGrass;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;
    }

    private void Start()
    {
        camDir = Vector3.zero;
        targetRot = Quaternion.identity;
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        switch (currentState)
        {
            case PlayerState.Move:
                Move();
                break;

            case PlayerState.Jump:
                Jump();
                break;

            case PlayerState.Flying:
                Flying();
                break;
        }
    }

    // 공중에 떠있을 때 실행되는 함수
    private void Flying()
    {
        // 바닥에 닿았을 때 Move 상태로 전환
        if(Physics.Raycast(transform.position, Vector3.down, rayDistance))
        {

            nav.enabled = true;
            rb.velocity = Vector3.zero;
            currentState = PlayerState.Move;
            rb.constraints = RigidbodyConstraints.FreezeAll;

        }
    }

    // 점프를 하는 함수
    private void Jump()
    {
        nav.enabled = false;

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        rb.velocity = transform.TransformDirection(new Vector3(0, 3, 1.5f));

        StartCoroutine(JumpDelay());
    }


    // 딜레이를 주고 state를 Flying 상태로 바꿔주는 코루틴
    IEnumerator JumpDelay()
    {


        yield return new WaitForSeconds(jumpWaitTime);

        currentState = PlayerState.Flying;

    }

    // Move 상태일 때 실행되는 함수
    // 캐릭터를 움직인다.
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        float v = Input.GetAxisRaw("Vertical");


        Vector3 dir = new Vector3(h, 0, v);

        dir.Normalize();

        

        nav.Move(dir * speed * Time.deltaTime);

        

        if (dir != Vector3.zero)
        {

            if (onLongGrass)
            {
                Debug.Log("풀숲 들어감");
                travelDistance += speed * Time.deltaTime;
                if (travelDistance >= 3f)
                    CheckForEncounters();
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);

            anim.SetBool("Walk", true);
        }
        else
            anim.SetBool("Walk", false);


        

    }

    private void CheckForEncounters()
    {
        
        int randNum = UnityEngine.Random.Range(0, 101);
        travelDistance = 0f;

        if (randNum < 16)
        {
            Debug.Log("포켓몬 나왔당, 나온 숫자" + randNum);
            anim.SetBool("Walk", false);
            OnEncountered();
        }
        else
        {
            Debug.Log("안나옴 ㅠ");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LongGrass"))
        {
            onLongGrass = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("LongGrass"))
        {
            onLongGrass = false;
        }
    }
}
