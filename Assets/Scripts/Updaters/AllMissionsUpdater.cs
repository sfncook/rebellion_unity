using System;
public class AllMissionsUpdater
{
    public void init()
    {
        MainGameState.gameState.addPreDayPrepEvent(onPrePrepEvent);
    }

    public void onPrePrepEvent()
    {
        foreach (StarSector sector in MainGameState.gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                foreach (Personnel personnel in planet.personnelsOnSurface)
                {
                    if(
                        personnel.hasMission() &&
                        MainGameState.gameState.gameTime >= personnel.dayMissionComplete
                    )
                    {
                        if(personnel.activeMission.Equals(MissionType.diplomacy))
                        {

                        } else if (personnel.activeMission.Equals(MissionType.espionage))
                        {

                        } else if (personnel.activeMission.Equals(MissionType.recruiting))
                        {

                        }
                    }
                }
            }
        }
    }
}
