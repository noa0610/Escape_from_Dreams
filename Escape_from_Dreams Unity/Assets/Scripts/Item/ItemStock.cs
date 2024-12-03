using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ストックアイテムを管理するスクリプト
/// </summary>
public class ItemStock : MonoBehaviour
{
    // 内部処理する変数
    private int MaxStock = 3; // 最大ストック数
    private List<ItemDate> itemStock = new List<ItemDate>();

    // アイテムを追加
    public void AddItem(ItemDate item)
    {
        if (itemStock.Count >= MaxStock)
        {
            itemStock.RemoveAt(0); // 一番古いアイテムを削除
        }
        itemStock.Add(item); // 取得したアイテムを追加
        UpdateUI(); // UIを更新する
    }

    // 最新のアイテムを使用
    public void UseLatestItem()
    {
        if (itemStock.Count > 0) // ストックアイテムが1個以上あるとき
        {
            ItemDate usedItem = itemStock[itemStock.Count - 1]; // 使用するアイテムの取得
            itemStock.RemoveAt(itemStock.Count - 1); // 使用したアイテムを削除
            UpdateUI(); // UIを更新する
            Debug.Log($"Used item: {usedItem.Name}");
        }
        else
        {
            Debug.Log("No items to use.");
        }
    }

    // UIの更新（UI管理クラスに通知）
    private void UpdateUI()
    {
        // ここでUI更新処理を呼び出す
        FindObjectOfType<UIItemStock>()?.UpdateUI(itemStock);
    }
}
