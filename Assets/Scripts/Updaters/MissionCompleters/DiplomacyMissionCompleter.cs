using UnityEngine;
using System.Collections.Generic;

public class DiplomacyMissionCompleter: MissionCompleter
{
    Dictionary<Team, int> teamToLoyaltyMultiplier = new Dictionary<Team, int>
    {
        {Team.TeamA, 1},
        {Team.TeamB, -1}
    };

    public override MissionReport completeMission(StarSector sector, Planet planet, Personnel personnel)
    {
        Debug.Log("DiplomacyMissionCompleter");
        bool missionSuccess = didMissionSucceed(personnel, personnel.diplomacy);
        Planet targetPlanet = personnel.missionTargetPlanet;
        float loyaltyDelta = Random.Range(0.0f, 0.10f);
        if(missionSuccess)
        {
            float signedLoyaltyDelta = loyaltyDelta * teamToLoyaltyMultiplier[personnel.team];
            targetPlanet.loyalty = targetPlanet.loyalty + signedLoyaltyDelta;
            Debug.Log("  - signedLoyaltyDelta:" + signedLoyaltyDelta  + " targetPlanet.loyalty:"+ targetPlanet.loyalty);
        }
        return new DiplomacyMissionReport(personnel, missionSuccess, MainGameState.gameState.gameTime, loyaltyDelta);
    }
}
