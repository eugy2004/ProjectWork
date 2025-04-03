using UnityEngine;

public class ActionState : BaseState
{
    // Stato IDLE/WAIT, stato in cui il character rimane in attesa che gli venga dato il turno
    public ActionState(Character character) : base(character)
    {

    }

    public override void BeginState()
    {
        base.BeginState();

        Debug.Log("----------- ACTION -----------");
        Debug.Log("X per Confirm // Z per Cancellare");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (Input.GetKeyDown(KeyCode.X))
        {
        
            character.actionPoints--;
            character.ChangeState(character.CharStates["SELECTION"]);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
          
            character.ChangeState(character.CharStates["SELECTION"]);
        }
    }
}
