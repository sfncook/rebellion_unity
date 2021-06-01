public abstract class AbstractType
{
    public readonly string name;
    public readonly TypeCategory typeCategory;

    public AbstractType(string name, TypeCategory typeCategory)
    {
        this.name = name;
        this.typeCategory = typeCategory;
    }
}
