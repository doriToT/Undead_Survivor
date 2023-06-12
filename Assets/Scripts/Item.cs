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
        icon = GetComponentsInChildren<Image>()[1];    // 두번째 값으로 가져오기(배열의 첫번째는 자기자신)
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }

    // 버튼 클릭 이벤트와 연결할 함수
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

        // 최대레벨에 도달하게 되면
        if(level == data.damages.Length)
        {
            // 스크립트블 오브젝트에 작성한 레벨 데이터 개수를 넘지않도록 로직추가
            GetComponent<Button>().interactable = false;
        }
    }
}
