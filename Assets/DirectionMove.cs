using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMove : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lastMoveDirection = transform.forward;

        //aimationLength 초기화.
    }

    Dictionary<string, float> aimationLength = null;

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
            transform.Translate(move * 5 * Time.deltaTime
                , Space.World);

            //transform.forward = move; // 이코드 있으면 작동안함.
            lastMoveDirection = move;
            state = StateType.Run;
            animator.Play("Run");
        }
        else
        {
            move = lastMoveDirection;
            state = StateType.Idle;
            animator.Play("Idle");
        };
        return move;
    }
    public float speed = 5;
    public float roateLerp = 0.5f;
    public Vector3 lastMoveDirection;

    public StateType state = StateType.Idle;
    public enum StateType
    {
        None,  // 아무것도 안하는 상태, 어택이 끝나면 되는 상태
        Idle,  // 가만있을때,...
        Run,
        Jump,
        Attack,
    }
}