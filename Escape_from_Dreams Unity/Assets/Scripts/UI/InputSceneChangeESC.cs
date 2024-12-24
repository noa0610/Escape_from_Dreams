using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Escapeキーで指定したシーンに移動するスクリプト
/// </summary>
public class InputSceneChangeESC : MonoBehaviour
{
    // インスペクターから設定する変数
    [SerializeField] private string SCENE_CHANGE_NAME;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneChangeManager.Instance.ChangeSceneLoad(SCENE_CHANGE_NAME);
        }
    }
}
