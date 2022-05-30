using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingStep :SkillUse
{
    public override void UseSkill()
    {
        Player player = (GameObject.FindGameObjectWithTag("Player")).GetComponent<Player>();
        player.moveSpeed *= 1.5f;
    }
    public override void UnUSingSkill()
    {
        Player player = (GameObject.FindGameObjectWithTag("Player")).GetComponent<Player>();
        player.moveSpeed =player.defaultMoveSpeed;
        
    }

}