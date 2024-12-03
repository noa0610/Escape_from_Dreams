using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの移動処理
/// </summary>
public class PlayerMove : MonoBehaviour
{
    // インスペクターから設定する変数
    [SerializeField] private float FORWARD_MOVE_SPEED = 5f; // 前に進む加速度
    [SerializeField] private float FORWARD_MOVE_SPEED_MAX = 10f; // 前に進む最大速度

    [SerializeField] private float SIDE_MOVE_SPEED = 8f; // 横移動の加速度
    [SerializeField] private float SIDE_MOVE_SPEED_MAX = 10f; // 横移動の最大速度

    // 内部処理する変数
    private Rigidbody _rigidbody; // リジッドボディ
    private CharacterAnime _characterAnime; // キャラクターアニメーション

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); // リジッドボディ取得
        _characterAnime = gameObject.AddComponent<CharacterAnime>(); // "CharacterAnime"をオブジェクトに追加
    }

    void FixedUpdate()
    {
        // 常に前に進む
        ForwardMove();

        // 左右移動入力
        if (Input.GetKey(KeyCode.A))
        {
            SideMoveVelocity(Vector3.left);
            // SideMoveAddForce(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SideMoveVelocity(Vector3.right);
            // SideMoveAddForce(Vector3.right);
        }
        else
        {
            SideMoveVelocity(Vector3.zero);
            // SideMoveAddForce(Vector3.zero);
        }

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
            currentVelocity.z = Mathf.Sign(currentVelocity.z) * FORWARD_MOVE_SPEED_MAX;
        }

        // Rigidbodyの速度を更新
        _rigidbody.velocity = new Vector3(currentVelocity.x, currentVelocity.y, currentVelocity.z);
    }

    // 前方移動
    private void ForwardMove()
    {
        _rigidbody.AddForce(Vector3.forward * FORWARD_MOVE_SPEED, ForceMode.Acceleration);
    }

    // 左右移動(AddForce)
    private void SideMoveAddForce(Vector3 direction)
    {
        _rigidbody.AddForce(Vector3.right * direction.x * SIDE_MOVE_SPEED, ForceMode.Acceleration);
    }

    // 左右移動(velocity変換)
    private void SideMoveVelocity(Vector3 direction)
    {
        _rigidbody.velocity = new Vector3(direction.x * SIDE_MOVE_SPEED, _rigidbody.velocity.y, _rigidbody.velocity.z);
        _characterAnime.Animator.SetFloat("SideMove",_rigidbody.velocity.x); // ブレンドツリーに速度を反映
    }
}