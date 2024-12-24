using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITimerText : MonoBehaviour
{
    // インスペクターから設定する変数
    [SerializeField] private GameObject OBJECT;
    [SerializeField] private GameObject TIMER_OBJECT;

    // 内部処理する変数
    private float countTimer; // 計測時間
    private TextMeshProUGUI TIMER_TEXT; // 計測時間を反映するテキスト
    private bool isTimerInitialized = false;
    private Coroutine setActiveFalseCoroutine;

    // プロパティ
    public float CountTimer
    {
        get { return countTimer; }
    }

    public bool IsTimerInitialized
    {
        get { return isTimerInitialized; }
    }

    private void Start()
    {
        StartCoroutine(SetActiveTrueTimer());
    }

    private void Update()
    {
        if (GameManager.Instance.IsGame)
        {
            if (!GameManager.Instance.IsGOAL && !GameManager.Instance.IsGameOver)
            {
                if (!isTimerInitialized) return; // 初期化が完了するまでUpdateTimerを呼ばない

                countTimer += Time.deltaTime; // 時間を計測する
                UpdateTimer(); // テキストに計測時間を反映
            }
        }
    }

    public void UpdateTimer()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(countTimer);
        TIMER_TEXT.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }

    private IEnumerator SetActiveTrueTimer()
    {
        // Debug.Log("UITimerText:SetActiveTrueTimer started コルーチン呼び出し");
        while (!GameManager.Instance.IsGame) // ゲームが開始されるまで待機
        {
            // Debug.Log("UITimerText:Waiting for game start... ゲーム開始待ち");
            yield return null;
        }

        OBJECT.SetActive(true); // オブジェクトをアクティブ化
        // Debug.Log("UITimerText;OBJECT activated. オブジェクトアクティブ化");
        TIMER_TEXT = TIMER_OBJECT.GetComponent<TextMeshProUGUI>(); // テキストを取得

        if (TIMER_TEXT == null)
        {
            yield break;
        }

        isTimerInitialized = true;
        yield return StartCoroutine(SetActiveFalseTimer());
    }

    private IEnumerator SetActiveFalseTimer()
    {
        // Debug.Log("UITimerText:SetActiveFalseTimer コルーチン呼び出し");

        while (GameManager.Instance.IsGame) // ゲームが終了するまで待機
        {
            // Debug.Log($"UITimerText:Waiting for game end (SetActiveFalseTimer): {GameManager.Instance.IsGame} ゲーム終了待ち");
            yield return null;
        }

        OBJECT.SetActive(false); // オブジェクトを非アクティブ化
        // Debug.Log($"UITimerText:OBJECT deactivated.");
    }

    private void StartSetActiveFalseTimer() // 同じ名前のコルーチン呼び出しを検知
    {
        if (setActiveFalseCoroutine != null)
        {
            StopCoroutine(setActiveFalseCoroutine); // 動作中のコルーチンを停止
        }
        setActiveFalseCoroutine = StartCoroutine(SetActiveFalseTimer());
    }

    private void OnEnable()
    {
        GameManager.Instance.OnIsGameChanged += HandleIsGameChanged;

        if (GameManager.Instance.IsGame)
        {
            StartCoroutine(SetActiveTrueTimer());
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.OnIsGameChanged -= HandleIsGameChanged;
    }

    private void HandleIsGameChanged(bool isGame)
    {
        if (!isGame)
        {
            StartCoroutine(SetActiveFalseTimer());
        }
        else
        {
            StartCoroutine(SetActiveFalseTimer());
        }
    }
}
