using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;         // 게임 시작
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# GameObject")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public GameObject uiResult;

    void Awake()
    {
        instance = this;  
    }

    public void GameStart()
    {
        health = maxHealth;
        uiLevelUp.Select(0);  // 임시 스크립트 (첫번째 캐릭터 선택)
        isLive = true;
    }

    public void GameOver()
    {
        StartCoroutine("GameOverRoutine");
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.SetActive(true);
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    // 시간정지 함수
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    // 시간정지 후 재개하는 함수
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
