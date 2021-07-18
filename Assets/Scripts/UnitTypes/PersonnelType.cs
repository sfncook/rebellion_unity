using System.Collections.Generic;

public class PersonnelType : AbstractType
{
    public static PersonnelType Soldiers = new PersonnelType(
        "Soldiers",
        isStealth:false,
        totalManyPeople:10,
        canAttack:true,
        attackAccuracyPerc: 50,
        defenseModifier: 10,
        isHero: false,
        defaultVisibility: 10
    );
    public static PersonnelType Diplomat = new PersonnelType(
        "Diplomat",
        isStealth: false,
        totalManyPeople:1,
        canAttack:false,
        isHero: true,
        defaultEspionage: 30,
        defaultRecruiting:50,
        defaultDiplomacy:60,
        defaultVisibility: 20,
        availableMissionTypes: new List<MissionType>() { MissionType.diplomacy, MissionType.espionage, MissionType.recruiting }
    );
    public static PersonnelType Spy = new PersonnelType(
        "Spy",
        isStealth: true,
        totalManyPeople:1,
        canAttack:false,
        isHero: true,
        defaultEspionage: 60,
        defaultRecruiting: 50,
        defaultDiplomacy: 20,
        defaultVisibility: 80,
        availableMissionTypes: new List<MissionType>() { MissionType.diplomacy, MissionType.espionage, MissionType.recruiting }
    );
    public static PersonnelType Hero = new PersonnelType(
        "Hero",
        isStealth: true,
        totalManyPeople: 1,
        canAttack: false,
        isHero: true,
        availableMissionTypes: new List<MissionType>() { MissionType.diplomacy, MissionType.espionage, MissionType.recruiting }
    );

    public readonly bool isStealth;
    public readonly bool isMultiple;
    public readonly int totalManyPeople;
    public readonly bool canAttack;
    public readonly int attackAccuracyPerc; // Out of 100
    public readonly int defenseModifier; // Out of 100

    public readonly bool isHero;
    public readonly int defaultVisibility;

    // Default values for personnel mission qualifications
    public readonly int defaultEspionage;
    public readonly int defaultRecruiting;
    public readonly int defaultDiplomacy;

    public List<MissionType> availableMissionTypes = new List<MissionType>();

    public PersonnelType(
        string name,
        bool isStealth,
        int totalManyPeople,
        bool canAttack,
        int attackAccuracyPerc = 0,
        int defenseModifier = 0,
        bool isHero = true,
        int defaultVisibility = 0,
        int defaultEspionage = 0,
        int defaultRecruiting = 0,
        int defaultDiplomacy = 0,
        List<MissionType> availableMissionTypes = null
        ) : base(name, TypeCategory.Personnel)
    {
        this.isStealth = isStealth;
        this.totalManyPeople = totalManyPeople;
        this.canAttack = canAttack;
        this.attackAccuracyPerc = attackAccuracyPerc;
        this.defenseModifier = defenseModifier;
        this.isHero = isHero;
        this.defaultEspionage = defaultEspionage;
        this.defaultRecruiting = defaultRecruiting;
        this.defaultDiplomacy = defaultDiplomacy;
        if(availableMissionTypes!=null)
        {
            this.availableMissionTypes = new List<MissionType>(availableMissionTypes);
        }
    }
}
