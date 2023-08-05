using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    // 移動量
    float moveForce = 5;
    float jumpForce = 5;

    Animator animator;

    // ジャンプ
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
        // ジャンプ処理
        if (Input.GetKeyUp("space"))
        {
            if (!isJump && !isJumpWait)
            {
                animator.Play("Jump", 0, 0);

                isJumpWait = true;
                jumpWaitTimer = 0.2f;
            }
        }

        // 少し立ってからジャンプ
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

    // 当たり判定が発生したときに呼ばれる
    private void OnCollisionEnter(Collision collision)
    {
        // 着地したらジャンプフラグを落とす
        isJump = false;
    }
}
