using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMove : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.A)) move.z = -1;
        if (Input.GetKey(KeyCode.D)) move.z = 1;
        if (Input.GetKey(KeyCode.S)) move.x = -1;
        if (Input.GetKey(KeyCode.W)) move.x = -1;

        if (move == Vector3.zero)
        {
            move.Normalize();
            transform.Translate(move * 5 * Time.deltaTime);

            transform.forward = move;
        }
    }
    public float speed = 5; //1초당 유닛 : 1초당 5유닛을 간다.

}
