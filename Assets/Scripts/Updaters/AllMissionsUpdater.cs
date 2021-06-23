using System.Collections.Generic;
using UnityEngine;

public class AllMissionsUpdater
{
    Dictionary<MissionType, MissionCompleter> typeToCompleter = new Dictionary<MissionType, MissionCompleter>
    {
        {MissionType.diplomacy, new DiplomacyMissionCompleter()},
        {MissionType.recruiting, new RecruitingMissionCompleter()},
    };

    public void init()
    {
        Debug.Log("init");
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
                        MainGameState.gameState.gameTime <= personnel.dayMissionComplete
                    )
                    {
                        Debug.Log("Mission complete:"+ personnel.activeMission.name);
                        MissionCompleter missionCompleter = typeToCompleter[personnel.activeMission];
                        MissionReport missionReport = missionCompleter.completeMission(sector, planet, personnel);

                        personnel.resetMission();
                        MainGameState.gameState.reportsUnAcked.Add(missionReport);
                    }// if mission done
                }
            }
        }
    }

    private bool didMissionSucceed(Personnel personnel)
    {
        MissionType missionType = personnel.activeMission;
        float successThreshold = 0;
        if(missionType.Equals(MissionType.diplomacy))
        {
            successThreshold = personnel.diplomacy;
        }
        else if (missionType.Equals(MissionType.espionage))
        {
            successThreshold = personnel.espionage;
        }
        else if(missionType.Equals(MissionType.recruiting))
        {
            successThreshold = personnel.recruiting;
        }
        return UnityEngine.Random.Range(0.0f, 100f)<= successThreshold;
    }
}
