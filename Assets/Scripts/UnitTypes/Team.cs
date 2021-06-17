using UnityEngine;

public enum Team
{
    TeamA, TeamB
}

static class StuffMethods
{

    public static Color getColorForTeam(this Team team)
    {
        switch (team)
        {
            case Team.TeamA:
                return Color.green;
            case Team.TeamB:
                return Color.red;
            default:
                return Color.gray;
        }
    }
}