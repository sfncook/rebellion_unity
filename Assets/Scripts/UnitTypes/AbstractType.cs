public abstract class AbstractType
{
    public readonly string name;
    public readonly TypeCategory typeCategory;
    public readonly int daysToBuild;

    public AbstractType(string name, TypeCategory typeCategory, int daysToBuild = 2)
    {
        this.name = name;
        this.typeCategory = typeCategory;
        this.daysToBuild = daysToBuild;
    }
}
