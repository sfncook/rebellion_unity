public class FactoryType : AbstractType
{
    public static FactoryType ctorYard = new FactoryType("Construction Yard");
    public static FactoryType shipYard = new FactoryType("Ship Yard");
    public static FactoryType trainingFac = new FactoryType("Training Facility");

    public FactoryType(string name) : base(name)
    {
    }
}
