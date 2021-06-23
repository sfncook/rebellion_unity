using UnityEngine;
public class RecruitingMissionCompleter: MissionCompleter
{
    public override MissionReport completeMission(StarSector sector, Planet planet, Personnel personnel)
    {
        Debug.Log("RecruitingMissionCompleter completeMission");
        bool missionSuccess;
        Personnel recruitedPersonnel = null;
        if (MainGameState.gameState.firstRecruitingTask)
        {
            Debug.Log("Should be working");
            missionSuccess = true;
            MainGameState.gameState.firstRecruitingTask = false;
            recruitedPersonnel = new Personnel(PersonnelType.Diplomat, personnel.team);
            planet.personnelsOnSurface.Add(recruitedPersonnel);
        } else
        {
            missionSuccess = didMissionSucceed(personnel, personnel.diplomacy);
            if (missionSuccess)
            {
                // Story line events
                if (MainGameState.gameState.firstRecruitingTask)
                {
                    MainGameState.gameState.firstRecruitingTask = false;
                    recruitedPersonnel = new Personnel(PersonnelType.Diplomat, personnel.team);
                }
                planet.personnelsOnSurface.Add(recruitedPersonnel);
            }
        }
        return new RecruiterMissionReport(personnel, missionSuccess, recruitedPersonnel);
    }
}
