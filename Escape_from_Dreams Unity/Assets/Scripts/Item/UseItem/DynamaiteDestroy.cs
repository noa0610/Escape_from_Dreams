using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamaiteDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject D_EFFECT; // エフェクトのプレハブ
    [SerializeField] private float EXPROM_TIME;//爆破までの時間
    [SerializeField] private float EXPROM_FIRD;//爆発エフェクトの効果時間

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Obstacle"))
        {
            Invoke(nameof(GenerateEffect), EXPROM_TIME); //EXPROM_TIMEの時間経過後にエフェクトを生成
            Destroy(gameObject, EXPROM_FIRD); // EXPROM_FILDの時間が経過した後にオブジェクトを削除
        }
    }

    private void GenerateEffect()
    {
        GameObject effect = Instantiate(D_EFFECT, gameObject.transform.position, gameObject.transform.rotation); // エフェクトを生成
        Debug.Log("エフェクトが生成されました");
    }
}


