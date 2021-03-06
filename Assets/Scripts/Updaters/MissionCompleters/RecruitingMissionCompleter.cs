using UnityEngine;
using System.Collections.Generic;
using System;

public class RecruitingMissionCompleter: MissionCompleter
{
    List<Tuple<Planet, Personnel>> addPersonnelToPlanets = new List<Tuple<Planet, Personnel>>();
    List<PersonnelType> recruitableTypes = new List<PersonnelType>() {
        PersonnelType.Diplomat,
        PersonnelType.Spy,
        PersonnelType.Hero
    };

    public override MissionReport completeMission(StarSector sector, Planet planet, Personnel personnel)
    {
        bool missionSuccess;
        Personnel recruitedPersonnel = null;
        if (MainGameState.gameState.firstRecruitingTask)
        {
            missionSuccess = true;
            MainGameState.gameState.firstRecruitingTask = false;
            recruitedPersonnel = new Personnel(PersonnelType.Diplomat, personnel.team);
            addPersonnelToPlanets.Add(new Tuple<Planet, Personnel>(planet, recruitedPersonnel));
        } else
        {
            missionSuccess = didMissionSucceed(personnel, personnel.recruiting);
            if (missionSuccess)
            {
                PersonnelType recruitedType = recruitableTypes[UnityEngine.Random.Range(0, recruitableTypes.Count)];
                if(recruitedType.Equals(PersonnelType.Hero))
                {
                    int heroIndex = UnityEngine.Random.Range(0, MainGameState.gameState.heroesAvailableForRecruiting.Count);
                    Hero recruitedHero = MainGameState.gameState.heroesAvailableForRecruiting[heroIndex];
                    recruitedPersonnel = new Personnel(
                        recruitedType,
                        personnel.team,
                        recruitedHero,
                        UnityEngine.Random.Range(10, 80),
                        UnityEngine.Random.Range(10, 80),
                        UnityEngine.Random.Range(10, 80),
                        UnityEngine.Random.Range(10, 80)
                    );
                    MainGameState.gameState.heroesAvailableForRecruiting.Remove(recruitedHero);
                } else
                {
                    recruitedPersonnel = new Personnel(recruitedType, personnel.team);
                }
                addPersonnelToPlanets.Add(new Tuple<Planet, Personnel>(planet, recruitedPersonnel));
            }
        }
        return new RecruiterMissionReport(personnel, missionSuccess, MainGameState.gameState.gameTime, false, recruitedPersonnel);
    }

    public void addAllRecruitedPersonnelToPlanets()
    {
        foreach(Tuple<Planet, Personnel> tuple in addPersonnelToPlanets)
        {
            Planet planet = tuple.Item1;
            Personnel recruitedPersonnel = tuple.Item2;
            planet.personnelsOnSurface.Add(recruitedPersonnel);
        }
        addPersonnelToPlanets = new List<Tuple<Planet, Personnel>>();
    }
}
