using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
        isLive = true;
        health = maxHealth;
    }

    // SpawnData 클래스의 변수들을 가져와서 어떤 몬스터타입인지 초기화해주는 함수
    public void Init(SpawnData data)
    {
        // 애니메이터에 몬스터 타입을 넣어준다
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if(health > 0)
        {
            // 살아있다. 히트 액션

        }
        else
        {
            // 죽었다.
            Dead();
        }
    }

    void Dead()
    {
        // 몬스터들은 오브젝트 풀링으로 소환되기 때문에 비활성화 시켜야한다.
        // Destroy 파괴시키면 안된다.
        gameObject.SetActive(false);
    }
}
