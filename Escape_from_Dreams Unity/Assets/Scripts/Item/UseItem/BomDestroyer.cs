using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject B_EFFECT; // エフェクトのプレハブ
    [SerializeField] private float EXPROM_TIME;//爆破までの時間
    [SerializeField] private float EXPROM_FIRD;//爆発エフェクトの効果時間

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Invoke(nameof(GenerateEffect), EXPROM_TIME); // EXPROM_TIMEの時間が経過した後にオブジェクトを生成
            Destroy(gameObject, EXPROM_FIRD);// EXPROM_FILDの時間が経過した後にオブジェクトを削除 
        }
    }

    private void GenerateEffect()
    {
        GameObject effect = Instantiate(B_EFFECT, gameObject.transform.position, gameObject.transform.rotation); // エフェクトを生成
        Debug.Log("エフェクトが生成されました");
    }
}
