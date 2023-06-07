using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        // 플레이어 키입력 값을 더한 이동 = 몬스터의 방향값을 더한 이동
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        // 물리속도가 이동에 영향을 주지 않도록 속도제거
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;
        // 몬스터 방향전환
        spriter.flipX = target.position.x < rigid.position.x;
        
    }

    // 스크립트가 활성화 될 때 호출되는 이벤트함수
    void OnEnable()
    {
        // GameManger의 요소인 player를 가져오는데 target의 형태인 Rigidbody2D를 적용시켜야한다.
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();    
    }
}
