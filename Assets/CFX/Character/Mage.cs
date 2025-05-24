public class Mage : Character
{
    public override void Attack(Character target, byte attackNumber) {
        base.Attack(target, attackNumber);

    }



    public override void OnHitSuffered() {
        base.OnHitSuffered();
    }
}
