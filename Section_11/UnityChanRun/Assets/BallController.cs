using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speedZ = -0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���݂̃I�u�W�F�N�g�̈ʒu����ɂ��Ĉړ�
        transform.Translate(0, 0, speedZ);

        // ��ʊO�ɏo�������
        if (transform.position.z < -5.0f)
        {
            Destroy(gameObject);
        }
    }
}
