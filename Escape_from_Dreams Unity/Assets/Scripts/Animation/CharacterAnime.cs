using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animatorを取得し、変更に応じてアニメーションの切り替えを行うクラス
/// </summary>
public class CharacterAnime : MonoBehaviour
{
    public Animator Animator {get; private set;}

    void Awake()
    {
        Animator = GetComponent<Animator>();
    }
}