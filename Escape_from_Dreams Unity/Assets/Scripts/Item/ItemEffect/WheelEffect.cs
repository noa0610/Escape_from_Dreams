using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム「タイヤ」を使用したときの効果
/// </summary>
public class WheelEffect : MonoBehaviour, IItemEffect
{
    [SerializeField] private float SPEED_BOOST_AMOUNT = 2f;  // 速度上昇量
    [SerializeField] private float DURATION = 3f;            // 効果時間
    [SerializeField] private GameObject WHEEL_ATTACK_PREFAB; // タイヤ攻撃判定
    
    public void ApplyEffect()
    {
        PlayerMove player = FindObjectOfType<PlayerMove>(); // プレイヤーを取得
        if (player != null)
        {
            SpeedUpEffect effect = new SpeedUpEffect(SPEED_BOOST_AMOUNT, DURATION, true); // 速度上昇効果を追加
            player.AddStatusEffect(effect); // 速度上昇効果をプレイヤーに付与

            player.SetAttackAreaPrefab(WHEEL_ATTACK_PREFAB); // 攻撃判定をプレイヤーに付与
        }
    }
}
