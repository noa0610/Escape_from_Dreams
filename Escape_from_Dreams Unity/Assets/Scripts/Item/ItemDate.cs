using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDate
{
    public string Name; // アイテム名
    public Sprite Icon; // アイテムのアイコン
    public IItemEffect Effect; // アイテムの効果
    public ItemDate(string name, Sprite icon, IItemEffect effect)
    {
        Name = name;
        Icon = icon;
        Effect = effect;
    }

    public void UseItem()
    {
        Effect?.ApplyEffect();
    }
}
