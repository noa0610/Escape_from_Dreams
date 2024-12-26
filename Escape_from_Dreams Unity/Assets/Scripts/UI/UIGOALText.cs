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
    [Header("アクティブ状態の切り替えをするオブジェクト")]
    [SerializeField] private GameObject GOAL_TEXT_OBJECT; // テキストオブジェクト
    [SerializeField] private GameObject GOAL_WINDOW_OBJECT; // ウィンドウオブジェクト

    [Header("ゴール直後にフェードイン表示するUI")]
    [SerializeField] private Graphic[] GOAL_FIRST_Graphics; // ゴール直後に表示するUI

    [Header("指定時間後にフェードイン表示するUI")]
    [SerializeField] private Graphic[] GOAL_NEXT_Graphics; // 指定時間後に表示するUI

    [Header("クリアタイム表示")]
    [SerializeField] private TextMeshProUGUI CLEARTIME_TEXT; // クリアタイム表示のテキスト
    [SerializeField] private GameObject TIMEROBJECT; // 時間を計測するスクリプトのオブジェクト

    [Header("表示設定")]
    [SerializeField] private float WINDOW_ACTUVE_TIME = 2; // ウィンドウ表示までの時間
    [SerializeField] private float FADE_TIME = 1; // フェード効果を反映させる時間

    // [Header("ゴール時のSE")]
    // [SerializeField] private string SE_NAME; // SEの名前


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

        // ゴールした瞬間最初にゴールのテキストを表示
        GOAL_TEXT_OBJECT.SetActive(true);
        if (GOAL_FIRST_Graphics != null)
        {
            FadeInGraphics(GOAL_FIRST_Graphics, FADE_TIME);
        }

        // if (SE_NAME != null)
        // {
        //     SoundManager.Instance.PlaySE(SE_NAME); // SEを再生
        // }

        // ウィンドウ表示までの時間待機
        yield return new WaitForSeconds(WINDOW_ACTUVE_TIME);

        // 最初のゴールのテキストを非表示
        GOAL_TEXT_OBJECT.SetActive(false);

        // ゴール後に表示するUIをフェードイン
        GOAL_WINDOW_OBJECT.SetActive(true);
        if (GOAL_NEXT_Graphics != null)
        {
            FadeInGraphics(GOAL_NEXT_Graphics, FADE_TIME);
        }


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

        // フェードインが終わるまでの時間待機
        yield return new WaitForSeconds(FADE_TIME);

        // ゲーム時間を停止
        Time.timeScale = 0f;
    }

    // フェードイン処理を一括適用
    private void FadeInGraphics(Graphic[] graphics, float duration)
    {
        foreach (var graphic in graphics)
        {
            if (graphic != null)
            {
                StartCoroutine(UIFade.FadeIn(graphic, duration));
            }
        }
    }
}
