using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistBattleStyle : BattleStyle
{
    Player player;
    Collider[] colls;
    
    int skillIndex;
    public FistBattleStyle(Player player)
    {
        this.player = player;
    }
    public override void Attack()
    {
        if(player.comboCounter<4)
        {
            colls = Physics.OverlapSphere(player.hitCollider.position,player.hitRadius,LayerMask.GetMask("Enemy"));
            ++player.comboCounter;
            player.anim.SetTrigger("onAttack");
            player.StartCoroutine(player.MoveWhileAttack((int)player.comboCounter));
            player.StartCoroutine(player.ComboReset());
            skillIndex=0;
            
        }
    }
    public override void Skill_A()
    {
        if(!player.isCoolTimeA)
        {
            colls = Physics.OverlapSphere(player.hitCollider.position,player.hitRadius,LayerMask.GetMask("Enemy"));
            player.anim.SetTrigger("onSkillA");
            skillIndex=1;
            player.isCoolTimeA=true;
            player.StartCoroutine(player.CoolTimeResetA(5f));
            player.isOnSkill=true;   
        }
    }
    public override void Skill_B()
    {
        if(!player.isCoolTimeB)
        {
            //Physics.SphereCast()
            colls = Physics.OverlapSphere(player.hitCollider.position,player.hitRadius,LayerMask.GetMask("Enemy"));
            player.anim.SetTrigger("onSkillB");
            skillIndex=2;
            player.isCoolTimeB=true;
            player.StartCoroutine(player.CoolTimeResetB(8f));
            player.isOnSkill=true;   
        }
    }
    public override void Dodge()
    {
        if(!player.isCoolTimeDodge)
        {
            player.anim.SetTrigger("OnDodge");
            player.isCoolTimeDodge=true;
            player.StartCoroutine(player.CoolTimeResetDodge(0.5f));  
            player.isInvincible = true; 
            player.isOnSkill=true;
        }
    }
    public override void OnHit()
    {
        float damageMultiplier;
        float knockBackRange;
        if(skillIndex == 0) {damageMultiplier = 1f; knockBackRange =0.5f;}
        else if (skillIndex == 1) {damageMultiplier = 2f; knockBackRange =3f;}
        else if (skillIndex == 2) {damageMultiplier = 3f; knockBackRange =2.5f;}
        else {damageMultiplier = 0f; knockBackRange = 0f;}

        if(colls.Length>0)
        {
            if(skillIndex ==0)
            {
                Enemy enemy = colls[0].gameObject.GetComponent<Enemy>();
                if(enemy.eState!=eEnemyState.DIE)
                {
                    enemy.Hit(player.damage * damageMultiplier,knockBackRange);
//                    Debug.Log(player.damage * damageMultiplier);
                } 
            }
            else if(skillIndex ==1)
            {
                for(int i=0; i<colls.Length;++i)
                {
                    Enemy enemy = colls[i].gameObject.GetComponent<Enemy>();
                    if(enemy.eState!=eEnemyState.DIE)
                    {
                        enemy.Hit(player.damage * damageMultiplier,knockBackRange);
                        Debug.Log(player.damage * damageMultiplier);
                        
                    } 

                }

            }
            else if(skillIndex ==2)
            {
                for(int i=0; i<colls.Length;++i)
                {
                    Enemy enemy = colls[i].gameObject.GetComponent<Enemy>();
                    if(enemy.eState!=eEnemyState.DIE)
                    {
                        enemy.Hit(player.damage * damageMultiplier,knockBackRange);
                    //    Debug.Log(player.damage * damageMultiplier);
                    } 
                }      
            }
        } 
        //else//스킬이펙트용
        {
            if(skillIndex ==1)
            {
                Vector3 effectPos = new Vector3(player.hitCollider.transform.position.x,player.hitCollider.transform.position.y-1.4f,player.hitCollider.transform.position.z);
                GameObject obj = GameObject.Instantiate(player.smashEffect,effectPos,Quaternion.identity);
                //Debug.Log(obj);
                GameObject.Destroy(obj,1.5f);
            }

        }     
    } 
}
