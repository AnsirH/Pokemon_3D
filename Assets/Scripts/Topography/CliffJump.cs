using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CliffJump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


   

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PlayerMove>().currentState = PlayerMove.PlayerState.Jump;
    }

}
