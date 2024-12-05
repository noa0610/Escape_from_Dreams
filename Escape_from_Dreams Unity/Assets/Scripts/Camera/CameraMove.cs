using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラを動かす処理
/// </summary>
public class CameraMove : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 10, -10); // プレイヤーからのオフセット位置
    public float cameraMoveSpeed = 5f; // カメラ追従のスピード

    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Playerタグのオブジェクトが見つかりません。");
        }
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // プレイヤーの位置 + オフセット位置にカメラをスムーズに移動させる
            Vector3 targetPosition = playerTransform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);
        }
    }
}
