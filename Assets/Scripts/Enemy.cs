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
    }
}
