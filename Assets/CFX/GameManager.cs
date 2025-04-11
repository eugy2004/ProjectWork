using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject instance;


    private List<Player> players;     //per avere informazioni riguardanti il player che sta svolgendo il suo turnos
    private int activePlayer;

    private bool turnEnded;
    private byte moveActions { get; set; }
    private byte attackActions { get; set; }
    public enum GameState { Moneta, Placement, Battle, Victory }
    private GameState currentState { get; set; }

    //funzioni di Unity
    void Start()
    {
        moveActions = 3;
        attackActions = 3;
        activePlayer = 0;
        players.Add(new Player());
        players.Add(new Player());
    }

    private void Update()
    {
        TurnHandler();
        ActionsUpdate();
    }


    //funzioni custom
    private void TurnHandler()
    {
        if ((moveActions == 0 && attackActions == 0) || turnEnded)       //possibilmente da cambiare
        {
            activePlayer = (activePlayer + 1) % 2;
            moveActions = 3;
            attackActions = 3;
        }
    }

    private void ActionsUpdate()
    {
        players[activePlayer].ActionsUpdate();
    }

    public void ChangeState(GameState newState)
    {
        ExitState();
        currentState = newState;
        EnterState();
    }

    private void ExitState()
    {

    }

    private void EnterState()
    {
        switch (currentState) 
        {
            case GameState.Battle:
                instance.SetActive(true);
                break;
        }
    }

    private void StateUpdate()
    {
        switch (currentState) 
        {
            case GameState.Moneta:
                break;

            case GameState.Placement:
                Debug.Log("ciao Diego");
                break;
                
            case GameState.Battle:
                break;

            case GameState.Victory:
                Debug.Log("Bella per Filo");
                break;
        }
    }
}
