using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    public float rotateSpeed = 50.0f;

    Rigidbody rigid;
    Animator anim;

    Vector3 direction;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            rigid.MovePosition(rigid.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    //private void Update()
    //{
    //}

    private void LateUpdate()
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion rotateValue = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            transform.rotation = rotateValue;
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        direction = new Vector3(inputVector.x, 0.0f, inputVector.y);
        anim.SetFloat("Speed", direction.magnitude);
    }
}
