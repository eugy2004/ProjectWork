using UnityEngine;

public class Character 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public byte hp, attack, defence;

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


