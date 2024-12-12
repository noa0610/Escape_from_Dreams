using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// アイテム「ダイナマイト」を使用したときの効果
/// </summary>
public class DynamaiteEffect : MonoBehaviour, IItemEffect
{
   [SerializeField] private Transform SHOT_POINT;//発射する位置と向き
    [SerializeField] private GameObject DYNAMAITE_PREFAB; 
    [SerializeField] private float DYNAMAITE_SPEED;//ダイナマイトの初速度
    public void ApplyEffect()
    {
        ShootDinamit();// ダイナマイトを発射するスクリプトを呼び出す
    }
    void ShootDinamit()//発射するスクリプト
    {
        Vector3 vector = new Vector3(SHOT_POINT.position.x,SHOT_POINT.position.y+1,SHOT_POINT.position.z + 1.5f);//生成する位置を取得
        //ボムを生成
        GameObject projectile = Instantiate(DYNAMAITE_PREFAB, vector, SHOT_POINT.rotation);

        // Rigidbodyを取得
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // 発射方向を計算（例: 前方方向）
        Vector3 launchDirection = SHOT_POINT.forward;

        // 初速度を与えて発射
        rb.velocity = launchDirection * DYNAMAITE_SPEED;
    }
}
