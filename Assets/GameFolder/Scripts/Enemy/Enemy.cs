using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eEnemyState
{
    IDLE,
    PATROL,
    TRACE,
    ATTACK,
    APART,
    HURT,
    DIE,

    SIZE,

}
public class Enemy : MonoBehaviour , IDamagable
{
    public Animator anim;
   public float hp;
   public float damage = 2f;
   public float moveSpeed = 5f;
   public eEnemyState eState = eEnemyState.IDLE;
   public virtual void Hit(float damage,float knockBackRange)
   {
       hp -= damage;
       if(hp<=0) Dead();
   }
   public virtual void Dead()
   {
       if(gameObject.tag!="Boss")
       {
            BattleStageManager.instance.cashingDeadEnemyPos = transform.position;
            BattleStageManager.instance.enemyCount -=1;
            BattleStageManager.enemyKilled+=1;
       }
       else
       {

       }
      
   }
   
}
