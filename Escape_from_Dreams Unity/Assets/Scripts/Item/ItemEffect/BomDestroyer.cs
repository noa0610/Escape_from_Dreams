using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject B_EFFECT; // エフェクトのプレハブ

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Invoke(nameof(GenerateEffect), 1.5f); // 1.5秒後にエフェクトを生成
            Destroy(gameObject, 1.5f); // 1.5秒後にオブジェクトを削除
        }
    }

    private void GenerateEffect()
    {
        GameObject effect = Instantiate(B_EFFECT, gameObject.transform.position, gameObject.transform.rotation); // エフェクトを生成
        Debug.Log("エフェクトが生成されました");
    }
}
