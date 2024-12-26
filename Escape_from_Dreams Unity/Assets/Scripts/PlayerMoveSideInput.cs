using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveSideInput : MonoBehaviour
{
    // /// <summary>
    // /// 左右移動ができるようにする処理
    // /// </summary>


    // [SerializeField] private float MoveSideSpeed; // 左右移動の速度を設定する
    // private KetboardControls controls; // InputAction(InputSystemのクラス)を参照する
    // private Vector2 moveInput; // 現在の移動入力値を保持する変数

    // void Awake()
    // {
    //     controls = new KetboardControls(); // KetboardControlsクラスのインスタンスを作成

    //     // Moveアクションの「performed」イベントにコールバックを登録
    //     // 入力が行われたとき、その値（Vector2）をmoveInputに代入
    //     controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();

    //     // Moveアクションの「canceled」イベントにコールバックを登録
    //     // 入力がキャンセルされたとき、moveInputを(0, 0)にリセット
    //     controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    // }

    // /// <summary>
    // /// OnEnableはスクリプトが有効化されたときに呼ばれるメソッド。
    // /// Input Systemのアクションマップを有効にします。
    // /// </summary>
    // private void OnEnable()
    // {
    //     controls.Player.Enable(); // アクションマップを有効化
    // }

    // /// <summary>
    // /// OnDisableはスクリプトが無効化されたときに呼ばれるメソッド。
    // /// Input Systemのアクションマップを無効にします。
    // /// </summary>
    // private void OnDisable()
    // {
    //     controls.Player.Disable(); // アクションマップを無効化
    // }

    // private void Update()
    // {
    //     // moveInput.xはAキー（左）で-1、Dキー（右）で1、それ以外で0となる
    //     // 入力に基づいて移動量を計算
    //     Vector3 move = new Vector3(moveInput.x, 0f, 0f) * MoveSideSpeed * Time.deltaTime;

    //     // 現在の位置に移動量を加算して、プレイヤーを移動させる
    //     transform.position += move;
    // }
}
