using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    // �ړ���
    float moveForce = 5;
    float jumpForce = 5;
    Vector3 targetPosition;

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
        // �v���C���[�̈ړ�����
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject target = null;

            // ���ׂĂ̓����蔻����擾
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

        // �ړ������s
        // ���̂��蔲���h�~
        targetPosition.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveForce * Time.deltaTime);

        // �Ђ�����Ԃ�h�~
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    // �����蔻�肪���������Ƃ��ɌĂ΂��
    private void OnCollisionEnter(Collision collision)
    {
        // ���n������W�����v�t���O�𗎂Ƃ�
        isJump = false;
    }

    // �����蔻��̏���
    private void OnTriggerEnter(Collider other)
    {
        // �q�b�g�����I�u�W�F�N�g�̖��O
        string name = other.gameObject.name;

        // �{�[�����ǂ�������
        if (!name.Contains("Ball")) return;

        Destroy(other.gameObject);

        // ���_�̌v�Z
        if (name.Contains("Red"))
        {
            director.GetComponent<GameDirector>().AddScore();
        }
        else
        {
            // �n�Y�� ���x�𗎂Ƃ�
            moveForce += 0.9f;
        }
    }
}
