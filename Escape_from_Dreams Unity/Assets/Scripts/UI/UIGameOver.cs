using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームオーバー時にUIを表示するクラス
/// </summary>
public class UIGameOver : MonoBehaviour
{
    // インスペクターから設定する変数
    [Header("アクティブ状態の切り替えをするオブジェクト")]
    [SerializeField] private GameObject GAMEOVER_TEXT_OBJECT; // テキストオブジェクト
    [SerializeField] private GameObject GAMEOVER_WINDOU_OBJECT; // ウィンドウオブジェクト

    [Header("ゲームオーバー直後にフェードイン表示するUI")]
    [SerializeField] private Graphic[] GAMEOVER_FIRST_Graphics; // ゲームオーバー直後に表示するUI

    [Header("指定時間後にフェードイン表示するUI")]
    [SerializeField] private Graphic[] GAMEOVER_NEXT_Graphics; // 指定時間後に表示するUI

    [Header("表示設定")]
    [SerializeField] private float WINDOW_ACTUVE_TIME = 2; // ウィンドウを表示するまでの時間
    [SerializeField] private float FADE_TIME = 1; // フェード効果を反映させる時間

    // 内部処理する変数
    

    private void Start()
    {
        Time.timeScale = 1f;
        GameManager.Instance.IsGameOver = false;

        // 初期化の際、設定忘れ防止でオブジェクトを非表示にする
        GAMEOVER_TEXT_OBJECT.SetActive(false);
        GAMEOVER_WINDOU_OBJECT.SetActive(false);

        StartCoroutine(GameOverUIActive());
    }

    private IEnumerator GameOverUIActive()
    {
        while (!GameManager.Instance.IsGameOver) // ゲームオーバーまで繰り返す
        {
            yield return null; // 次のフレームまで待機
        }

        // ゲームオーバー直後にテキストを表示
        GAMEOVER_TEXT_OBJECT.SetActive(true);
        if (GAMEOVER_FIRST_Graphics != null)
        {
            FadeInGraphics(GAMEOVER_FIRST_Graphics, FADE_TIME);
        }

        // ウィンドウ表示までの時間待機
        yield return new WaitForSeconds(WINDOW_ACTUVE_TIME);

        // 最初のゲームオーバーのテキストを非表示
        GAMEOVER_TEXT_OBJECT.SetActive(false);

        // ゴール後に表示するUIをフェードイン
        GAMEOVER_WINDOU_OBJECT.SetActive(true);
        if (GAMEOVER_NEXT_Graphics != null)
        {
            FadeInGraphics(GAMEOVER_NEXT_Graphics, FADE_TIME);
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
