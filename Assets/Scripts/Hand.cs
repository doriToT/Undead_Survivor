using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftRotate = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotateReverse = Quaternion.Euler(0, 0, -135);

    void Awake()
    {
        //                                     2��°�� �θ��� SpriteRenderer
        player = GetComponentsInParent<SpriteRenderer>()[1];    
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX;
        
        if(isLeft) // ��������
        {
            transform.localRotation = isReverse ? leftRotateReverse : leftRotate;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else // ���Ÿ�����
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
