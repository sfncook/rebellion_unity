using UnityEngine;
using System.Collections.Generic;
using System;

public class PersonnelUpdater : MonoBehaviour
{
    public static void performAttackActions(Planet planet, Personnel personnel)
    {
        PersonnelType personnelType = (PersonnelType)personnel.type;
        if(personnelType.canAttack)
        {
            List<Personnel> enemyPersonnel = new List<Personnel>();
            foreach (Personnel otherPersonnel in planet.personnelsOnSurface)
            {
                if (!otherPersonnel.team.Equals(personnel.team))
                {
                    enemyPersonnel.Add(otherPersonnel);
                }
            }

            if (enemyPersonnel.Count > 0)
            {
                Personnel enemyPersonnelToAttack = enemyPersonnel[UnityEngine.Random.Range(0, enemyPersonnel.Count)];
                PersonnelType enemyPersonnelType = (PersonnelType)enemyPersonnelToAttack.type;
                // Each person in the group gets an attack
                for (var i = 0; i < personnel.manyPeople; i++)
                {
                    int attackValue = UnityEngine.Random.Range(0, 100);
                    float defenseModifierValue = UnityEngine.Random.Range(0.0f, enemyPersonnelType.defenseModifier);
                    float damage = Math.Max(attackValue - defenseModifierValue, 0);
                    //Debug.Log("attackValue:" + attackValue + " defenseModifierValue:" + defenseModifierValue + " damage:" + damage);
                    if (damage >= personnelType.attackAccuracyPerc)
                    {
                        enemyPersonnelToAttack.manyPeopleDead++;
                    }
                }
                //Debug.Log("enemyPersonnelToAttack.manyPeopleDead:"+ enemyPersonnelToAttack.manyPeopleDead);
            }
        }
    }// performAttackActions
}
