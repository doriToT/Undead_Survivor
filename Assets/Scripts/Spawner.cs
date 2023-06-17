using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    int level;
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        // 최대 시간에 몬스터 데이터 크기로 나누어 자동으로 구간거리 계산
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1); // 10초가 되면 레벨업

        if(timer > (spawnData[level].spawnTime))
        {
            timer = 0;
            Spawn();
        }
    }
    
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        // 자식 오브젝트에서만 선택되도록 랜덤시작은 1부터
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

// 직렬화
[System.Serializable]
// 몬스터 스폰 클래스
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}