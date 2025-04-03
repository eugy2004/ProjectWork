using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Action Points

    public int actionPoints; // action points che abbiamo ora
    public int maxActionPoints = 2; // action points massimi
    

    public BaseState currentState;

    public Dictionary<string, BaseState> CharStates;

    private void Start()
    {
        CharStates = new Dictionary<string, BaseState>();

        CharStates.Add("IDLE", new IdleState(this));
        CharStates.Add("SELECTION", new SelectionState(this));
        CharStates.Add("MOVE", new MoveState(this));
        CharStates.Add("ACTION", new ActionState(this));

        ChangeState(CharStates["IDLE"]);
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public void ChangeState(BaseState newState)
    {
        if (currentState != null)
        {
            currentState.EndState();
        }

        currentState = newState;

        currentState.BeginState();
    }

    // metodo alternativo per gli action points
    /* public void RefreshCharAP()
    {
        actionPoints = maxActionPoints;
    }

    public bool HasActionPoints()
    {
        return actionPoints > 0;
    }

    public void UseActionPoint()
    {
        actionPoints--;
    } */
}
