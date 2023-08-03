using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    // �^�C���֘A
    List<GameObject> tiles;
    List<Vector2> startPositions;
    public int shuffleCount = 2;

    // clear flag
    bool isClear;
    GameObject txtInfo;

    // Start is called before the first frame update
    void Start()
    {
        txtInfo = GameObject.Find("Text (TMP)");
        txtInfo.SetActive(false);

        tiles = new List<GameObject>();
        startPositions = new List<Vector2>();

        // �^�C���֘A
        for (int i = 0; i < 16; i++)
        {
            GameObject obj = GameObject.Find("" + i);
            tiles.Add(obj);

            // �����̃|�W�V�����i������ԁj
            startPositions.Add(obj.transform.position);
        }

        // �V���b�t������
        for (int i = 0; i < shuffleCount; i++)
        {
            List<GameObject> moves = new List<GameObject>();

            // ���ׂẴI�u�W�F�N�g
            foreach (GameObject obj in tiles)
            {
                // 0�ɗאڂ��Ă���I�u�W�F�N�g���擾
                if (null != GetExTile(obj))
                {
                    moves.Add(obj);
                }
            }

            if ( 0 < moves.Count)
            {
                // �����_����1������
                int rnd = Random.Range(0, moves.Count);
                GameObject tile0 = GetExTile(moves[rnd]);

                ChangeTile(moves[rnd], tile0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isClear) return;

        // �N���A����
        isClear = true;
        for (int i = 0; i < tiles.Count; i++)
        {
            Vector2 pos = tiles[i].transform.position;
            // �ŏ��ɕۑ������^�C���̈ʒu�Ɠ������𔻒�
            if (startPositions[i] != pos)
            {
                isClear = false;
            }
        }

        if (isClear)
        {
            txtInfo.SetActive(true);

            // �e���鏈��
            for (int i = 0; i < tiles.Count; i++)
            {
                float x = Random.Range(-30, 30);
                float y = Random.Range(-30, 30);
                tiles[i].AddComponent<Rigidbody2D>().velocity = new Vector2(x, y);
            }
        }

        // �^�b�`����
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10);

            if (hit.collider)
            {
                // �q�b�g�����I�u�W�F�N�g���擾
                GameObject hitObj = hit.collider.gameObject;
                // ����ւ��\�ȃI�u�W�F�N�g���擾
                GameObject target = GetExTile(hitObj);

                // ����ւ�
                ChangeTile(hitObj, target);
            }
        }
    }

    // ����ւ��\�ȃ^�C�����������ꍇ�A�����Ԃ�
    GameObject GetExTile(GameObject tile)
    {
        GameObject ret = null;

        Vector2 posa = tile.transform.position;

        foreach (var obj in tiles)
        {
            Vector2 posb = obj.transform.position;
            float dist = Vector2.Distance(posa, posb);

            // �אڂ���^�C���Ƃ̋�����1�ŁA����"0"�̃^�C���̂Ƃ�
            if ( 1 == dist && obj.name.Equals("0"))
            {
                ret = obj;
            }
        }

        return ret;
    }

    // �ꏊ�����ւ���
    void ChangeTile(GameObject tileA, GameObject tileB)
    {
        if (null == tileA || null == tileB) return;

        // �|�W�V�����X�V �ʒu�����ւ���
        Vector2 tmp = tileA.transform.position;
        tileA.transform.position = tileB.transform.position;
        tileB.transform.position = tmp;
    }

    // ���g���C�i�V�[���ēǂݍ��݁j
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
