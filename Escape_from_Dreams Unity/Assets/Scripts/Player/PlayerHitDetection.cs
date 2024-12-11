using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// プレイヤーの命中判定、アイテムの取得、使用ができる
/// </summary>
public class PlayerHitDetection : MonoBehaviour
{
    // インスペクターから設定する変数

    // 内部処理する変数
    private ItemStock _ItemStock;
    private PlayerMove _PlayerMove;

    private void Start()
    {
        this.gameObject.AddComponent<ItemStock>(); // アイテムストックスクリプトをアタッチ
        _ItemStock = GetComponent<ItemStock>();     // アイテムストック取得
        _PlayerMove = GetComponent<PlayerMove>();  // プレイヤーの移動
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // "E"キーでアイテム使用
        {
            _ItemStock.UseLatestItem(); // アイテムのストックを消費
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log($"{this.gameObject.name}:{other.gameObject.name}");
        if (other.CompareTag("Item")) // アイテムに触れたら
        {
            ItemPickup pickup = other.GetComponent<ItemPickup>(); // アイテムのコンポーネント取得
            if (pickup != null)
            {
                _ItemStock.AddItem(pickup.GetItem()); // ストック内にアイテムを追加
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーが障害物に命中したときの処理
        // 衝突した相手オブジェクトのタグを確認
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 最初の接触点を取得
            ContactPoint contact = collision.GetContact(0);

            // 衝突した自分側のコライダーを確認
            if (contact.thisCollider.CompareTag("BodyCollider"))
            {
                GameManager.Instance.IsGameOver = true; // ゲームオーバーフラグを立てる

                Debug.Log($"Game Over: Body hit an obstacle! Flag:{GameManager.Instance.IsGameOver}");

            }
            else if (contact.thisCollider.CompareTag("SideCollider"))
            {
                _PlayerMove.ForwardMoveReset(); // スピード減少
                _PlayerMove.Knockback(collision.transform.position); // ノックバック

                Debug.Log("Speed reduced: Side hit an obstacle!");
            }
        }
    }
}
