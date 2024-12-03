using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージに配置するプレハブの効果
/// </summary>
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private string itemName; // アイテム名
    [SerializeField] private Sprite itemIcon; // アイテムの画像

    /// <summary>
    /// アイテムのデータを取得できるメソッド
    /// </summary>
    /// <returns>ItemDate</returns>
    public ItemDate GetItem()
    {
        IItemEffect effect = GetComponent<IItemEffect>(); // プレハブにアタッチされた効果を取得
        return new ItemDate(itemName, itemIcon, effect); // プレハブのアイテムのデータを返す
    }
}
