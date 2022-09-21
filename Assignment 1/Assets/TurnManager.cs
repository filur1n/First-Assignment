using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    //AT the MOMENT
    [SerializeField] private GameObject projectilePrefab;
    //Maybe remove to another manager
    
    private static TurnManager instance;
    private int currentPlayerIndex;
    [SerializeField] private CinemachineFreeLook cam1;
    [SerializeField] private CinemachineFreeLook cam2;
     private PlayerTurn playerOne;
     private PlayerTurn playerTwo;
    [SerializeField] private GameObject playerOneObject;
    [SerializeField] private GameObject playerTwoObject;
    
    [SerializeField] private float timeBetweenTurns;
    
    private bool waitingForNextTurn = false;
    private float turnDelay;
    
    private int playerIndex;
    private bool hasShot = false;
    public void SetPlayerTurn(int index)
    {
        playerIndex = index;
    }

    /*public bool IsPlayerTurn()
    {
        return TurnManager.GetInstance().IsItPlayerTurn(playerIndex);
    }*/

    public GameObject GetProcetilePrefab()
    {
        return projectilePrefab;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            currentPlayerIndex = 1;
            playerOne = playerOneObject.GetComponent<PlayerTurn>();
            playerTwo = playerTwoObject.GetComponent<PlayerTurn>();
            //playerOne.SetPlayerTurn(1);
            //playerTwo.SetPlayerTurn(2);
            
        }
    }
    
    private void Update()
    {
        hasShot = false;
        /*if (waitingForNextTurn)
        {
            turnDelay += Time.deltaTime;
            if (turnDelay >= timeBetweenTurns)
            {
                turnDelay = 0;
                waitingForNextTurn = false;
                ChangeTurn();
            }
        }*/
    }

    public bool IsItPlayerTurn(int index)
    {
        if (hasShot)
        { 
            return false;
        }
        return index == currentPlayerIndex;
    }
    

    public static TurnManager GetInstance()
    {
        return instance;
    }

    public void ChangeTurn()
    {
        if (hasShot == false)
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
        

        hasShot = true;
    }
        
}
