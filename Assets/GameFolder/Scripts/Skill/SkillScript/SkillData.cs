using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public SkillUse skillUse;
    public int skillID;
    new public string name = "SkillName";
    public bool isPassive;
    public bool isTargetingSingle;
    public bool isMovingWhileOnSkill;
    
    public float waitFor;
//    public int combo;
    public float moveDistance;
    public float coolTime;
    public float damageMultiplier;
    public float knockBackRange;
    public Sprite icon = null;
    public GameObject prefEffectHit = null;
    public GameObject prefEffectOnGround = null;
    public GameObject prefEffectOnHand =null;
    [TextArea]
    public string skillDescription = "";
    public string animString = "";
    public abstract void Use();

}
