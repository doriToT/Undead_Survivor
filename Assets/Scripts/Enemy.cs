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

        // �÷��̾� Ű�Է� ���� ���� �̵� = ������ ���Ⱚ�� ���� �̵�
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        // �����ӵ��� �̵��� ������ ���� �ʵ��� �ӵ�����
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;
        // ���� ������ȯ
        spriter.flipX = target.position.x < rigid.position.x;
        
    }

    // ��ũ��Ʈ�� Ȱ��ȭ �� �� ȣ��Ǵ� �̺�Ʈ�Լ�
    void OnEnable()
    {
        // GameManger�� ����� player�� �������µ� target�� ������ Rigidbody2D�� ������Ѿ��Ѵ�.
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    // SpawnData Ŭ������ �������� �����ͼ� � ����Ÿ������ �ʱ�ȭ���ִ� �Լ�
    public void Init(SpawnData data)
    {
        // �ִϸ����Ϳ� ���� Ÿ���� �־��ش�
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
            // ����ִ�. ��Ʈ �׼�

        }
        else
        {
            // �׾���.
            Dead();
        }
    }

    void Dead()
    {
        // ���͵��� ������Ʈ Ǯ������ ��ȯ�Ǳ� ������ ��Ȱ��ȭ ���Ѿ��Ѵ�.
        // Destroy �ı���Ű�� �ȵȴ�.
        gameObject.SetActive(false);
    }
}
