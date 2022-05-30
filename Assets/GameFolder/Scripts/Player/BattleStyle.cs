using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleStyle
{
    public SkillData curSkillA;
    public SkillData curSkillB;
    public abstract void Attack();
    public abstract void Skill_A();
    public abstract void Skill_B();
    public abstract void Dodge();
    public abstract void OnHit();
}
