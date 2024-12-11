using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの移動処理
/// </summary>
public class PlayerMove : MonoBehaviour
{
    // インスペクターから設定する変数
    [Header("前方移動ステータス")]
    [SerializeField] private float FORWARD_MOVE_SPEED = 5f; // 前に進む加速度
    [SerializeField] private float FORWARD_MOVE_SPEED_MAX = 10f; // 前に進む最大速度

    [Header("左右移動ステータス")]
    [SerializeField] private float SIDE_MOVE_SPEED = 8f; // 横移動の加速度
    [SerializeField] private float SIDE_MOVE_SPEED_MAX = 10f; // 横移動の最大速度

    [Header("ノックバック効果量")]
    [SerializeField] private float KNOCKBACK_FORSE = 10f; // ノックバック力
    [SerializeField] private float KNOCKBACK_TIME = 0.3f; // ノックバック時間

    // 内部処理する変数
    private bool isKnockedBack; // ノックバック中のフラグ
    private Rigidbody _rigidbody; // リジッドボディ
    private CharacterAnime _characterAnime; // キャラクターアニメーション

    private List<SpeedUpEffect> activeEffects = new List<SpeedUpEffect>();
    private float speedBoost = 1f;          // 速度増加倍率
    private GameObject attackAreaPrefab;    // 攻撃判定プレハブ
    private GameObject activeAttackArea;
    private bool canAttack = false;         // 攻撃可能状態か

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); // リジッドボディ取得
        _characterAnime = gameObject.AddComponent<CharacterAnime>(); // "CharacterAnime"をオブジェクトに追加
    }

    private void FixedUpdate()
    {
        // 常に前に進む
        ForwardMove();

        ApplyStatusEffects(); // アイテムの効果を確認

        if (isKnockedBack == false) // ノックバックが発生していなければ
        {
            SideMoveInput(); // 左右移動入力
        }

        MoveSpeedLimit(); // 移動速度制限
    }



    // 前方移動
    private void ForwardMove()
    {
        _rigidbody.AddForce(Vector3.forward * FORWARD_MOVE_SPEED * speedBoost, ForceMode.Acceleration);
    }

    // 前方移動速度リセット
    public void ForwardMoveReset()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, 0); // 前方移動速度をリセット
    }


    // 左右移動入力
    private void SideMoveInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            SideMoveVelocity(Vector3.left); // 左に移動
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SideMoveVelocity(Vector3.right); // 右に移動
        }
        else
        {
            SideMoveVelocity(Vector3.zero); // 移動しない
        }
    }

    // 左右移動(velocity変換)
    private void SideMoveVelocity(Vector3 direction)
    {
        _rigidbody.velocity = new Vector3(direction.x * SIDE_MOVE_SPEED, _rigidbody.velocity.y, _rigidbody.velocity.z);
        _characterAnime.Animator.SetFloat("SideMove", _rigidbody.velocity.x); // ブレンドツリーに速度を反映
    }

    // 速度制限
    private void MoveSpeedLimit()
    {
        // 現在の速度を取得
        Vector3 currentVelocity = _rigidbody.velocity;

        // X軸速度制限
        if (Mathf.Abs(currentVelocity.x) > SIDE_MOVE_SPEED_MAX)
        {
            currentVelocity.x = Mathf.Sign(currentVelocity.x) * SIDE_MOVE_SPEED_MAX;
        }

        // Z軸速度制限
        if (Mathf.Abs(currentVelocity.z) > FORWARD_MOVE_SPEED_MAX)
        {
            currentVelocity.z = Mathf.Sign(currentVelocity.z) * FORWARD_MOVE_SPEED_MAX * speedBoost;
        }

        // Rigidbodyの速度を更新
        _rigidbody.velocity = new Vector3(currentVelocity.x, currentVelocity.y, currentVelocity.z);
    }

    // 命中したオブジェクトから離れるノックバック
    public void Knockback(Vector3 hitPosition)
    {
        Debug.Log("KnockBack");
        // ノックバック方向を計算
        Vector3 knockbackDirection = (transform.position - hitPosition).normalized;

        // Y軸とZ軸の影響を排除（X軸のみに限定）
        knockbackDirection.y = 0;
        knockbackDirection.z = 0;
        knockbackDirection = knockbackDirection.normalized; // 再度正規化

        // ノックバックの力を加える
        _rigidbody.AddForce(knockbackDirection * KNOCKBACK_FORSE, ForceMode.Impulse);

        // ノックバック中のフラグを設定
        isKnockedBack = true;

        // ノックバックの終了を遅れて実行
        Invoke(nameof(ResetKnockback), KNOCKBACK_TIME);
    }

    // ノックバックの終了
    private void ResetKnockback()
    {
        isKnockedBack = false;
    }


    // 速度上昇効果
    private void ApplyStatusEffects()
    {
        // 有効な効果を適用
        activeEffects.RemoveAll(effect => effect.IsExpired()); // 効果が終了していれば削除

        if (activeEffects.Count == 0)
        {
            Destroy(activeAttackArea); // 効果が終了しているなら攻撃判定を削除
            speedBoost = 1f;          // 速度倍率を元に戻す
        }
        else
        {
            foreach (var effect in activeEffects)
            {
                speedBoost = effect.speedBoost; // 速度上昇効果を取得
                if (effect.enableAttack) // 効果に攻撃判定が含まれるなら
                {
                    UseAttack(); // 攻撃を有効化
                }
            }
        }
    }

    public void AddStatusEffect(SpeedUpEffect effect)
    {
        activeEffects.Add(effect); // ステータス効果を追加
    }

    // 攻撃判定プレハブを設定
    public void SetAttackAreaPrefab(GameObject prefab)
    {
        attackAreaPrefab = prefab;
    }

    // 攻撃判定の生成
    private void UseAttack()
    {
        if (attackAreaPrefab != null)
        {
            if(activeAttackArea != null) // 攻撃判定が存在しているなら
            {
                Destroy(activeAttackArea); // 古い攻撃判定を削除
            }

            // 攻撃判定オブジェクトを生成し、プレイヤーの子オブジェクトとして設定
            activeAttackArea = Instantiate(attackAreaPrefab, transform.position, Quaternion.identity);
            activeAttackArea.transform.SetParent(transform); // プレイヤーの子に設定
            attackAreaPrefab = null;
        }
    }
}