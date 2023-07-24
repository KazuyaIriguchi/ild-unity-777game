using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject objGround;
    public GameObject txtInfo;

    Vector2 startPos;
    Rigidbody2D rb;

    // ���C�̒l
    float frictionForce = 0.98f;
    bool gameEnd;

    int touchCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd) return;

        if (Input.GetMouseButtonDown(0))
        {
            // �J�n�n�_���擾
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (0 < touchCount)
            {
                // �I���n�_���擾
                Vector2 endPos = Input.mousePosition;
                // ���Z����l
                Vector2 addForce = endPos - startPos;

                rb.AddForce(addForce);

                touchCount--;
            }
        }

        // ���C�����ď����Â���������
        rb.velocity *= frictionForce;

        // �e�L�X�g�X�V
        float sx = transform.position.x;
        float sy = transform.position.y;
        float ex = objGround.transform.localScale.x / 2;
        float dx = ex - sx;

        txtInfo.GetComponent<TMP_Text>().text = dx.ToString("N1") + " m";

        // �S�[������
        if (0 < dx && dx < 1)
        {
            // ��~����
            if (0.01f > rb.velocity.x)
            {
                txtInfo.GetComponent<TMP_Text>().text = "Success";

                // �S�[�����o
                txtInfo.GetComponent<TMP_Text>().color = Color.black;
                GetComponent<SpriteRenderer>().color = Color.black;
                objGround.GetComponent<SpriteRenderer>().color = Color.black;
                Camera.main.GetComponent<Camera>().backgroundColor = Color.white;

                gameEnd = true;
            }
        }
        else if (-10> sy)
        {
            // ���ɗ������玸�s
            txtInfo.GetComponent<TMP_Text>().text = "Failure...";
            gameEnd = true;
        }
    }

    // RETRY�{�^���̏���
    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
