public class Defense : AbstractUnit
{
    public int health;

    public Defense(DefenseType defenseType) : base(defenseType)
    {
        this.health = defenseType.fullHealth;
    }
}
