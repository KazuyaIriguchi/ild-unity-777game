using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    // 移動量
    float moveForce = 5;
    float jumpForce = 5;
    Vector3 targetPosition;

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
        // プレイヤーの移動処理
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject target = null;

            // すべての当たり判定を取得
            foreach (RaycastHit hit in Physics.RaycastAll(ray))
            {
                target = (hit.transform.name.Equals("Ground"))
                    ? hit.transform.gameObject : null;
                if (null != target) break;
            }

            if (null != target)
            {
                targetPosition = target.transform.position;
                transform.forward = targetPosition;
            }
        }

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

        // 移動を実行
        // 床のすり抜け防止
        targetPosition.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveForce * Time.deltaTime);

        // ひっくり返り防止
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    // 当たり判定が発生したときに呼ばれる
    private void OnCollisionEnter(Collision collision)
    {
        // 着地したらジャンプフラグを落とす
        isJump = false;
    }

    // 当たり判定の処理
    private void OnTriggerEnter(Collider other)
    {
        // ヒットしたオブジェクトの名前
        string name = other.gameObject.name;

        // ボールかどうか判定
        if (!name.Contains("Ball")) return;

        Destroy(other.gameObject);

        // 得点の計算
        if (name.Contains("Red"))
        {
            director.GetComponent<GameDirector>().AddScore();
        }
        else
        {
            // ハズレ 速度を落とす
            moveForce += 0.9f;
        }
    }
}
