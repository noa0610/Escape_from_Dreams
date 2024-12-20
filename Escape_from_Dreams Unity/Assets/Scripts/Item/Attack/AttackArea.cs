using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // インスペクターから設定する変数
    [SerializeField] private float DURATION = 1f;     // 持続時間
    [SerializeField] private int DAMAGE = 10;         // ダメージ量
    [SerializeField] private ParticleSystem particle; // 

    // プロパティ
    public float Duration
    {
        set{DURATION = value;}
        get{return DURATION;}
    }
    public int Damage
    {
        set{DAMAGE = value;}
        get{return DAMAGE;}
    }
    private void Start()
    {
        Destroy(gameObject, DURATION); // 一定時間後に自身を破壊
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) // 触れたのが障害物なら
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(DAMAGE); // 障害物にダメージを与える
            }
        }
    }
}