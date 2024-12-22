using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStartText : MonoBehaviour
{
    // インスペクターから設定する変数
    [SerializeField] private GameObject START_TEXT_OBJECT;  // テキストを変更するオブジェクト
    [SerializeField] private float START_COUNTDOUN_TIME;    // テキスト表示終了までの時間
    [SerializeField] private float START_TEXT_CHANGE_TIME;  // テキストを変更する時間
    [SerializeField] private string CHANGE_TEXT;            // 変更後のテキスト

    // 内部処理する変数
    private float countdounTime;
    private TextMeshProUGUI _textMeshPro;
    void Awake()
    {
        GameManager.Instance.IsGame = false;
        countdounTime = START_COUNTDOUN_TIME;
        START_TEXT_OBJECT.SetActive(true); // テキストオブジェクトを表示
        _textMeshPro = START_TEXT_OBJECT.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        countdounTime -= Time.deltaTime; // 経過時間を測る

        // 時間に応じてテキストを変化させる
        if(countdounTime <= START_TEXT_CHANGE_TIME && GameManager.Instance.IsGame == false)
        {
            _textMeshPro.text = string.Format(CHANGE_TEXT); // テキストを変更する
        }

        if(countdounTime <= 0)
        {
            GameManager.Instance.IsGame = true; // ゲーム開始のフラグを立てる
            START_TEXT_OBJECT.SetActive(false); // テキストオブジェクトを非表示
            return;
        }
    }
}
