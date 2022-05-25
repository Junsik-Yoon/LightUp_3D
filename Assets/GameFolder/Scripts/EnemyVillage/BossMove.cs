using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMove : Enemy
{
    public Light bossLight;
    public GameObject spikeEffect;
    public GameObject slashEffect;
    public GameObject tauntEffect;
    public GameObject skillIndicator;
    public GameObject pierceEffect;
    float maxHP;
    GameObject player;
    Vector3 playerPrevPos;
    float distance;
    public Transform hitCollider;
    public Transform boxHit;
    public float hitRadius = 1f;
    private void Awake()
    {
        hp = 100f;
        maxHP = hp;
        damage = 5f;
        player = GameObject.FindGameObjectWithTag("Player");
        eState = eEnemyState.IDLE;
        anim = GetComponent<Animator>();
        
    }
    private void Start()
    {
        BossBattleManager.instance.OnChangePhase+=PhaseChanged;
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
    public void PhaseChanged()
    {
        if(BossBattleManager.instance.phase==2)
        {
             anim.SetTrigger("OnPhase2");
        }
        else if( BossBattleManager.instance.phase==3)
        {
            transform.position = new Vector3(0,20,0);
            Rigidbody rigidBody = GetComponent<Rigidbody>();
            rigidBody.isKinematic=false;
            rigidBody.AddForce(Vector3.down*100f);
            StartCoroutine(LandOnGround());
        }
    }
    IEnumerator LandOnGround()
    {
        //GameObject indicator = Instantiate(skillIndicator,new Vector3(0,0.1f,0),Quaternion.identity);
        //Destroy(indicator,2f);
        yield return new WaitForSeconds(1.5f);
        anim.Play("BossLanding");
        Collider[] playerColls = Physics.OverlapSphere(transform.position,10f,LayerMask.GetMask("Player"));
        if(playerColls.Length>0)
        {
            playerColls[0]?.GetComponent<Player>().Stunned(transform.position,0.5f,5f);
        }
        BossBattleManager.instance.phase=4;
        Invoke("Attack",3f);
    }
    public void ShockWave()
    {
        GameObject shockWave = Instantiate(tauntEffect,transform.position,Quaternion.identity);
        Destroy(shockWave,3f);
    }
    public void JumpOutside()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic=false;
        rigidBody.AddForce(Vector3.up*3000f);
    }
    public void StayOut()
    {
        //GetComponent<Rigidbody>().AddForce(Vector3.up*3000f);
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic=true;
        transform.position= new Vector3(-10f,1f,-10f);
    }
    private void Update()
    {
        if(eState==eEnemyState.DIE) return;

        IDLE();
       // Target();  
       // Attack();  
        
    }
    public void IDLE()
    {      
        if(BossBattleManager.instance.phase==2||
        BossBattleManager.instance.phase==3)return;

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
        Vector3 fixedDirVec = new Vector3(dirVec.x,0,dirVec.z);
       // if(fixedDirVec.x>0) anim.SetTrigger("TurnLeft");
       // else if(fixedDirVec.x<0) anim.SetTrigger("TurnRight");
       
        transform.rotation= Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(fixedDirVec),0.02f);//,transform.up);
       // transform.rotation= Quaternion.LookRotation(fixedDirVec);
        //transform.LookAt(playerPos);
        //transform.Translate(dirVec * moveSpeed * Time.deltaTime, Space.World);
       
    }
    public void Attack()
    {  
        if(BossBattleManager.instance.phase==2||
        BossBattleManager.instance.phase==3)return;

        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        Vector3 dirVec = (playerPos - enemyPos).normalized;
        Vector3 fixedDirVec = new Vector3(dirVec.x,0,dirVec.z);
        transform.rotation= Quaternion.LookRotation(fixedDirVec);

        distance = (playerPos-enemyPos).magnitude;
        int index = Random.Range(0,2);
        if(BossBattleManager.instance.phase !=4)
        {
            if(distance <=2.5f)
            {           
                if(index ==0)
                {
                    anim.SetTrigger("Attack3");
                }
                else if (index == 1)
                {
                    anim.SetTrigger("Attack4");
                }
            }
            else
            {
                StartCoroutine(MoveForward());
                if(index ==0)
                {
                    anim.SetTrigger("Attack1");
                }
                else if (index == 1)
                {
                anim.SetTrigger("Attack2");
                }
            }
            Invoke("Attack",5f);
        }
        else
        {
            anim.SetTrigger("Attack5");
            playerPrevPos = player.transform.position;
            
            Invoke("Attack",3f);
        }
        
        

    }
    IEnumerator MoveForward()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        Vector3 dirVec = (playerPos - enemyPos).normalized;
        Vector3 fixedDirVec = new Vector3(dirVec.x,0,dirVec.z);
        
        for(int i=0; i<100; ++i)
        {
            yield return new WaitForEndOfFrame();
            transform.Translate( fixedDirVec * 5f *Time.deltaTime,Space.World);
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
        if(colls.Length>0)
        {
            colls[0]?.GetComponent<Player>().Stunned(transform.position,0.1f,1.5f);
        }
    }
    public void OnAttack2Event()
    {
        Collider[] colls = Physics.OverlapSphere(hitCollider.position,hitRadius,LayerMask.GetMask("Player"));
        if(colls.Length>0) colls[0]?.GetComponent<Player>().Damaged(damage);
        GameObject obj = Instantiate(slashEffect,transform.position,Quaternion.identity);
        obj.transform.rotation = Quaternion.LookRotation(transform.forward);
        obj.transform.parent = transform.GetChild(1).GetChild(0) ;
        Destroy(obj,2f);
        if(colls.Length>0)
        {
            colls[0]?.GetComponent<Player>().Stunned(transform.position,0.1f,1.5f);
        }
    }
    public void OnAttack5Event()
    {

        
        
        //Collider[] colls = Physics.OverlapBox(boxHit.position,new Vector3(1f,1f,5),Quaternion.identity,LayerMask.GetMask("Player"));
        //Collider[] colls = Physics.OverlapSphere(hitCollider.position,hitRadius,LayerMask.GetMask("Player"));
        //Debug.DrawLine()
        //if(colls.Length>0) colls[0]?.GetComponent<Player>().Damaged(damage);
       
        GameObject obj = Instantiate(pierceEffect,hitCollider.position,Quaternion.identity);
        obj.transform.rotation = Quaternion.LookRotation(transform.forward);
        Destroy(obj,1f);
        RaycastHit[] hit = Physics.SphereCastAll(transform.position,2f,(playerPrevPos - transform.position).normalized,20f,LayerMask.GetMask("Player"));
        if(hit.Length>0)
        {
            hit[0].collider.gameObject.GetComponent<Player>().Stunned(transform.position,0.1f,1.5f);
        }
            //obj.transform.parent = transform.GetChild(1).GetChild(0) ;
            
            //Debug.Log(raycastHit);
            //raycastHit.
            

        
        // if(colls.Length>0)
        // {
            
        // }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        Gizmos.DrawWireSphere(hitCollider.position,hitRadius);
        
       
    }

    public override void Hit(float damage , float knockBackRange)
   {
       if(BossBattleManager.instance.phase==2)return;
       hp -= damage;
       if(eState!=eEnemyState.DIE) anim.SetTrigger("onDamaged");
       StartCoroutine(Hurt(0.3f,knockBackRange,eState));
       eState = eEnemyState.HURT;
       if(hp<=maxHP/2 && BossBattleManager.instance.phase==0)
       {
           BossBattleManager.instance.phase=2;
           
       } 
       if(hp<=0) {Dead(); return;}
   }
   
    public override void Dead()
    {
        base.Dead();
        GameObject.Find("PlayerSpotLight").GetComponent<PlayerLight>().LightUp(100f);
        GetComponent<CapsuleCollider>().enabled=false;
        eState = eEnemyState.DIE;
        anim.SetTrigger("OnDead");
        GetComponent<Rigidbody>().useGravity=false;
        
        CancelInvoke();
        //Destroy(gameObject,2f);
        
        BossBattleManager.instance.StartCoroutine(BossBattleManager.instance.ShowBattleResult());
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
