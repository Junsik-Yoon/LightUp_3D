using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianBattleStyle : BattleStyle
{
   Player player;
    Collider[] colls; //콜라이더 캐싱
    
    int skillIndex;
    public MagicianBattleStyle(Player player)
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
            player.StartCoroutine(player.MoveWhileAttack(0,(int)player.comboCounter,0.03f));
            player.StartCoroutine(player.ComboReset());
            skillIndex=0;           
        }
    }
    public override void Skill_A()
    {
        if(curSkillA.isPassive)return;
        if(!player.isCoolTimeA)
        {
            //colls = Physics.OverlapSphere(player.hitCollider.position,player.hitRadius,LayerMask.GetMask("Enemy"));
            player.anim.SetTrigger(curSkillA.animString);
            skillIndex=1;
            player.isCoolTimeA=true;
            player.StartCoroutine(player.CoolTimeResetA(curSkillA.coolTime));
            player.isOnSkill=true;   
            if(curSkillA.isMovingWhileOnSkill)
            {
                player.StartCoroutine(player.MoveWhileAttack(curSkillA.waitFor,0,curSkillA.moveDistance));
            }
        }
    }
    public override void Skill_B()
    {
        if(curSkillB.isPassive)return;
        if(!player.isCoolTimeB)
        {
            player.anim.SetTrigger(curSkillB.animString);
            skillIndex=2;
            player.isCoolTimeB=true;
            player.StartCoroutine(player.CoolTimeResetB(curSkillB.coolTime));
            player.isOnSkill=true;   
            if(curSkillB.isMovingWhileOnSkill)
            {
                player.StartCoroutine(player.MoveWhileAttack(curSkillB.waitFor,0,curSkillB.moveDistance));
            }
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
        else if (skillIndex == 1) {damageMultiplier = curSkillA.damageMultiplier; knockBackRange =curSkillA.knockBackRange;}
        else if (skillIndex == 2) {damageMultiplier = curSkillB.damageMultiplier; knockBackRange =curSkillB.knockBackRange;}
        else {damageMultiplier = 0f; knockBackRange = 0f;}
        colls = Physics.OverlapSphere(player.hitCollider.position,player.hitRadius,LayerMask.GetMask("Enemy"));
        if(colls.Length>0)
        {
            if(skillIndex ==0)
            {
                Enemy enemy = colls[0].gameObject.GetComponent<Enemy>();
                if(enemy.eState!=eEnemyState.DIE)
                {
                    enemy.Hit(player.damage * damageMultiplier,knockBackRange);
                   // Vector3 effectPos = new Vector3(player.hitCollider.transform.position.x,player.hitCollider.transform.position.y-1.4f,player.hitCollider.transform.position.z);
                    GameObject obj = GameObject.Instantiate(player.hitImpactEffect,player.hitCollider.transform.position,Quaternion.identity);
                    GameObject.Destroy(obj,0.5f);
//                    Debug.Log(player.damage * damageMultiplier);
                } 
            }
            else if(skillIndex ==1)
            {
                
                if(curSkillA.isTargetingSingle)
                {
                    Enemy enemy = colls[0].gameObject.GetComponent<Enemy>();
                    if(enemy.eState!=eEnemyState.DIE)
                    {
                        enemy.Hit(player.damage * damageMultiplier,knockBackRange);
                        if(curSkillA.prefEffectHit!=null)
                        {
                            Vector3 effectPos = new Vector3(player.hitCollider.transform.position.x,player.hitCollider.transform.position.y+1f,player.hitCollider.transform.position.z);
                            GameObject obj = GameObject.Instantiate(curSkillA.prefEffectHit,effectPos,Quaternion.identity);
                            GameObject.Destroy(obj,0.5f);
                        }       
                    } 
                }
                else
                {
                    for(int i=0; i<colls.Length;++i)
                    {    
                        Enemy enemy = colls[i].gameObject.GetComponent<Enemy>();
                        if(enemy.eState!=eEnemyState.DIE)
                        {
                            enemy.Hit(player.damage * damageMultiplier,knockBackRange);
                            if(curSkillA.prefEffectHit!=null)
                            {
                                Vector3 effectPos = new Vector3(player.hitCollider.transform.position.x,player.hitCollider.transform.position.y+1f,player.hitCollider.transform.position.z);
                                GameObject obj = GameObject.Instantiate(curSkillA.prefEffectHit,effectPos,Quaternion.identity);
                                GameObject.Destroy(obj,0.5f);
                            }                                   
                        } 
                    }
                }
            }
            else if(skillIndex ==2)
            {
                if(curSkillB.isTargetingSingle)
                {
                    Enemy enemy = colls[0].gameObject.GetComponent<Enemy>();
                    if(enemy.eState!=eEnemyState.DIE)
                    {
                        enemy.Hit(player.damage * damageMultiplier,knockBackRange);
                        if(curSkillB.prefEffectHit!=null)
                        {
                            Vector3 effectPos = new Vector3(player.hitCollider.transform.position.x,player.hitCollider.transform.position.y+1f,player.hitCollider.transform.position.z);
                            GameObject obj = GameObject.Instantiate(curSkillB.prefEffectHit,effectPos,Quaternion.identity);
                            GameObject.Destroy(obj,0.5f);
                        }                        
                    } 
                }
                else
                {
                    for(int i=0; i<colls.Length;++i)
                    {    
                        Enemy enemy = colls[i].gameObject.GetComponent<Enemy>();
                        if(enemy.eState!=eEnemyState.DIE)
                        {
                            enemy.Hit(player.damage * damageMultiplier,knockBackRange);
                            if(curSkillB.prefEffectHit!=null)
                            {
                                Vector3 effectPos = new Vector3(player.hitCollider.transform.position.x,player.hitCollider.transform.position.y+1f,player.hitCollider.transform.position.z);
                                GameObject obj = GameObject.Instantiate(curSkillB.prefEffectHit,effectPos,Quaternion.identity);
                                GameObject.Destroy(obj,0.5f);
                            }
                        } 
                    }
                }    
            }
        } 
        //else//스킬이펙트용
        {
            if(skillIndex ==1)
            {
                if(curSkillA.prefEffectOnGround!=null)
                {
                    Vector3 effectPos = new Vector3(player.hitCollider.transform.position.x,player.hitCollider.transform.position.y-1.4f,player.hitCollider.transform.position.z);
                    GameObject obj = GameObject.Instantiate(curSkillA.prefEffectOnGround,effectPos,Quaternion.identity);
                    GameObject.Destroy(obj,1.5f);
                }

            }
            else if(skillIndex ==2)
            {
                if(curSkillB.prefEffectOnGround!=null)
                {
                    Vector3 effectPos = new Vector3(player.hitCollider.transform.position.x,player.hitCollider.transform.position.y-1.4f,player.hitCollider.transform.position.z);
                    GameObject obj = GameObject.Instantiate(curSkillB.prefEffectOnGround,effectPos,Quaternion.identity);
                    GameObject.Destroy(obj,1.5f);
                }
            }

        }     
    } 
}

