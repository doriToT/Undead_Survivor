using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();    
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1); // 10�ʰ� �Ǹ� ������

        if(timer > (spawnData[level].spawnTime))
        {
            timer = 0;
            Spawn();
        }
    }
    
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        // �ڽ� ������Ʈ������ ���õǵ��� ���������� 1����
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

// ����ȭ
[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}