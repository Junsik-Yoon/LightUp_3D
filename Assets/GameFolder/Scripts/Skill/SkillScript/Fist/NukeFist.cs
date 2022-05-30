using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeFist : SkillUse
{
    public override void UseSkill()
    {
        GameObject obj = GameObject.Find("FistEffect");
        obj.transform.GetChild(0).gameObject.SetActive(true);
        Player player = (GameObject.FindGameObjectWithTag("Player")).GetComponent<Player>();
        player.damage *= 1.1f;
    }
    public override void UnUSingSkill()
    {
        Player player = (GameObject.FindGameObjectWithTag("Player")).GetComponent<Player>();
        player.damage = player.defaultDamage;    
    }
}