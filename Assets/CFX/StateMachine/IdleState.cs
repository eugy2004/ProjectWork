using UnityEngine;

public class IdleState : BaseState
{
    // Stato IDLE/WAIT, stato in cui il character rimane in attesa che gli venga dato il turno
    public IdleState(Character character) : base(character)
    {
       
    }

    public override void BeginState()
    {
        base.BeginState();

        Debug.Log("-----------IDLE-----------");

        character.actionPoints = character.maxActionPoints;
        character.ChangeState(character.CharStates["SELECTION"]);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        // TEMP --> finchè non abbiamo un gestore del turno di gioco che ci dà l'ok per iniziare il turno
    }
}
