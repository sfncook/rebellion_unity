using UnityEngine;
using System.Collections.Generic;

public class AiAgentPlanUpdater
{
    private MainGameState gameState;

    public void init()
    {
        gameState = MainGameState.gameState;
        gameState.addListenerAgentPlanEvent(onAgentPlanEvent);
    }

    public void onAgentPlanEvent()
    {
        // Dictionary<Unit, Action>
        // Each action, below, gets its own src code file
        // Interface method takes the shared dictionary<Unit,Action> and modifies it as-needed
        // Actions should be completed in prioritized phases:
        //  - phase 1: Load personnel onto ships
        //  - phase 2: Move ships
        //  - phase 3: Factory-build orders
        //
        // 1. protect capitol ship
        //  - If teamA units on planet then move to random planet with no teamA units
        //  - If there are fewer teamA units in a different sector then move there
        //  - Keep sufficient (TBD) ships and personnel around for protection
        // 2. attack chosen one
        //  - If the chosen one is found dispatch enough units to capture it on the planet (scope: galaxy)
        // protect teamB assets
        //  - Personnel:
        //      - If we have more soldiers on the planet surface then fight it out (scope: planet)
        //      - Else (we have fewer soldiers on the planet surface) move as many soldiers as possible to ships (see ship extraction, below) (scope: planet)
        //  - Ships:
        //      - If we have more firepower in orbit then fight it out (scope: ind. unit)
        //      - Rescue personnel that need it (scope: planet)
        //      - Go to ship yard for healing (scope: ind. unit)
        // attack teamA assets
        //      - Send enough ships to capture and hold a planet (scope: sector)
        //      - Send enough personnel to capture and hold a planet (scope: sector)
        // seek teamA assets & capture territory
        //      - Send available ships to undiscovered planets
        // Ctor Yards
        //      - Cover each factory-planet with at least one shield (scope: sector)
        //      - Try to cover each planet with an orbital battery (scope: sector)
        //      - Build factories everywhere, randomly choose type (scope: sector)
        //      - Build ctor in newly discovered planets/sectors (scope: galaxy)
        // Ship yards
        //      - Ship yards keep building ships and randomly send them around the sector (scope: sector)
        //      - If local sector is saturated with ships and lacking teamA-ships then deploy ships to new sectors for discovery or population (scope: galaxy)
        // Training Facs
        //      - Training fac's keep building ships and randomly send them around the sector (scope: sector)
        //

        foreach (StarSector sector in gameState.galaxy.sectors)
        {
            foreach (Planet planet in sector.planets)
            {
                foreach (Defense defense in planet.defenses)
                {
                    DefenseUpdater.performAttackActions(planet, defense);
                }
            }
        }
    }
}
