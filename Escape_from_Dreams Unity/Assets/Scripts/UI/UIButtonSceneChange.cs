using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// クリックでシーン遷移するボタン
/// </summary>
public class UIButtonSceneChange : MonoBehaviour, IPointerClickHandler
{
    // インスペクターから設定する変数
    [Header("遷移先のシーン名")]
    [SerializeField] private string SCENE_CHANGE_NAME; // 遷移先のシーン名

    [Header("遷移までの待機時間")]
    [SerializeField] private float SCENE_CHANGE_DELAY = 0f; // 遷移前の遅延時間（任意）

    void Start()
    {
        if (SceneChangeManager.Instance == null)
        {
            Debug.LogError($"{gameObject.name}: SceneChangeManagerが存在しません。");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(SCENE_CHANGE_NAME))
        {
            if (SCENE_CHANGE_DELAY > 0)
            {
                SceneChangeManager.Instance.ChangeSceneLoad(SCENE_CHANGE_NAME, SCENE_CHANGE_DELAY); // 遅延あり
            }
            else
            {
                SceneChangeManager.Instance.ChangeSceneLoad(SCENE_CHANGE_NAME); // 遅延なし
            }
        }
        else
        {
            Debug.LogWarning("遷移先のシーン名が設定されていません。");
        }
    }
}