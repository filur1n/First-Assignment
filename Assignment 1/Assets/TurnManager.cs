using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;
    private int currentPlayerIndex;
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            currentPlayerIndex = 1;
        }
    }

    public bool IsItPlayerTurn(int index)
    {
        return index == currentPlayerIndex;
    }

    public static TurnManager GetInstance()
    {
        return instance;
    }

    public void ChangeTurn()
    {
        if (currentPlayerIndex == 1)
        {
            currentPlayerIndex = 2;
            cam1.gameObject.SetActive(false);
            cam2.gameObject.SetActive(true);
        }
        else if (currentPlayerIndex == 2)
        {
            currentPlayerIndex = 1;
            cam1.gameObject.SetActive(true);
            cam2.gameObject.SetActive(false);
        }
    }
        
}
