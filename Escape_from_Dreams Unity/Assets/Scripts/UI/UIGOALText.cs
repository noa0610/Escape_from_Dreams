using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// ゴール時にUIを表示するクラス
/// </summary>
public class UIGOALText : MonoBehaviour
{
    // インスペクターから設定する変数
    [SerializeField] private GameObject GOAL_TEXT_OBJECT; // テキストオブジェクト
    [SerializeField] private GameObject GOAL_WINDOW_OBJECT; // ウィンドウオブジェクト
    [SerializeField] private Image GOAL_BUTTON_IMAGE; // ウィンドウのボタンオブジェクト
    [SerializeField] private TextMeshProUGUI GOAL_BUTTON_TEXT; // ボタンのテキスト
    [SerializeField] private TextMeshProUGUI CLEARTIME_TEXT; // クリアタイム表示のテキスト
    [SerializeField] private GameObject TIMEROBJECT; // 時間を計測するスクリプトのオブジェクト
    [SerializeField] private float GOAL_WINDOW_ACTUVE_TIME = 2; // ウィンドウ表示までの時間
    [SerializeField] private float GOAL_FADE_TIME = 1; // フェード効果を反映させる時間


    // 内部処理する変数
    private UITimerText _uITimerText; // 時間計測タイマー参照用

    private void Start()
    {
        // 初期化の際、設定忘れ防止でオブジェクトを非表示にする
        GOAL_TEXT_OBJECT.SetActive(false);
        GOAL_WINDOW_OBJECT.SetActive(false);

        StartCoroutine(GOALActive()); // ゴールまで待機するコルーチン
    }

    // ゴールを確認したらUIを表示
    private IEnumerator GOALActive()
    {
        while (!GameManager.Instance.IsGOAL) // ゴールするまで繰り返す
        {
            yield return null; // 次のフレームまで待機
        }

        // 最初にゴールのテキストを表示
        GOAL_TEXT_OBJECT.SetActive(true);

        // ウィンドウ表示までの時間待機
        yield return new WaitForSeconds(GOAL_WINDOW_ACTUVE_TIME);

        // ウィンドウを表示
        Image image = GOAL_WINDOW_OBJECT.GetComponent<Image>();
        StartCoroutine(UIFade.FadeIn(image, GOAL_FADE_TIME));
        GOAL_WINDOW_OBJECT.SetActive(true);

        // ボタンの画像を表示
        StartCoroutine(UIFade.FadeIn(GOAL_BUTTON_IMAGE, GOAL_FADE_TIME));


        // ボタンのテキストを表示
        StartCoroutine(UIFade.FadeIn(GOAL_BUTTON_TEXT, GOAL_FADE_TIME));


        // クリアタイムのテキストを表示
        if (CLEARTIME_TEXT != null && TIMEROBJECT != null)
        {
            _uITimerText = TIMEROBJECT.GetComponent<UITimerText>();
            if (_uITimerText != null)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(_uITimerText.CountTimer);
                CLEARTIME_TEXT.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
                StartCoroutine(UIFade.FadeIn(CLEARTIME_TEXT, 1));
            }
        }

        // 最初のゴールのテキストを非表示
        GOAL_TEXT_OBJECT.SetActive(false);

        // フェードインが終わるまでの時間待機
        yield return new WaitForSeconds(GOAL_FADE_TIME);

        Time.timeScale = 0f;
    }
}
