using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMove : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        lastMoveDirection = transform.forward;
    }
    void Update()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.A)) move.z = -1;
        if (Input.GetKey(KeyCode.D)) move.z = 1;
        if (Input.GetKey(KeyCode.S)) move.x = -1;
        if (Input.GetKey(KeyCode.W)) move.x = 1;

        if (move != Vector3.zero)
        {
            move.Normalize();
            transform.Translate(move * 5 * Time.deltaTime, Space.World);

            transform.forward = move;
            state = StateType.Run;
            animator.Play("Run");
        }
        else
        {
            lastMoveDirection = move;
            state = StateType.Idle;
            animator.Play("Idle");
        }

        //회전 부드럽게 하는 것
        transform.forward = Vector3.Slerp(transform.forward, move, roateLerp);

    }
    public float speed = 5; //1초당 유닛 : 1초당 5유닛을 간다.
    public float roateLerp = 0.5f;
        public Vector3 lastMoveDirection ;

    public StateType state = StateType.Idle;
    public enum StateType
    {
    Idle,
    Run,
    Jump,
    Attack,
    }

}
