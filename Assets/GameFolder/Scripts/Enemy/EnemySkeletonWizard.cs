using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonWizard : Enemy
{
    public Vector3 minPos;
    public Vector3 maxPos;
    GameObject player;
    float distance;
    public Transform magicGenSpot;
    public GameObject magicBallParticle;

    GameObject instiatedMagicBall;
    public float hitRadius = 1f;
    private void Awake()
    {
        hp = 20f;
        damage = 5f;
        player = GameObject.FindGameObjectWithTag("Player");
        eState = eEnemyState.IDLE;
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        Invoke("Attack",4f);
    }
    private void Update()
    {
        if(eState==eEnemyState.DIE) return;
        Target(); 
    }

    public void Target()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;

        Vector3 dirVec = (playerPos - enemyPos).normalized;
        Vector3 fixedDirVec = new Vector3(dirVec.x,0,dirVec.z);

        transform.rotation= Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(fixedDirVec),0.02f);//,transform.up);
    }

    public void Attack()
    {  

        StartCoroutine(MoveAndHit());

        Invoke("Attack",4f);

    }

    Vector3 savedDirVec;
    IEnumerator MoveAndHit()
    {
        Debug.Log(minPos);
        Debug.Log(maxPos);
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3(Random.Range(minPos.x,maxPos.x),0f,Random.Range(minPos.z,maxPos.z));
        
        yield return new WaitForSeconds(0.1f);
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        Vector3 dirVec = (playerPos - enemyPos).normalized;
        Vector3 fixedDirVec = new Vector3(dirVec.x,0,dirVec.z);
        transform.rotation= Quaternion.LookRotation(fixedDirVec);
        savedDirVec = fixedDirVec;
        // distance = (playerPos-enemyPos).magnitude;
        // float distanceTic = distance * 0.1f;

        instiatedMagicBall = Instantiate(magicBallParticle, magicGenSpot);
        anim.SetTrigger("Hit");
        
    }
    public void OnAttackEvent()
    {
        if(instiatedMagicBall!=null)
        {
            MagicBall magicBall = instiatedMagicBall?.GetComponent<MagicBall>();

            magicBall.transform.parent =null;
            magicBall.dir = savedDirVec;
            magicBall.isMovable = true;
            magicBall.damage = damage;
        }



    }



    public override void Hit(float damage , float knockBackRange)
   {
       hp -= damage;
       if(eState!=eEnemyState.DIE) anim.SetTrigger("Damage");
       StartCoroutine(Hurt(0.3f,knockBackRange,eState));
       eState = eEnemyState.HURT;
       if(hp<=0) {Dead(); return;}
   }
    public override void Dead()
    {
        base.Dead();
        GameObject.Find("PlayerSpotLight").GetComponent<PlayerLight>().LightUp(20f);
        GetComponent<SphereCollider>().enabled=false;
        eState = eEnemyState.DIE;
        anim.SetTrigger("Die");
        Destroy(gameObject,2f);
        CancelInvoke();
        MoneyManager.instance.MoneyDrop(transform,30);

        
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