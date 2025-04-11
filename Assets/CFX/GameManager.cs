using NUnit.Framework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player1, player2;     //per avere informazioni riguardanti il player che sta svolgendo il suo turno
    private byte moveActions { get; set; }
    private byte attackActions { get; set; }
    public enum GameState { Moneta, Placement, Battle, Victory }
    private GameState currentState { get; set; }

    //funzioni di Unity
    void Start()
    {
        moveActions = 3;
        attackActions = 3;
    }

    private void Update()
    {
        StateUpdate();
    }


    //funzioni custom
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
                
            case GameState.Battle:
                break;

            case GameState.Victory:
                Debug.Log("Bella per Filo");
                break;
        }
    }
}
