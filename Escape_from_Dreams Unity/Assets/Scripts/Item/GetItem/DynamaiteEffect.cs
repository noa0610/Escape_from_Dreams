using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// アイテム「ダイナマイト」を使用したときの効果
/// </summary>
public class DynamaiteEffect : MonoBehaviour, IItemEffect
{
   private GameObject player;
    [SerializeField] private GameObject DYNAMAITE_PREFAB; 
    [SerializeField] private float DYNAMAITE_SPEED;//ダイナマイトの初速度
    [SerializeField] private string SE_NAME; // ダイナマイトを投げるSEの名前
    public void ApplyEffect()
    {
        ShootDinamit();// ダイナマイトを発射するスクリプトを呼び出す
    }
    void ShootDinamit()//発射するスクリプト
    {
        player = GameObject.FindWithTag("Player");

        if (SE_NAME != null)
        {
            SoundManager.Instance.PlaySE(SE_NAME); // SEを再生
        }
        Vector3 vector = new Vector3(player.transform.position.x,player.transform.position.y+1,player.transform.position.z + 1.5f);//生成する位置を取得
        //ボムを生成
        GameObject projectile = Instantiate(DYNAMAITE_PREFAB, vector, player.transform.rotation);

        // Rigidbodyを取得
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // 発射方向を計算（例: 前方方向）
        Vector3 launchDirection = player.transform.forward;

        // 初速度を与えて発射
        rb.velocity = launchDirection * DYNAMAITE_SPEED;
    }
}
