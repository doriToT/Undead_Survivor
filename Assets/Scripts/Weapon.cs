using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;   
    public float damage;
    public int count;    // 관통력
    public float speed;   // 연사속도를 의미: 적을수록 많이 발사

    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // Test용
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage;
        this.count += count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    // id에 따라 초기화시켜주는 함수
    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero; // player안에서 위치조정하기 때문에 localPosition사용

        // Property Set
        id = data.itemId;
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            // index는 결국 prefab의 id가 될것이다.
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }        
        switch(id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed; // 음수값으로 하면 시계방향으로 회전함
                Batch();
                break;
            default:
                speed = 0.5f * Character.WeaponRate;
                break;
        }

        // Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        //                                     꼭 Receiver가 필요하진 않다. 추가
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for(int index = 0; index < count; index++)
        {
            // 왜 Transform 이냐 >> poolManager에서 Weapon0으로 부모를 바꾸기 위해서 
            Transform bullet;
            
            if (index < transform.childCount)
            {
                // 기존 오브젝트를 먼저 활용하고
                bullet = transform.GetChild(index);
            }
            else
            {
                // 모자란 것은 오브젝트 풀링에서 가져온다
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform; // PoolManager에서 Weapon0 으로 부모 변경
            }
  

            bullet.localPosition = Vector3.zero; // 플레이어의 위치로 초기화
            bullet.localRotation = Quaternion.identity;

            // 근접무기 회전
            Vector3 rotateVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotateVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity Per.
        }
    }

    // 총알 발사하는 로직
    void Fire()
    {
        if (!player.scanner.nearstTarget)
            return;

        Vector3 targetPos = player.scanner.nearstTarget.position;
        Vector3 dir = targetPos - transform.position;  // 크기가 포함된 방향: 목표위치 - 나의 위치
        dir = dir.normalized;
        Transform bullet = GameManager.instance.pool.Get(2).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
