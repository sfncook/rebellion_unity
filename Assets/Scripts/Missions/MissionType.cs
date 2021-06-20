using System;
public class MissionType
{
    public static MissionType espionage = new MissionType("Espionage");
    public static MissionType recruiting = new MissionType("Recruiting");
    public static MissionType diplomacy = new MissionType("Diplomacy");

    public readonly string name;

    public MissionType(string name)
    {
        this.name = name;
    }

    public override int GetHashCode()
    {
        return name.GetHashCode();
    }

    public override bool Equals(Object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            MissionType p = (MissionType)obj;
            return p.name.Equals(name);
        }
    }
}
