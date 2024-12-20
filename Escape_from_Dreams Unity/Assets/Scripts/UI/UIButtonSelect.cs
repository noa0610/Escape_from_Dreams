using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonSelect : MonoBehaviour
{
    // インスペクターから設定する変数
    [SerializeField] private Button[] SELECT_BUTTUNS; // 各ボタン
    [SerializeField] private string[] SCENE_NAME; // 遷移するシーンの名前

    // 内部処理する変数
    private int selectedButtunIndex = 0; // 現在選択中のボタン
    private void Start()
    {
        HighlightSelectedButton(); // 最初のボタンをハイライト（選択状態）
    }

    private void Update()
    {
        HandleKeyboardInput(); // キーボード操作を処理
    }

    // 選択しているボタンをハイライト表示
    private void HighlightSelectedButton()
    {
        for (int i = 0; i < SELECT_BUTTUNS.Length; i++)
        {
            ColorBlock colorBlock = SELECT_BUTTUNS[i].colors; // ボタンの色を取得
            colorBlock.normalColor = (i == selectedButtunIndex) ? Color.yellow : Color.white; // 現在選択したものは黄色、それ以外の選択肢は白
            SELECT_BUTTUNS[i].colors = colorBlock; // ハイライトを適用
        }
    }

    // ステージ選択操作
    private void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) // 左キーで次のボタン
        {
            // 現在のインデックスから左方向のボタンを探す
            selectedButtunIndex--;
            if (selectedButtunIndex < 0) // 最初のボタンを超えたら最後に戻る
            {
                selectedButtunIndex = SELECT_BUTTUNS.Length - 1;
            }

            HighlightSelectedButton(); // 選択したボタンをハイライト表示

            Debug.Log($"{selectedButtunIndex}");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) // 右キーで前のボタン
        {
            // 現在のインデックスから右方向のボタンを探す
            selectedButtunIndex++;
            if (selectedButtunIndex >= SELECT_BUTTUNS.Length) // 最後のボタンを超えたら最初に戻る
            {
                selectedButtunIndex = 0;
            }

            HighlightSelectedButton(); // 選択したボタンをハイライト表示

            Debug.Log($"{selectedButtunIndex}");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectButtun(selectedButtunIndex);

            Debug.Log($"{selectedButtunIndex}");
        }
    }

    // ボタンを選択したときの処理
    private void SelectButtun(int stageIndex)
    {
        Debug.Log($"Buttun {stageIndex} selected!");
        Debug.Log($"{SCENE_NAME[stageIndex]}");
        SceneChangeManager.Instance.ChangeSceneLoad(SCENE_NAME[stageIndex]); // シーン遷移する
    }
}
