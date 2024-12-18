using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    private const string StageKey = "StageData";

    public static void SaveStageData(bool[] stageData)
    {
        string data = string.Join(",", stageData);// ステージ解放情報を文字列として保存
        PlayerPrefs.SetString(StageKey, data);
        PlayerPrefs.Save(); // データを保存
    }

    public static bool[] LoadStageData(int stageCount)
    {
        if (PlayerPrefs.HasKey(StageKey))
        {
            string data = PlayerPrefs.GetString(StageKey);
            string[] splitData = data.Split(',');
            bool[] stageData = new bool[stageCount];
            for (int i = 0; i < stageCount; i++)
            {
                stageData[i] = bool.Parse(splitData[i]);
            }
            return stageData;
        }
        return new bool[stageCount]; // 初期化（すべてfalse）
    }
}
