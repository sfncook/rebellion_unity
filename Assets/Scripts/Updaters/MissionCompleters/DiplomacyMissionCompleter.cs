using UnityEngine;
using System.Collections.Generic;

public class DiplomacyMissionCompleter: MissionCompleter
{
    Dictionary<Team, int> teamToLoyaltyMultiplier = new Dictionary<Team, int>
    {
        {Team.TeamA, 1},
        {Team.TeamB, -1}
    };

    public override MissionReport completeMission(StarSector sector, Planet planet, Personnel reporter)
    {
        Debug.Log("DiplomacyMissionCompleter");
        bool missionSuccess = didMissionSucceed(reporter, reporter.diplomacy);
        Planet targetPlanet = reporter.missionTargetPlanet;
        Team teamOrig = targetPlanet.getTeam();
        float loyaltyDelta = Random.Range(0.0f, 0.10f);
        bool loyaltyLost = false;
        float loyaltyLostDelta = 0;
        if (missionSuccess)
        {
            float signedLoyaltyDelta = loyaltyDelta * teamToLoyaltyMultiplier[reporter.team];
            targetPlanet.loyalty = targetPlanet.loyalty + signedLoyaltyDelta;
            Debug.Log("  - signedLoyaltyDelta:" + signedLoyaltyDelta  + " targetPlanet.loyalty:"+ targetPlanet.loyalty);
        } else
        {
            // Mission failed, % chance that loyalty actually worsens
            int random = Random.Range(0, 100);
            if(random <= 3)
            {
                loyaltyLost = true;
                loyaltyLostDelta = loyaltyDelta * teamToLoyaltyMultiplier[reporter.team] * -1;
                targetPlanet.loyalty = targetPlanet.loyalty + loyaltyLostDelta;
                Debug.Log("  - loyaltyLossDelta:" + loyaltyLostDelta + " targetPlanet.loyalty:" + targetPlanet.loyalty);
            }
        }

        // Planetary loyalty change
        Team teamFinal = targetPlanet.getTeam();
        if(!teamOrig.Equals(teamFinal)) {
            string content;
            ReportSeverity severity;
            if (teamFinal.Equals(MainGameState.gameState.myTeam))
            {
                content = "We have turned the loyalty of planet " + targetPlanet.name + " in our favor!";
                severity = ReportSeverity.Info;
            } else
            {
                content = "We have lost the loyalty of planet " + targetPlanet.name;
                severity = ReportSeverity.Warning;
            }
            InfoReport infoReport = new InfoReport(
                    "Planet Loyalty Change",
                    reporter,
                    MainGameState.gameState.gameTime,
                    severity,
                    content,
                    targetPlanet
                );
            MainGameState.gameState.reportsShowImmediately.Add(infoReport);
        }
        return new DiplomacyMissionReport(reporter, missionSuccess, MainGameState.gameState.gameTime, false, loyaltyDelta, loyaltyLost, loyaltyLostDelta);
    }
}
