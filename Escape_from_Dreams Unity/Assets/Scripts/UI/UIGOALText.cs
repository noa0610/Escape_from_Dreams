using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGOALText : MonoBehaviour
{
    [SerializeField] private GameObject GOAL_TEXT_OBJECT; // テキストオブジェクト
    [SerializeField] private GameObject GOAL_WINDOW_OBJECT; // ウィンドウオブジェクト
    [SerializeField] private Image GOAL_BUTTON_IMAGE; // ウィンドウのボタンオブジェクト
    [SerializeField] private Graphic TEXT;
    [SerializeField] private float GOAL_WINDOW_ACTUVE_TIME; // 画像表示までの時間

    private void Start()
    {
        // 初期化の際、設定忘れ防止でオブジェクトを非表示にする
        GOAL_TEXT_OBJECT.SetActive(false);
        GOAL_WINDOW_OBJECT.SetActive(false);

        StartCoroutine(GOALActive()); // ゴールまで待機するコルーチン
    }

    // ゴールを確認したらテキストとウィンドウを表示
    private IEnumerator GOALActive()
    {
        while (!GameManager.Instance.IsGOAL) // ゴールするまで繰り返す
        {
            yield return null; // 次のフレームまで待機
        }

        GOAL_TEXT_OBJECT.SetActive(true); // テキストを表示

        yield return new WaitForSeconds(GOAL_WINDOW_ACTUVE_TIME); // 指定の時間待機

        Image image = GOAL_WINDOW_OBJECT.GetComponent<Image>();
        StartCoroutine(UIFade.FadeIn(image,1));
        GOAL_WINDOW_OBJECT.SetActive(true); // ウィンドウは遅れて表示

        GOAL_BUTTON_IMAGE = GetComponent<Image>();
        StartCoroutine(UIFade.FadeIn(GOAL_BUTTON_IMAGE,1));

        TEXT = GetComponent<TextMeshProUGUI>();
        StartCoroutine(UIFade.FadeIn(TEXT,1));


        
        GOAL_TEXT_OBJECT.SetActive(false);  // テキストを非表示
    }

    IEnumerator Color_FadeOut(GameObject gameObject) // Webで借りたコルーチン
    {
        // 画面をフェードインさせるコールチン
        // 前提：画面を覆うPanelにアタッチしている

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = gameObject.GetComponent<Image>();

        // フェード後の色を設定（黒）★変更可
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, (0.0f / 255.0f));

        // フェードインにかかる時間（秒）★変更可
        const float fade_time = 0.5f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 10;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
    }
}
