using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonMelee : Enemy
{
    GameObject player;
    float distance;
    public Transform hitCollider;
    public float hitRadius = 1f;
    private void Awake()
    {
        hp = 20f;
        damage = 2f;
        player = GameObject.FindGameObjectWithTag("Player");
        eState = eEnemyState.TRACE;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        Trace();  
        Attack();  
        
    }
    public void Trace()
    {
        
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        distance = (playerPos-enemyPos).magnitude;
        if(eState != eEnemyState.TRACE) return;
        anim.SetTrigger("onTrace");
        if(distance <=2f)
        {           
           eState = eEnemyState.ATTACK;
        }
        Vector3 dirVec = (playerPos - enemyPos).normalized;
       
        transform.LookAt(playerPos);
        transform.Translate(dirVec * moveSpeed * Time.deltaTime, Space.World);
        
    }
    public void Attack()
    {  
        if(eState != eEnemyState.ATTACK) return;
        anim.SetTrigger("onAttack");
        if(distance >= 2f)
        {
            eState = eEnemyState.TRACE;
        }
    }
    public void OnAttackEvent()
    {
        Collider[] colls = Physics.OverlapSphere(hitCollider.position,hitRadius,LayerMask.GetMask("Player"));
        if(colls.Length>0) colls[0]?.GetComponent<Player>().Damaged(damage);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        Gizmos.DrawWireSphere(hitCollider.position,hitRadius);
    }

    public override void Hit(float damage , float knockBackRange)
   {
       hp -= damage;
       if(eState!=eEnemyState.DIE) anim.SetTrigger("onDamaged");
       StartCoroutine(Hurt(0.3f,knockBackRange,eState));
       eState = eEnemyState.HURT;
       if(hp<=0) {Dead(); return;}
   }
    public override void Dead()
    {
      //  if(SceneManager.GetActiveScene().name!="Tutorial") 
        base.Dead();
        GameObject.Find("PlayerSpotLight").GetComponent<PlayerLight>().LightUp(10f);
        GetComponent<CapsuleCollider>().enabled=false;
        eState = eEnemyState.DIE;
        anim.SetTrigger("onDead");
        Destroy(gameObject,2f);
        
        MoneyManager.instance.MoneyDrop(transform,20);
        
    }
    IEnumerator Hurt(float animTime ,float knockBackRange,eEnemyState prevState)
    {
        Outline outline = GetComponent<Outline>();
        Color prevColor = outline.OutlineColor;
        outline.OutlineWidth = 1f;
        outline.OutlineColor = Color.red;
        for(int i=0; i<5; ++i)
        {
            yield return new WaitForSeconds(animTime/10f);
            transform.Translate(Vector3.back*knockBackRange/5f);   
        }
        outline.OutlineWidth = 0f;
        outline.OutlineColor = prevColor;
        yield return new WaitForSeconds(animTime/2f);
       
        if(hp>0) eState = prevState;
    }   
}

