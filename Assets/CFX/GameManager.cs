using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject instance;       //Il gameObject che contiene il playerMove


    private List<Player> players;     //per avere informazioni riguardanti il player che sta svolgendo il suo turnos
    private int activePlayer;

    private bool turnEnded;
    private byte moveActions { get; set; }
    private byte attackActions { get; set; }
    public enum GameState { Moneta, Placement, Pesca, PlayerAction, Victory }    //aggiungo la fine del turno?
    private GameState currentState { get; set; }

    //funzioni di Unity
    void Start()
    {
        moveActions = 3;
        attackActions = 3;
        activePlayer = 0;
        players.Add(new Player());       //andrà inserita una lista di personaggi durante la fase di selezione
        players.Add(new Player());
    }

    private void Update()
    {
        //SwitchTurn();
    }


    //funzioni custom
    private void SwitchTurn()      //faccio il cambio turno con un evento o così?
    {
        if ((moveActions == 0 && attackActions == 0) || turnEnded)       //possibilmente da cambiare
        {
            activePlayer = (activePlayer + 1) % 2;
            moveActions = 3;
            attackActions = 3;
        }
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

            case GameState.Pesca:
                Debug.Log("Rob, scelgo te!");
                break;
                
            case GameState.PlayerAction:
                break;

            case GameState.Victory:
                Debug.Log("Bella per Filo");
                break;
        }
    }
}
