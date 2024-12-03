using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour
{
    // 内部処理する変数
    private ItemStock itemStock;

    void Start()
    {
        itemStock = GetComponent<ItemStock>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E)) // "E"キーでアイテム使用
        {
            itemStock.UseLatestItem(); // アイテムのストックを消費
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item")) // アイテムに触れたら
        {
            ItemPickup pickup = other.GetComponent<ItemPickup>(); // アイテムのコンポーネント取得
            if(pickup != null)
            {
                itemStock.AddItem(pickup.GetItem()); // ストック内にアイテムを追加
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーが障害物に命中したときの処理
    }
}
