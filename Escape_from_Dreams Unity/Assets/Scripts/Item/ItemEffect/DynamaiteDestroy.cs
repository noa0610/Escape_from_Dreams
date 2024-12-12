using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamaiteDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject D_EFFECT; // エフェクトのプレハブ

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Invoke(nameof(GenerateEffect), 0.5f); // 0.5秒後にエフェクトを生成
            Destroy(gameObject, 0.5f); // 0.5秒後にオブジェクトを削除
        }
    }

    private void GenerateEffect()
    {
        GameObject effect = Instantiate(D_EFFECT, gameObject.transform.position, gameObject.transform.rotation); // エフェクトを生成
        Debug.Log("エフェクトが生成されました");
    }
}


