using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    bool isStop;
    float startY;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStop)
        {
            // Go�{�^���������ꂽ�珈�������Ȃ�
            return;
        }

        // ���t���[����]������
        transform.Rotate(new Vector3(0, 5, 0));

        // ������������
        transform.Translate(0, -0.01f, 0);
        
        // ���܂ł��������֖߂�
        if (1 > transform.position.y)
        {
            Vector3 pos = transform.position;
            pos.y = startY;
            transform.position = pos;
        }
    }

    // �R���C�_�[�̓����蔻�肪�������Ƃ��ɌĂ΂��
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.name.Equals("Clear"))
        {
            Debug.Log("Clear.");
        }
    }

    public void OnGoButtonPressed()
    {
        isStop = true;

        // ����
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
