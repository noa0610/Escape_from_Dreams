using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムに含まれるデータを管理するクラス
/// </summary>
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

    /// <summary>
    /// アイテムを使用する処理
    /// </summary>
    public void UseItem()
    {
        Effect?.ApplyEffect();
    }
}
