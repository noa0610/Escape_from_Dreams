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
        Debug.Log("SceneChange1");
        if (isChangingScene) return; // 既に遷移中なら無視

        Debug.Log("SceneChange2");
        isChangingScene = true; // 遷移中のフラグを設定
        StartCoroutine(LoadSceneAsync(sceneName)); // 非同期ロードを開始
    }

    // シーン遷移を遅延させるコルーチンを呼び出すメソッド(オーバーロード)
    public void ChangeSceneLoad(string sceneName, float changetime) // 引数sceneNameは他スクリプトから指定
    {
        Debug.Log("SceneChangeDeray1");
        if (isChangingScene) return; // 既に遷移中なら無視
        
        Debug.Log("SceneChangeDeray2");
        StartCoroutine(ChangeSceneWithDelay(sceneName, changetime)); // 引数からシーンをロード
    }

    // 非同期でシーンをロード
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.completed += _ => isChangingScene = false; // ロード完了時にフラグを解除
        yield return asyncLoad; // ロード完了を待機
    }

    // 実際のコルーチン処理は内部で管理
    private IEnumerator ChangeSceneWithDelay(string sceneName, float delay)
    {
        isChangingScene = true; // 遷移中のフラグを設定
        yield return new WaitForSeconds(delay); // 指定の時間待機
        yield return LoadSceneAsync(sceneName); // シーンロードを非同期で処理
    }
}