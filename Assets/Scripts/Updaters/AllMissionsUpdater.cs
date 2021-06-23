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
                        MissionReport missionReport = null;
                        if(didMissionSucceed(personnel))
                        {
                            MissionType missionType = personnel.activeMission;
                            if (missionType.Equals(MissionType.diplomacy))
                            {
                                Planet targetPlanet = personnel.missionTargetPlanet;
                                float loyaltyDelta = UnityEngine.Random.Range(0.0f, 5f);
                                switch (personnel.team)
                                {
                                    case Team.TeamA:
                                        targetPlanet.loyalty -= loyaltyDelta;
                                        break;
                                    case Team.TeamB:
                                        targetPlanet.loyalty += loyaltyDelta;
                                        break;
                                }
                                missionReport = new DiplomacyMissionreport(personnel, missionType, true, loyaltyDelta);
                            }
                            else if (missionType.Equals(MissionType.espionage))
                            {

                            }
                            else if (missionType.Equals(MissionType.recruiting))
                            {

                            }
                        }// if mission succeeds
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
