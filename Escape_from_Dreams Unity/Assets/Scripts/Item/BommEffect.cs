using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム「バクダン」を使用したときの効果
/// </summary>
public class BommEffect : MonoBehaviour, IItemEffect
{
    [SerializeField] private GameObject BOMM_PREFAB; 
    public void ApplyEffect()
    {
        // バクダン生成処理の実装
    }
}
