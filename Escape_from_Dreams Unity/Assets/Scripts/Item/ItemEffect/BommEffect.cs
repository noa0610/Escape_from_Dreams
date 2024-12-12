using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// アイテム「バクダン」を使用したときの効果
/// </summary>
public class BommEffect : MonoBehaviour, IItemEffect
{
    [SerializeField] private Transform SHOT_POINT;//発射する向きと位置
    [SerializeField] private GameObject BOMM_PREFAB; 
    [SerializeField] private float BOM_SPEED;//ボムの初速度
    public void ApplyEffect() // バクダン生成処理の実装
    {
        ShootBom(); // ShotBomメソッドを呼び出す
    }
    void ShootBom()//ボムの発射スクリプト
    {
        Vector3 vector = new Vector3(SHOT_POINT.position.x,SHOT_POINT.position.y,SHOT_POINT.position.z + 1.5f);
        //ボムを生成
        GameObject projectile = Instantiate(BOMM_PREFAB, vector, SHOT_POINT.rotation);

        // Rigidbodyを取得
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // 発射方向を計算（例: 前方方向）
        Vector3 launchDirection = SHOT_POINT.forward;

        // 初速度を与えて発射
        rb.velocity = launchDirection * BOM_SPEED;
    }
}
