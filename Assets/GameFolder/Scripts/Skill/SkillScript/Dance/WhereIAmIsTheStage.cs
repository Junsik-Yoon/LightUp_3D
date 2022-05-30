using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereIAmIsTheStage : SkillUse
{
    public override void UseSkill()
    {
        Player player = (GameObject.FindGameObjectWithTag("Player")).GetComponent<Player>();
        PlayerLight playerLight = player.playerLight;
        player.damage *= 1.1f;
        SkillManager.instance.StartCoroutine(SkillManager.instance.ClubLight(playerLight));
        
    }
    public override void UnUSingSkill()
    {
        Player player = (GameObject.FindGameObjectWithTag("Player")).GetComponent<Player>();
        player.damage = player.defaultDamage;    
    }

}
