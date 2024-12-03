using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemStock : MonoBehaviour
{
    [SerializeField] public List<Image> itemSlots; // アイテムスロットUI（3つのImageコンポーネント）

    // ストックに基づいてUIを更新
    public void UpdateUI(List<ItemDate> items)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < items.Count)
            {
                itemSlots[i].sprite = items[i].Icon;
                itemSlots[i].enabled = true;
            }
            else
            {
                itemSlots[i].enabled = false;
            }
        }
    }
}
