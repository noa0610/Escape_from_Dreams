using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGOALText : MonoBehaviour
{
    [SerializeField] private GameObject GOAL_TEXT_OBJECT;
    void Update()
    {
        if(GameManager.Instance.IsGOAL)
        {
            GOAL_TEXT_OBJECT.SetActive(true);
        }
    }
}
