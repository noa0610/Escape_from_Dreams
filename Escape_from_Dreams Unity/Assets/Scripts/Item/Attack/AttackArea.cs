using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // インスペクターから設定する変数
    [SerializeField] private float DURATION = 1f;   // 持続時間
    [SerializeField] private int DAMAGE = 10;       // ダメージ量

    [SerializeField] private string SE_FIRST_NAME;  // 最初に再生するSEの名前
    [SerializeField] private string SE_ATTACK_NAME; // 攻撃命中時のSE

    // プロパティ
    public float Duration
    {
        set { DURATION = value; }
        get { return DURATION; }
    }
    public int Damage
    {
        set { DAMAGE = value; }
        get { return DAMAGE; }
    }
    private void Start()
    {
        if (SE_FIRST_NAME != null)
        {
            Debug.Log("FIRST_SE");
            SoundManager.Instance.PlaySE(SE_FIRST_NAME); // SEを再生
        }
        Destroy(gameObject, DURATION); // 一定時間後に自身を破壊
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) // 触れたのが障害物なら
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (SE_ATTACK_NAME != null)
                {
                    SoundManager.Instance.PlaySE(SE_ATTACK_NAME); // SEを再生
                }
                enemy.TakeDamage(DAMAGE); // 障害物にダメージを与える
            }
        }
    }
}