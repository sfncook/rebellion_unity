using UnityEngine;
using System.Collections.Generic;

public class DiplomacyMissionCompleter: MissionCompleter
{
    Dictionary<Team, int> teamToLoyaltyMultiplier = new Dictionary<Team, int>
    {
        {Team.TeamA, -1},
        {Team.TeamB, +1}
    };

    public override MissionReport completeMission(StarSector sector, Planet planet, Personnel personnel)
    {
        bool missionSuccess = didMissionSucceed(personnel, personnel.diplomacy);
        Planet targetPlanet = personnel.missionTargetPlanet;
        float loyaltyDelta = Random.Range(0.0f, 5f);
        if(missionSuccess)
        {
            targetPlanet.loyalty = loyaltyDelta * teamToLoyaltyMultiplier[personnel.team];
        }
        return new DiplomacyMissionReport(personnel, missionSuccess, loyaltyDelta);
    }
}
