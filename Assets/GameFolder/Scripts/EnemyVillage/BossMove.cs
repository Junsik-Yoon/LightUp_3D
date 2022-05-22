using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMove : Enemy
{
    public GameObject spikeEffect;
    public GameObject slashEffect;
    public GameObject tauntEffect;
    int phase=0;
    GameObject player;
    float distance;
    public Transform hitCollider;
    public float hitRadius = 1f;
    private void Awake()
    {
        hp = 100f;
        damage = 5f;
        player = GameObject.FindGameObjectWithTag("Player");
        eState = eEnemyState.IDLE;
        anim = GetComponent<Animator>();
        phase = 1;
    }
    private void Start()
    {
        
        anim.Play("BossTauntCollider");
    }
    public void OnTaunt()
    {
        GameObject obj = Instantiate(tauntEffect,transform.position,Quaternion.identity);
        Destroy(obj,3f);
        Collider[] colls = Physics.OverlapSphere(transform.position,10f,LayerMask.GetMask("Destroyable"));
        Collider[] playerColls = Physics.OverlapSphere(transform.position,10f,LayerMask.GetMask("Player"));
        //Debug.Log(colls.Length);
        // for(int i=0; i<colls.Length;++i)
        // {
        //     Debug.Log(colls[i].gameObject.name);
        // }
        if(colls.Length>0) 
        {
            for(int i=0; i<colls.Length;++i)
            {
                (colls[i]?.gameObject.GetComponent<SelfDestroyScript>()).BeingDestroyed(transform.position);
            }
        }
        if(playerColls.Length>0)
        {
            playerColls[0]?.GetComponent<Player>().Stunned(transform.position,2f,6f);
        }

        Invoke("Attack",5f);
    }
    private void Update()
    {
        IDLE();
       // Target();  
       // Attack();  
        
    }
    public void IDLE()
    {
        Target();
    }
    public void Target()
    {
        
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        // distance = (playerPos-enemyPos).magnitude;
        // if(eState != eEnemyState.TRACE) return;
        // anim.SetTrigger("onTrace");
        // if(distance <=2.5f)
        // {           
        //    eState = eEnemyState.ATTACK;
        // }

        Vector3 dirVec = (playerPos - enemyPos).normalized;
        Vector3 fixedDirVec = new Vector3(dirVec.x,transform.position.y,dirVec.z);
       // if(fixedDirVec.x>0) anim.SetTrigger("TurnLeft");
       // else if(fixedDirVec.x<0) anim.SetTrigger("TurnRight");
       
        transform.rotation= Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(fixedDirVec),0.02f);//,transform.up);
       // transform.rotation= Quaternion.LookRotation(fixedDirVec);
        //transform.LookAt(playerPos);
        //transform.Translate(dirVec * moveSpeed * Time.deltaTime, Space.World);
       
    }
    public void Attack()
    {  
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        Vector3 dirVec = (playerPos - enemyPos).normalized;
        Vector3 fixedDirVec = new Vector3(dirVec.x,transform.position.y,dirVec.z);
        transform.rotation= Quaternion.LookRotation(fixedDirVec);

        StartCoroutine(MoveForward());
        
        Invoke("Attack",5f);
       // if(eState != eEnemyState.ATTACK) return;
       int index = Random.Range(0,2);
       if(index ==0)
       {
           anim.SetTrigger("Attack1");
       }
       else if (index == 1)
       {
           anim.SetTrigger("Attack2");
       }
        
        // if(distance >= 2.5f)
        // {
        //     eState = eEnemyState.TRACE;
        // }
    }
    IEnumerator MoveForward()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        Vector3 dirVec = (playerPos - enemyPos).normalized;
        
        for(int i=0; i<100; ++i)
        {
            yield return new WaitForEndOfFrame();
            transform.Translate( dirVec * 5f *Time.deltaTime,Space.World);
        }

    }
    public void OnAttackEvent()
    {
        // Vector3 playerPos = player.transform.position;
        // Vector3 enemyPos = transform.position;
        // Vector3 dirVec = (playerPos - enemyPos).normalized;
        Collider[] colls = Physics.OverlapSphere(hitCollider.position,hitRadius,LayerMask.GetMask("Player"));
        if(colls.Length>0) colls[0]?.GetComponent<Player>().Damaged(damage);
        GameObject obj = Instantiate(spikeEffect,hitCollider.transform.position,Quaternion.identity);
        obj.transform.rotation = Quaternion.LookRotation(transform.forward);
        Destroy(obj,1.3f);
    }
    public void OnAttack2Event()
    {
        Collider[] colls = Physics.OverlapSphere(hitCollider.position,hitRadius,LayerMask.GetMask("Player"));
        if(colls.Length>0) colls[0]?.GetComponent<Player>().Damaged(damage);
        GameObject obj = Instantiate(slashEffect,transform.position,Quaternion.identity);
        obj.transform.rotation = Quaternion.LookRotation(transform.forward);
        obj.transform.parent = transform.GetChild(1).GetChild(0) ;
        Destroy(obj,2f);
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
        base.Dead();
        GameObject.Find("PlayerSpotLight").GetComponent<PlayerLight>().LightUp(10f);
        GetComponent<SphereCollider>().enabled=false;
        eState = eEnemyState.DIE;
        anim.SetTrigger("onDead");
        Destroy(gameObject,2f);
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            GameObject.Find("RegenSpot").GetComponent<GenerateMonster>().GenMonster();
        }
        
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
        outline.OutlineWidth = 0.1f;
        outline.OutlineColor = prevColor;
        yield return new WaitForSeconds(animTime/2f);
       
        if(hp>0) eState = prevState;
    }   
}
