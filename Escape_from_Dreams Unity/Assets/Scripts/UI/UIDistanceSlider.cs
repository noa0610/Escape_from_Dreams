using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーとゴールまでの距離を反映させるSlider
/// </summary>
public class UIDistanceSlider : MonoBehaviour
{
    // インスペクターから設定する変数

    // 内部処理する変数
    private Slider _slider;          // スライダー
    private GameObject _player;      // プレイヤーのオブジェクト
    private GameObject _goalPoint;   // ゴールのオブジェクト
    private float direction_ZMax;    // プレイヤーとゴールの初期の距離
    private float direction_Z;       // プレイヤーとゴールの距離
    void Start()
    {
        _slider = GetComponent<Slider>(); // スライダー取得
        _player = GameObject.FindGameObjectWithTag("Player"); // プレイヤーを探す
        _goalPoint = GameObject.FindGameObjectWithTag("GOALPoint"); // ゴールを探す
        if (_player == null)
        {
            Debug.Log($"プレイヤーが見つかりません{_player}");
        }
        if (_goalPoint == null)
        {
            Debug.Log($"ゴールが見つかりません{_player}");
        }
        direction_ZMax = GetDistance(); // プレイヤーとゴールの距離を計算
        _slider.maxValue = direction_ZMax; // 現在の距離をスライダーの最大値として設定
        _slider.value = 0; // スライダーの初期値を0にする
    }

    void Update()
    {
        Debug.Log($"GameManager.Instance.IsGOAL:{GameManager.Instance.IsGOAL}");
        if (GameManager.Instance.IsGOAL == false)
        {
            direction_Z = GetDistance(); // プレイヤーとゴールの距離を計算
            _slider.value = direction_ZMax - direction_Z; // スライダーに進んだ距離を反映
        }
    }

    // プレイヤーとゴールの距離を計算するメソッド
    private float GetDistance()
    {
        Vector3 playerPos = _player.transform.position; // プレイヤーの位置を取得
        Vector3 goalPointPos = _goalPoint.transform.position; // ゴールの位置を取得
        return Mathf.Abs(playerPos.z - goalPointPos.z); // プレイヤーとゴールの奥行の距離を計算
    }
}
