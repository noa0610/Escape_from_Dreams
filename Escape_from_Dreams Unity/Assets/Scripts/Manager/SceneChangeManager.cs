using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移を管理するシングルトン
/// </summary>
public class SceneChangeManager : MonoBehaviour
{
    // シングルトンのインスタンスを保持するプロパティ
    public static SceneChangeManager Instance { get; private set; }

    // 内部処理する変数
    private bool isChangingScene = false; // シーン遷移中かを判定するフラグ
    private void Awake()
    {
        // シングルトンパターンの実装
        // インスタンスが存在しなければ、このオブジェクトをシングルトンとして設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンが変わってもこのオブジェクトを破棄しない
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は、このオブジェクトを破棄
        }
    }

    // 指定されたシーンに遷移するメソッド
    public void ChangeSceneLoad(string sceneName) // 引数sceneNameは他スクリプトから指定
    {
        if (isChangingScene) return; // 既に遷移中なら無視

        isChangingScene = true; // 遷移中のフラグを設定
        SceneManager.LoadScene(sceneName); // 引数からシーンをロード
        isChangingScene = false; // シーン遷移が終わったら解除
    }

    // シーン遷移を遅延させるコルーチンを呼び出すメソッド(オーバーロード)
    public void ChangeSceneLoad(string sceneName, float changetime) // 引数sceneNameは他スクリプトから指定
    {
        if (isChangingScene) return; // 既に遷移中なら無視
        
        StartCoroutine(ChangeSceneWithDelay(sceneName, changetime)); // 引数からシーンをロード
    }

    // 実際のコルーチン処理は内部で管理
    private IEnumerator ChangeSceneWithDelay(string sceneName, float delay)
    {
        isChangingScene = true; // 遷移中のフラグを設定
        yield return new WaitForSeconds(delay); // 指定の時間待機
        SceneManager.LoadScene(sceneName); // 引数からシーンをロード
        isChangingScene = false; // シーン遷移が終わったら解除
    }
}