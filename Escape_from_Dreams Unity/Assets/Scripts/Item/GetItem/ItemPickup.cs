using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージに配置するプレハブの効果（）
/// </summary>
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private string itemName; // アイテム名
    [SerializeField] private Sprite itemIcon; // アイテムの画像
    [SerializeField] private string SE_NAME;  // アイテム取得時のSEの名前

    /// <summary>
    /// アイテムのデータを取得できるメソッド
    /// </summary>
    /// <returns>ItemDate</returns>
    public ItemDate GetItem()
    {
        IItemEffect effect = GetComponent<IItemEffect>(); // プレハブにアタッチされた効果を取得
        return new ItemDate(itemName, itemIcon, effect); // プレハブのアイテムのデータを返す
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (SE_NAME != null)
            {
                SoundManager.Instance.PlaySE(SE_NAME); // SEを再生
            }
            Destroy(this.gameObject);
        }
    }
}
