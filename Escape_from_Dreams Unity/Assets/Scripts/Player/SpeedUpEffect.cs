using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpEffect
{
    public float speedBoost;  // スピード上昇量
    public float duration;    // 効果時間
    public bool enableAttack; // 攻撃判定を有効化するか
    private float startTime;   // 効果開始時間

    /// <summary>
    /// 速度上昇効果
    /// </summary>
    /// <param name="speedBoost">速度上昇倍率</param>
    /// <param name="duration">効果時間</param>
    /// <param name="enableAttack">攻撃判定の有無</param>
    public SpeedUpEffect(float speedBoost, float duration, bool enableAttack = false)
    {
        this.speedBoost = speedBoost;
        this.duration = duration;
        this.enableAttack = enableAttack;
        this.startTime = Time.time; // 効果開始時刻を記録
    }

    public bool IsExpired()
    {
        return Time.time > startTime + duration; // 現在時刻と開始時刻を比較し、効果が終わっているかを判定
    }
}