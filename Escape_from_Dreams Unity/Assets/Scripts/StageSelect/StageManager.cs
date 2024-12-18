using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの進行状態管理をするシングルトン
/// </summary>
public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    private List<bool> stageUnlocked; // ステージ解放状況のリスト

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンロード後も保持
        }
        else
        {
            Destroy(gameObject); // 同じものがあれば消去
        }
    }

    private void Start()
    {
        LoadStageData(); // ステージの解放状態をロード
    }

    // 解放されているステージを取得できる
    public bool IsStageUnlocked(int stageIndex)
    {
        if (stageUnlocked == null || stageIndex >= stageUnlocked.Count)
        {
            Debug.LogError("stageUnlocked is not initialized or index is out of range!");
            return false; // エラー回避
        }
        return stageUnlocked[stageIndex]; // ステージが解放されているか判定
    }

    public void UnlockStage(int stageIndex) // ステージを解放する
    {
        if (stageIndex < stageUnlocked.Count && !stageUnlocked[stageIndex])
        {
            stageUnlocked[stageIndex] = true; // ステージを解放
            SaveStageData(); // 解放状況をセーブ
        }
    }

    private void LoadStageData()
    {
        if (stageUnlocked == null) // nullチェックを追加
        {
            // セーブデータからロード（例: PlayerPrefs）
            stageUnlocked = new List<bool> { true, true, false, false }; // 初期状態（ステージ1-1のみ解放）
            // PlayerPrefsからロードするロジックを追加
        }
    }

    private void SaveStageData()
    {
        // セーブデータの保存（例: PlayerPrefs）
        // PlayerPrefsに保存するロジックを追加
    }
}
