using UnityEngine;

public class Character : MonoBehaviour
{
    public int hp, attack, defence;

    public Troop troop;

    public void Awake()
    {
        troop = GetComponent<Troop>();
    }

    public virtual void Attack(Character target, byte attackNumber) 
    {
        // damageManager.ProcessDamage(this, target);
        target.hp = (attack/attackNumber)-target.defence;
    }

    public virtual void OnHitSuffered() 
    {

    }

    public void OnSelect()
    {
        //quando clicchi sul personaggio mostra le sue stat
    }
}


