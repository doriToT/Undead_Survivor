using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;

    Image icon;
    Text textLevel;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];    // �ι�° ������ ��������(�迭�� ù��°�� �ڱ��ڽ�)
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }

    // ��ư Ŭ�� �̺�Ʈ�� ������ �Լ�
    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:

                break;
            case ItemData.ItemType.Glove:

                break;
            case ItemData.ItemType.Shoe:

                break;
            case ItemData.ItemType.Heal:

                break;
        }

        level++;

        // �ִ뷹���� �����ϰ� �Ǹ�
        if(level == data.damages.Length)
        {
            // ��ũ��Ʈ�� ������Ʈ�� �ۼ��� ���� ������ ������ �����ʵ��� �����߰�
            GetComponent<Button>().interactable = false;
        }
    }
}
