using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    // �ړ���
    float moveForce = 5;
    float jumpForce = 5;

    Animator animator;

    // �W�����v
    bool isJump, isJumpWait;
    float jumpWaitTimer;

    GameObject director;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        // �W�����v����
        if (Input.GetKeyUp("space"))
        {
            if (!isJump && !isJumpWait)
            {
                animator.Play("Jump", 0, 0);

                isJumpWait = true;
                jumpWaitTimer = 0.2f;
            }
        }

        // ���������Ă���W�����v
        if (isJumpWait)
        {
            jumpWaitTimer -= Time.deltaTime;
            if (0 > jumpWaitTimer)
            {
                GetComponent<Rigidbody>().velocity = transform.up * jumpForce;
                isJumpWait = false;
                isJump = true;
            }
        }
    }

    // �����蔻�肪���������Ƃ��ɌĂ΂��
    private void OnCollisionEnter(Collision collision)
    {
        // ���n������W�����v�t���O�𗎂Ƃ�
        isJump = false;
    }
}
