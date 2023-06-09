using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;   
    public float damage;
    public int count;    // ����
    public float speed;

    void Start()
    {
        Init();    
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                break;
        }

        // Test��
        if (Input.GetButtonDown("Jump"))
        {
            // ������ 20, ���� 5���� ������
            LevelUp(20, 5);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();
    }

    // id�� ���� �ʱ�ȭ�����ִ� �Լ�
    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = 150; // ���������� �ϸ� �ð�������� ȸ����
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch()
    {
        for(int index = 0; index < count; index++)
        {
            // �� Transform �̳� >> poolManager���� Weapon0���� �θ� �ٲٱ� ���ؼ� 
            Transform bullet;
            
            if (index < transform.childCount)
            {
                // ���� ������Ʈ�� ���� Ȱ���ϰ�
                bullet = transform.GetChild(index);
            }
            else
            {
                // ���ڶ� ���� ������Ʈ Ǯ������ �����´�
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform; // PoolManager���� Weapon0 ���� �θ� ����
            }
  

            bullet.localPosition = Vector3.zero; // �÷��̾��� ��ġ�� �ʱ�ȭ
            bullet.localRotation = Quaternion.identity;

            // �������� ȸ��
            Vector3 rotateVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotateVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 is Infinity Per.
        }
    }
}
