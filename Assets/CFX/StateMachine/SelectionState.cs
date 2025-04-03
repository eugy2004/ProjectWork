using Unity.VisualScripting;
using UnityEngine;

public class SelectionState : BaseState
{

    public SelectionState(Character character) : base(character) { }

    public override void BeginState()
    {
        base.BeginState();

        Debug.Log("---------SELECTION---------");
        Debug.Log("A per ActionState // M per MoveState");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(character.actionPoints == 0)
        {
            character.ChangeState(character.CharStates["IDLE"]);
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            // Action STATE
            character.ChangeState(character.CharStates["ACTION"]);
            return;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Move State
            character.ChangeState(character.CharStates["MOVE"]);
            return;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            // pass > idle
            // character.ChangeState(new IdleState(character));
            character.ChangeState(character.CharStates["IDLE"]);
            return;
        }
    }
    
}
