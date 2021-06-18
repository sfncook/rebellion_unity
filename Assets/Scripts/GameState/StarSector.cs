using System;
using System.Collections.Generic;

[System.Serializable]
public class StarSector
{
    public string name;
    public float galaxyX;
    public float galaxyY;
    public List<Planet> planets;



    public override int GetHashCode()
    {
        return name.GetHashCode() +
            (int)galaxyX +
            (int)galaxyY +
            planets.GetHashCode();
    }

    public override bool Equals(Object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            StarSector s = (StarSector)obj;
            return s.name.Equals(name);
        }
    }
}
