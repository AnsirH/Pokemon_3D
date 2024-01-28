using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    Vector3 offset;

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
