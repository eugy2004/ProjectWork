using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Troop> player1Troops, player2Troops;    // le truppe del primo e del secondo player
    private List<Troop> activeTroops;
    private byte playerID;            // serve a capire di chi sarà il prossimo turno (viene passato alla funzione SetUpNextPlayerAction)

    private byte MoveActions { get; set; }
    private byte AttackActions { get; set; }
    public enum GameState { Moneta, Placement, Pesca, PlayerAction, Victory }
    private GameState CurrentState { get; set; }

    public GameObject prefabWarrior;

    public GameObject prefabArcher;

    public GameObject prefabWizard;

    public Troop troop;

    private PlayerMove activePlayerMove;

    //funzioni di Unity
    void Start()
    {

    }

    private void Update()
    {

    }


    //funzioni custom
    public void ChangeState(GameState newState)
    {
        ExitState();
        CurrentState = newState;
        EnterState();
    }

    private void ExitState()
    {

    }

    private void EnterState()
    {
        switch (CurrentState) 
        {
            case GameState.PlayerAction:
                SetUpNextPlayerAction();
                break;
        }
    }

    private void StateUpdate()
    {
        switch (CurrentState)
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

    private void SetUpNextPlayerAction()
    {
        MoveActions = 3;
        AttackActions = 3;
        switch (playerID)
        {
            case 1:
                activeTroops = player1Troops;
                break;

            case 2:
                activeTroops = player2Troops;
                break;
        }
    }

    public void DeployTroop(int IdTroop)
    {
        switch (IdTroop)
        {
            case 0:
                DeployWarrior();
                break;
            case 1:
                DeployArcher();
                break;
            case 2:
                DeployWizard();
                break;
            default:
                break;
        }
    }

    public void DeployWarrior()
    {
        GameObject newPlayer = Instantiate(prefabWarrior, Vector3.zero, Quaternion.identity);
        activePlayerMove = newPlayer.GetComponent<PlayerMove>();

        foreach (GridNode node in FindObjectsOfType<GridNode>())
        {
            node.SetPlayer(activePlayerMove);
        }
    }

    public void DeployArcher()
    {
        GameObject newPlayer = Instantiate(prefabArcher, Vector3.zero, Quaternion.identity);
        activePlayerMove = newPlayer.GetComponent<PlayerMove>();

        foreach (GridNode node in FindObjectsOfType<GridNode>())
        {
            node.SetPlayer(activePlayerMove);
        }
    }

    public void DeployWizard()
    {
        GameObject newPlayer = Instantiate(prefabWizard, Vector3.zero, Quaternion.identity);
        activePlayerMove = newPlayer.GetComponent<PlayerMove>();

        foreach (GridNode node in FindObjectsOfType<GridNode>())
        {
            node.SetPlayer(activePlayerMove);
        }
    }
}
