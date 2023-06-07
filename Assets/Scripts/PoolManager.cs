using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹들을 보관할 변수가 필요
    public GameObject[] prefabs;

    // 풀 담당을 하는 리스트들이 필요   >> 보관할 변수와 리스트는 1:1 비율로
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        // 반복문을 통해 모든 오브젝트 풀 리스트 초기화
        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    // 게임 오브젝트를 반환하는 함수
    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀에 놀고있는 게임오브젝트 접근
        foreach(GameObject item in pools[index])
        {
            // 내용물 오브젝트가 비활성화(대기상태)인지 확인하는 조건
            if (!item.activeSelf)
            {
                // 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true); // SetActive 함수로 활성화
                break;
            }
        }

        // 못 찾았으면?
        if(!select)
        {
            // >> 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
