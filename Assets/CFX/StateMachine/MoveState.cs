using UnityEngine;

public class MoveState : BaseState
{
    // Stato IDLE/WAIT, stato in cui il character rimane in attesa che gli venga dato il turno
    public MoveState(Character character) : base(character)
    {

    }

    public override void BeginState()
    {
        base.BeginState();

        Debug.Log("----------- MOVE -----------");
        Debug.Log("X per ConfirmMove (-1) // Z per CancelMove");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (Input.GetKeyDown(KeyCode.X))
        {
            //DEBUG
            character.actionPoints--;
            character.ChangeState(character.CharStates["SELECTION"]);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            character.ChangeState(character.CharStates["SELECTION"]);
            return;
        }
    }
}
