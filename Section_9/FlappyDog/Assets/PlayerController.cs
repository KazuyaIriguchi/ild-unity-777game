using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // jump
    public float jumpVelocity = 400;
    Rigidbody2D rigidbody;

    // out of field
    Vector2 screenTop, screenBottom;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        screenTop = Camera.main.ViewportToWorldPoint(Vector2.one);
        screenBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Jump();
        }
    }

    // ��������
    private void FixedUpdate()
    {
        // ��ʊO�̔���
        Vector3 pos = transform.position;

        // top
        if (pos.y > screenTop.y)
        {
            // reset to speed
            rigidbody.velocity = Vector2.zero;
            pos.y = screenTop.y;
        }

        // bottom
        if (pos.y < screenBottom.y)
        {
            // ���܂ōs������W�����v
            Jump();
            pos.y = screenBottom.y;
        }

        // update
        transform.position = pos;
    }

    void Jump()
    {
        // �������x�̃��Z�b�g
        rigidbody.velocity = Vector2.zero;

        // jump
        rigidbody.AddForce(new Vector2(0, jumpVelocity));
    }

    // collision check
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ǂɂԂ�������v���C���[������
        Destroy(gameObject);
    }
}
