using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴール地点の処理
/// </summary>
public class GOALPoint : MonoBehaviour
{
    // インスペクターから設定する変数
    [SerializeField] private int UNLOCK_STAGE_NUMBER; // 解放するステージの番号

    // オブジェクトすり抜け判定取得
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // プレイヤータグに接触したら
        {
            if (StageManager.Instance != null && StageManager.Instance.IsStageUnlocked(UNLOCK_STAGE_NUMBER) == false)
            {
                StageManager.Instance.UnlockStage(UNLOCK_STAGE_NUMBER); // ステージを解放
            }
            GameManager.Instance.IsGOAL = true; // ゴールフラグを立てる
        }
    }
}
