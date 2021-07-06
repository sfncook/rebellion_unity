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
        bool loyaltyLost = false;
        float loyaltyLostDelta = 0;
        if (missionSuccess)
        {
            float signedLoyaltyDelta = loyaltyDelta * teamToLoyaltyMultiplier[personnel.team];
            targetPlanet.loyalty = targetPlanet.loyalty + signedLoyaltyDelta;
            Debug.Log("  - signedLoyaltyDelta:" + signedLoyaltyDelta  + " targetPlanet.loyalty:"+ targetPlanet.loyalty);
        } else
        {
            // Mission failed, % chance that loyalty actually worsens
            int random = Random.Range(0, 100);
            if(random <= 10)
            {
                loyaltyLost = true;
                loyaltyLostDelta = loyaltyDelta * teamToLoyaltyMultiplier[personnel.team] * -1;
                targetPlanet.loyalty = targetPlanet.loyalty + loyaltyLostDelta;
                Debug.Log("  - loyaltyLossDelta:" + loyaltyLostDelta + " targetPlanet.loyalty:" + targetPlanet.loyalty);
            }
        }
        return new DiplomacyMissionReport(personnel, missionSuccess, MainGameState.gameState.gameTime, loyaltyDelta, loyaltyLost, loyaltyLostDelta);
    }
}
