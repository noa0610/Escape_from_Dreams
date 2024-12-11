using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 10;

    private int currentHP;
    void Start()
    {
        currentHP = HP;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Obstacle : HP ={currentHP}");
        currentHP = currentHP - damage;
        if(currentHP <= 0)
        {
            Debug.Log($"Obstacle : Destroy");
            Destroy(this.gameObject);
        }
    }
}
