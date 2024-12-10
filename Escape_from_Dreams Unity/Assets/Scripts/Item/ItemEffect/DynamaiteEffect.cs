using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム「ダイナマイト」を使用したときの効果
/// </summary>
public class DynamaiteEffect : MonoBehaviour, IItemEffect
{
    [SerializeField] private GameObject DYNAMAITE_PREFAB; 
    public void ApplyEffect()
    {
        // ダイナマイト生成処理実装
    }
}
