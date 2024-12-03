using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// ゲームのフラグを管理するシングルトン
    /// </summary>
    public static GameManager Instance; // シングルトンインスタンスを保持

    // 内部処理する変数
    private bool isGame = false; // ゲーム中フラグ
    private bool isGOAL = false; // ゴールフラグ

    // プロパティ
    public bool IsGame // ゲーム中フラグ取得プロパティ
    {
        get { return isGame; }
        set { isGame = value; }
    }
    public bool IsGOAL // ゴールフラグ取得プロパティ
    {
        get { return isGOAL; }
        set { isGOAL = value; }
    }

    void Start()
    {
        // シングルトンインスタンスの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は、重複しないように破棄
        }
    }
}
