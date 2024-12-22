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
    [SerializeField] private GameObject GAMEOVER_TEXT_OBJECT; // テキストをまとめた親オブジェクト
    [SerializeField] private GameObject GAMEOVER_WINDOU_OBJECT; // ウィンドウをまとめた親オブジェクト
    [SerializeField] private Image GAMEOVER_BUTTON_IMAGE; // ボタンの画像
    [SerializeField] private TextMeshProUGUI GAMEOVER_TEXT; // ボタンのテキスト
    [SerializeField] private float GAMEOVER_WINDOW_ACTUVE_TIME = 2; // ウィンドウを表示するまでの時間
    [SerializeField] private float GAMEOVER_FADE_TIME = 1; // フェード効果を反映させる時間

    // 内部処理する変数
    

    private void Start()
    {
        // 初期化の際、設定忘れ防止でオブジェクトを非表示にする
        GAMEOVER_TEXT_OBJECT.SetActive(false);
        GAMEOVER_WINDOU_OBJECT.SetActive(false);
    }

    private IEnumerator GameOverUIActive()
    {
        while (!GameManager.Instance.IsGameOver) // ゲームオーバーまで繰り返す
        {
            yield return null; // 次のフレームまで待機
        }
        // 最初にゲームオーバーのテキストを表示
        GAMEOVER_TEXT_OBJECT.SetActive(true);

        // ウィンドウ表示までの時間待機
        yield return new WaitForSeconds(GAMEOVER_WINDOW_ACTUVE_TIME);

        // ウィンドウを表示
        Image image = GAMEOVER_WINDOU_OBJECT.GetComponent<Image>();
        StartCoroutine(UIFade.FadeIn(image, GAMEOVER_FADE_TIME));
        GAMEOVER_WINDOU_OBJECT.SetActive(true);

        // ボタンの画像を表示
        StartCoroutine(UIFade.FadeIn(GAMEOVER_BUTTON_IMAGE, 1));

        // ボタンのテキストを表示
        StartCoroutine(UIFade.FadeIn(GAMEOVER_TEXT, 1));

        // 最初のゲームオーバーのテキストを非表示
        GAMEOVER_TEXT_OBJECT.SetActive(false);
    }
}
