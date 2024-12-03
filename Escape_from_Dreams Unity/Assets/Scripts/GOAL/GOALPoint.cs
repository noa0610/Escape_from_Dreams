using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴール地点の処理
/// </summary>
public class GOALPoint : MonoBehaviour
{
    // オブジェクトすり抜け判定取得
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) // プレイヤータグに接触したら
        {
            GameManager.Instance.IsGOAL = true; // ゴールフラグを立てる
        }
    }
}
