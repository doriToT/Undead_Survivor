using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �����յ��� ������ ������ �ʿ�
    public GameObject[] prefabs;

    // Ǯ ����� �ϴ� ����Ʈ���� �ʿ�   >> ������ ������ ����Ʈ�� 1:1 ������
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        // �ݺ����� ���� ��� ������Ʈ Ǯ ����Ʈ �ʱ�ȭ
        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    // ���� ������Ʈ�� ��ȯ�ϴ� �Լ�
    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� ����ִ� ���ӿ�����Ʈ ����
        foreach(GameObject item in pools[index])
        {
            // ���빰 ������Ʈ�� ��Ȱ��ȭ(������)���� Ȯ���ϴ� ����
            if (!item.activeSelf)
            {
                // �߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true); // SetActive �Լ��� Ȱ��ȭ
                break;
            }
        }

        // �� ã������?
        if(!select)
        {
            // >> ���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
