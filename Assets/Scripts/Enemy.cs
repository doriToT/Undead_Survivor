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
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        // ������ ������¸� ��Ÿ���� �Լ��� ���ǿ� �߰��� �˹��� �߻������ش�.
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
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
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine("KnockBack");

        if(health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;  // ���� �ϳ��� ���� ������ ������
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        // ���͵��� ������Ʈ Ǯ������ ��ȯ�Ǳ� ������ ��Ȱ��ȭ ���Ѿ��Ѵ�.
        // Destroy�� �ı���Ű�� �ȵȴ�.
        gameObject.SetActive(false);
    }
}
