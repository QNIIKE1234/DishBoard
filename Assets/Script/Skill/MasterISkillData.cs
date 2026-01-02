using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class MasterISkillData 
{
    public static List<Skill> masterSkillList =  new List<Skill>(){
        new Skill(){skillIndex = 0 , skillCode = "SKI_001_Bash" , skillName = "Bash" , skillIconPath = "SKILLICON/16" , skillDescription = "Attacks with high damage, push enemy 1 position and stunned."},
        new Skill(){skillIndex = 1 , skillCode = "SKI_002_FireBall" , skillName = "FireBall" , skillIconPath = "SKILLICON/104" , skillDescription = "Magic attacks with fire damage and is enemy burned."},
        new Skill(){skillIndex = 2 , skillCode = "SKI_003_BraveSpirit" , skillName = "BraveSpirit" , skillIconPath = "SKILLICON/68" , skillDescription = "Increases own attack power by 25 percent."},
        new Skill(){skillIndex = 3 , skillCode = "SKI_004_WolfStrike" , skillName = "WolfStrike" , skillIconPath = "SKILLICON/108" , skillDescription = "Use Wolf soul attacks with wind damage and enemy is stunned."},
        new Skill(){skillIndex = 4 , skillCode = "SKI_006_HookShot" , skillName = "HookShot" , skillIconPath = "SKILLICON/28" , skillDescription = "pull enemies towards yourself 3 position."},
        new Skill(){skillIndex = 5 , skillCode = "SKI_007_FocusFire" , skillName = "FocusFire" , skillIconPath = "SKILLICON/86" , skillDescription = "Increases your attack range 1 position."},
        new Skill(){skillIndex = 6 , skillCode = "SKI_008_PoisionBlade" , skillName = "PoisionBlade" , skillIconPath = "SKILLICON/15 " , skillDescription = "Attacks with poision damage and enemy is poisioned."},
        new Skill(){skillIndex = 7 , skillCode = "SKI_009_GhostWalk" , skillName = "GhostWalk" , skillIconPath = "SKILLICON/56" , skillDescription = "Move yourself to the designated position and increase your evasion."},
        new Skill(){skillIndex = 8 , skillCode = "SKI_010_ShockWave" , skillName = "ShockWave" , skillIconPath = "SKILLICON/43" , skillDescription = "Attacks with high magic damage and push enemy 1 position"},
        new Skill(){skillIndex = 9 , skillCode = "SKI_000_NormalAttack" , skillName = "NormalAttack" , skillIconPath = "SKILLICON/Attack" , skillDescription = "Attacks with your damage."},
        new Skill(){skillIndex = 10 , skillCode = "SKI_000_Move" , skillName = "Move" , skillIconPath = "SKILLICON/Buff" , skillDescription = "Move yourself to the designated position"},
        new Skill(){skillIndex = 11 , skillCode = "SKI_011_Dash" , skillName = "Dash" , skillIconPath = "SKILLICON/36" , skillDescription = "Move yourself to the designated position and increase your attack."},
        new Skill(){skillIndex = 12 , skillCode = "SKI_012_Teleport" , skillName = "Teleport" , skillIconPath = "SKILLICON/1" , skillDescription = "Move yourself to the designated position and heal your self 5 % of MaxHP."},
        new Skill(){skillIndex = 13 , skillCode = "SKI_013_LightingBall" , skillName = "LightingBall" , skillIconPath = "SKILLICON/54" , skillDescription = "Attacks with wind damage and push enemy 1 position"},
        new Skill(){skillIndex = 14 , skillCode = "SKI_014_EarthDrive" , skillName = "EarthDrive" , skillIconPath = "SKILLICON/96" , skillDescription = " Magic attacks with earth damage, and stunned."},
        new Skill(){skillIndex = 15 , skillCode = "SKI_015_FurySwip" , skillName = "FurySwip" , skillIconPath = "SKILLICON/84" , skillDescription = "Attacks with shadow damage and make enemy bleed 3 wound."},
        new Skill(){skillIndex = 16 , skillCode = "SKI_016_WeaponBreaking" , skillName = "WeaponBreaking" , skillIconPath = "SKILLICON/5" , skillDescription = "Disarm enemy and can't use attack skill. If enemy has weapon, Decreased weapon attack 50 percent."},
        new Skill(){skillIndex = 17 , skillCode = "SKI_018_TerraVortex" , skillName = "TerraVortex" , skillIconPath = "SKILLICON/21" , skillDescription = "Magic attacks with water damage, and Slow."},
        new Skill(){skillIndex = 18 , skillCode = "SKI_019_Slash" , skillName = "Slash" , skillIconPath = "SKILLICON/59" , skillDescription = " Attacks with shadow damage and make enemy bleed 2 wound an slow"},
        new Skill(){skillIndex = 19 , skillCode = "SKI_020_ThunderStrom" , skillName = "ThunderStrom" , skillIconPath = "SKILLICON/113" , skillDescription = "Magic attacks with wind damage, and stunned. Make enemy bleed 2 wound."},
        new Skill(){skillIndex = 20 , skillCode = "SKI_021_Explosion" , skillName = "Explosion" , skillIconPath = "SKILLICON/14" , skillDescription = "Magic attacks with fire damage,Make burn and bleed 2 wound."},
        new Skill(){skillIndex = 21 , skillCode = "SKI_022_TitanForm" , skillName = "TitanForm" , skillIconPath = "SKILLICON/111" , skillDescription = "Double all statuses"},
        new Skill(){skillIndex = 19 , skillCode = "SKI_023_Heal" , skillName = "Heal" , skillIconPath = "SKILLICON/110" , skillDescription = "Heal your self 20 percent of max HP"},
        new Skill(){skillIndex = 19 , skillCode = "SKI_024_PoisonWave" , skillName = "PoisonWave" , skillIconPath = "SKILLICON/26" , skillDescription = "Attacks with poision damage and enemy is poisioned."},

    };



    public static List<BuffAndDebuff> masterBuffList =  new List<BuffAndDebuff>(){
        new BuffAndDebuff(){Index = 0 , BuffName = "Stun" , BuffIconPath = "UI/Icon/BuffIcon/Stun" , BuffDescription = "Can't do every action. and pass your action 1 turn."},
        new BuffAndDebuff(){Index = 1 , BuffName = "Blind" , BuffIconPath = "UI/Icon/BuffIcon/Blind" , BuffDescription = "Decrease accuracy 90 percent."},
        new BuffAndDebuff(){Index = 2 , BuffName = "Silence" , BuffIconPath = "SKILLICON/Silence" , BuffDescription = "Can't use every magic skill except item."},
        new BuffAndDebuff(){Index = 3 , BuffName = "Burn" , BuffIconPath = "UI/Icon/BuffIcon/Burn" , BuffDescription = "Obtain Damage with 25 percent of attack damage."},
        new BuffAndDebuff(){Index = 4 , BuffName = "Bled" , BuffIconPath = "SKILLICON/Bled" , BuffDescription = "Obtain Damage when change position with 5 percent multiply of wound."},
        new BuffAndDebuff(){Index = 5 , BuffName = "Poison" , BuffIconPath = "SKILLICON/Poison" , BuffDescription = "Obtain Damage with 3 percent multiply of posion stack."},
        new BuffAndDebuff(){Index = 6 , BuffName = "Curse" , BuffIconPath = "SKILLICON/Curse" , BuffDescription = "This item for test only, So sorry honey jub jub!"},
        new BuffAndDebuff(){Index = 7 , BuffName = "Disarm" , BuffIconPath = "SKILLICON/Disarm" , BuffDescription = "Can't use weapon status "},


        new BuffAndDebuff(){Index = 11 , BuffName = "AttackUP" , BuffIconPath = "SKILLICON/Buff" , BuffDescription = "This item for test only, So sorry honey jub jub!"},
        new BuffAndDebuff(){Index = 12 , BuffName = "MagicUP" , BuffIconPath = "SKILLICON/Buff" , BuffDescription = "This item for test only, So sorry honey jub jub!"},
        new BuffAndDebuff(){Index = 13 , BuffName = "Guard" , BuffIconPath = "SKILLICON/Buff" , BuffDescription = "This item for test only, So sorry honey jub jub!"},
        new BuffAndDebuff(){Index = 14 , BuffName = "SpellGuard" , BuffIconPath = "SKILLICON/Buff" , BuffDescription = "This item for test only, So sorry honey jub jub!"},
        new BuffAndDebuff(){Index = 15 , BuffName = "Cure" , BuffIconPath = "SKILLICON/Buff" , BuffDescription = "This item for test only, So sorry honey jub jub!"},




    };    
}
