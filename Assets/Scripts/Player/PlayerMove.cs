using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public event Action OnEncountered;

    // �÷��̾� �̵��ӵ�
    public float speed = 10.0f;

    // ���� üũ ���� ����
    public float rayDistance = 1.2f;

    // NavMeshAgent
    NavMeshAgent nav;

    // Rigidbody
    Rigidbody rb;

    public Animator anim;

    // �÷��̾��� ���� ���
    public enum PlayerState
    {
        Move,
        Jump,
        Flying
    }

    // ���� ����
    public PlayerState currentState = PlayerState.Move;

    // ���� �� Flying ���·� �Ѿ�� �� ������ �ð�
    public float jumpWaitTime = 0.5f;

    // �̵��Ÿ�
    float travelDistance = 0f;

    // ȸ�� �ӵ�
    public float rotSpeed = 10.0f;

    // �߻����ϸ� ���� Ȯ�� ������ �� ����� �ڵ�
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

    // ���߿� ������ �� ����Ǵ� �Լ�
    private void Flying()
    {
        // �ٴڿ� ����� �� Move ���·� ��ȯ
        if(Physics.Raycast(transform.position, Vector3.down, rayDistance))
        {

            nav.enabled = true;
            rb.velocity = Vector3.zero;
            currentState = PlayerState.Move;
            rb.constraints = RigidbodyConstraints.FreezeAll;

        }
    }

    // ������ �ϴ� �Լ�
    private void Jump()
    {
        nav.enabled = false;

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        rb.velocity = transform.TransformDirection(new Vector3(0, 3, 1.5f));

        StartCoroutine(JumpDelay());
    }


    // �����̸� �ְ� state�� Flying ���·� �ٲ��ִ� �ڷ�ƾ
    IEnumerator JumpDelay()
    {


        yield return new WaitForSeconds(jumpWaitTime);

        currentState = PlayerState.Flying;

    }

    // Move ������ �� ����Ǵ� �Լ�
    // ĳ���͸� �����δ�.
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
                Debug.Log("Ǯ�� ��");
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
            Debug.Log("���ϸ� ���Դ�, ���� ����" + randNum);
            anim.SetBool("Walk", false);
            OnEncountered();
        }
        else
        {
            Debug.Log("�ȳ��� ��");
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
