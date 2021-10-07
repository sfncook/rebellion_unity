using UnityEngine;

public class WinLoseUpdater
{
    public void init()
    {
        MainGameState.gameState.addListenerUiUpdateEvent(onUiUpdateEvent);
    }

    public void onUiUpdateEvent()
    {
        bool teamBCapitolShipStillExists = false;
        bool teamAChosenOneStillExists = false;
        foreach (StarSector sector in MainGameState.gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                if (
                    planet.shipsInOrbit.Exists(ship => ship.type == ShipType.Capitol) ||
                    planet.shipsInTransit.Exists(ship => ship.type == ShipType.Capitol)
                )
                {
                    teamBCapitolShipStillExists = true;
                }

                if (
                    planet.personnelsOnSurface.Exists(unit => unit.type == PersonnelType.ChosenOne) ||
                    planet.personnelsInTransit.Exists(unit => unit.type == PersonnelType.ChosenOne)
                )
                {
                    teamAChosenOneStillExists = true;
                }
            }

            if(teamBCapitolShipStillExists && teamAChosenOneStillExists)
            {
                break;
            }
        }

        if (!teamBCapitolShipStillExists)
        {
            if (MainGameState.gameState.myTeam == Team.TeamA)
            {
                MainGameState.gameState.playerWins();
            } else
            {
                MainGameState.gameState.playerLoses();
            }
        }

        if (!teamAChosenOneStillExists)
        {
            if (MainGameState.gameState.myTeam == Team.TeamA)
            {
                MainGameState.gameState.playerLoses();
            }
            else
            {
                MainGameState.gameState.playerWins();
            }
        }

    }
}
