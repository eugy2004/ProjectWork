using UnityEngine;

public class Character : MonoBehaviour 
{

    public byte maxHp, hp, attack, defence;
    public byte timesAttacked;
    public Troop troop;
    private void Awake() {
        troop = GetComponent<Troop>();
    }
    public virtual void Attack(Character target) 
    {
        // damageManager.ProcessDamage(this, target);
    }

    public virtual void Move() 
    {
        
    }

    public virtual void OnHitSuffered(byte dmg) 
    {
        hp -= dmg;
    }

    public void OnSelect()
    {
        //quando clicchi sul personaggio mostra le sue stat
    }
}


