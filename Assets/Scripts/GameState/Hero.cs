using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Hero
{
    public string moniker;


    public override int GetHashCode()
    {
        return moniker.GetHashCode();
    }

    public override bool Equals(Object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Hero h = (Hero)obj;
            return h.moniker.Equals(moniker);
        }
    }
}
