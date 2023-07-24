using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject prefabBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �}�E�X�������ꂽ�Ƃ�
        if (Input.GetMouseButtonUp(0))
        {
            // �����ꂽ�}�E�X�̏ꏊ�փ��C�i�����j���΂�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // �x�N�g�����擾�i���[���h���W�j
            Vector3 dir = ray.direction;

            // �Q�[���I�u�W�F�N�g�𐶐�����
            GameObject bullet = Instantiate(prefabBullet);

            // �ʒu���w��
            bullet.transform.position = transform.position;

            // ���� (3000�͗́j
            bullet.GetComponent<BulletController>().Shoot(dir.normalized * 3000);
        }
    }
}
