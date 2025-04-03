public class BaseState
{
    protected Character character;

    public BaseState(Character character)
    {
        this.character = character;
    }

    public virtual void BeginState() { }

    public virtual void EndState() { }

    public virtual void UpdateState() { }
}

/*
 * idle / wait (in attesa di iniziare)
 * selection (scegliere il tipo di azione)
 * action -1 Action Point 
 * move -1 Action Point
 * 
 * 
 */
