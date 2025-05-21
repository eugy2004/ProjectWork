using UnityEngine;

public class Character : MonoBehaviour
{
    public int hp, attack, defence;

    public virtual void Attack(Character target) 
    {
        // damageManager.ProcessDamage(this, target);
    }

    public virtual void Move() 
    {
        
    }

    public virtual void OnHitSuffered() 
    {

    }

    public void OnSelect()
    {
        //quando clicchi sul personaggio mostra le sue stat
    }
}


