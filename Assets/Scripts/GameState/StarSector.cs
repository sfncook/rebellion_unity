using System.Collections.Generic;

[System.Serializable]
public class StarSector
{
    public string name;
    public readonly float galaxyX;
    public readonly float galaxyY;
    public List<Planet> planets;
}
