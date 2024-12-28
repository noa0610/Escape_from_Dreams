using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStartText : MonoBehaviour
{
    // インスペクターから設定する変数
    [Header("アクティブ状態の切り替えをするUIオブジェクト")]
    [SerializeField] private GameObject START_FIRST_OBJECT;  // 最初に表示するオブジェクト
    [SerializeField] private GameObject START_NEXT_OBJECT;   // 最後に表示する画像

    [Header("スタートUI表示時間")]
    [SerializeField] private float START_COUNTDOUN_TIME;    // テキスト表示終了までの時間

    [Header("2つ目のUIへの表示切替時間")]
    [SerializeField] private float START_TEXT_CHANGE_TIME;  // 表示を変更する時間

    [Header("スタート時のSE")]
    [SerializeField] private string SE_NAME; // SEの名前


    // 内部処理する変数
    private float countdounTime;
    private bool isSE;
    void Awake()
    {
        GameManager.Instance.IsGame = false;
        countdounTime = START_COUNTDOUN_TIME;
        isSE = false;
        START_FIRST_OBJECT.SetActive(true); // テキストオブジェクトを表示
    }

    void Update()
    {
        countdounTime -= Time.deltaTime; // 経過時間を測る

        // 時間に応じてテキストを変化させる
        if (countdounTime <= START_TEXT_CHANGE_TIME && GameManager.Instance.IsGame == false)
        {
            START_FIRST_OBJECT.SetActive(false); // テキストオブジェクトを非表示
            START_NEXT_OBJECT.SetActive(true); // 画像を表示する
            if (SE_NAME != null && isSE == false)
            {
                SoundManager.Instance.PlaySE(SE_NAME); // SEを再生
                isSE = true;
            }
        }

        if (countdounTime <= 0)
        {
            GameManager.Instance.IsGame = true; // ゲーム開始のフラグを立てる
            START_NEXT_OBJECT.SetActive(false); // 画像を非表示にする
            return;
        }
    }
}
