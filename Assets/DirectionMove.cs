
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
        lastMoveDirection = transform.forward;

        //aimationLength 초기화.
        foreach (var animationClip in animator.runtimeAnimatorController.animationClips)
        {
            aimationLength[animationClip.name] = animationClip.length; // aimationLength["Attack"] = 1.4;
        }
    }

    Dictionary<string, float> aimationLength = new Dictionary<string, float>();

    void Update()
    {

        void Update()
        {
            Vector3 move = Vector3.zero; // move = new Vector3(0, 0, 0)

            // 어택을 했으면 어택 애니메이션 진행중인 동안은 
            // Run과 Idle을 하지 않게 하자
            // 각 애니메이션의 길이가 필요하다.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //어택에니메이션이 끝났을때 state = None
                StartCoroutine(AttackCo());
            }

            if (state != StateType.Attack)
            {
                move = MoveAndIdle(move);
            }

            transform.forward = Vector3.Slerp(transform.forward
                , move, roateLerp);
        }

        IEnumerator AttackCo()
        {
            state = StateType.Attack;
            animator.Play("Attack");
            float attackAnimationTime = aimationLength["Attack"];
            yield return new WaitForSeconds(attackAnimationTime);
            state = StateType.None;
        }

        /// <summary>
        /// Move/Idle애니메이션, 실제 이동
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        Vector3 MoveAndIdle(Vector3 move)
        {
            if (Input.GetKey(KeyCode.A)) move.x = -1;
            if (Input.GetKey(KeyCode.D)) move.x = 1;
            if (Input.GetKey(KeyCode.W)) move.z = 1;
            if (Input.GetKey(KeyCode.S)) move.z = -1;

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