using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// アイテム「バクダン」を使用したときの効果
/// </summary>
public class BommEffect : MonoBehaviour, IItemEffect
{
    [SerializeField] private GameObject BOMM_PREFAB; 
    [SerializeField] private float BOM_SPEED;//ボムの初速度
    [SerializeField] private string SE_NAME; // ボムを投げるSEの名前
    private GameObject player; //プレイヤータグのオブジェクトを探す
    public void ApplyEffect() // バクダン生成処理の実装
    {
        ShootBom(); // ShotBomメソッドを呼び出す
    }
    void ShootBom()//ボムの発射スクリプト
    {
        player = GameObject.FindWithTag("Player");

        if (SE_NAME != null)
        {
            SoundManager.Instance.PlaySE(SE_NAME); // SEを再生
        }
        Vector3 vector = new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z + 1.5f);
        //ボムを生成
        GameObject projectile = Instantiate(BOMM_PREFAB, vector, player.transform.rotation);

        // Rigidbodyを取得
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // 発射方向を計算（例: 前方方向）
        Vector3 launchDirection = player.transform.forward;

        // 初速度を与えて発射
        rb.velocity = launchDirection * BOM_SPEED;
    }
}
