using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイヤを回転させるスクリプト
/// </summary>
public class WheelRotation : MonoBehaviour
{
    private Vector3 wheelRotationSpeed = new Vector3 (360, 0, 0);
    void Start()
    {
        StartCoroutine(RotationWheel());
    }

    // 軽量化のためリジッドボディ、Update不使用
    // 一定間隔で動くコルーチンを使用
    private IEnumerator RotationWheel()
    {
        while(true)
        {
            transform.Rotate(wheelRotationSpeed * Time.deltaTime);
            yield return null; // 次のフレームまで待機
        }
    }
}
