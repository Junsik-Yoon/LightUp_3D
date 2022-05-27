using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHiddenBoss : Enemy
{
    [SerializeField] private AnimatorControllerParameterType paramType;
    [SerializeField] private string nameString;
    [SerializeField] private float _value;
    GameObject player;
    float distance;
    public Transform hitCollider;
    public GameObject fireExplosionEffect;
    float onCoolTime=1f;
    public float hitRadius = 1f;
    private void Awake()
    {
        hp = 50f;
        damage = 10f;
        player = GameObject.FindGameObjectWithTag("Player");
        eState = eEnemyState.IDLE;
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        Invoke("Attack",3f);
    }
    private void Update()
    {
        if(eState==eEnemyState.DIE) return;
        Target();
       // Attack();  
        
    }

     public void Target()
    {
        
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;

        Vector3 dirVec = (playerPos - enemyPos).normalized;
        Vector3 fixedDirVec = new Vector3(dirVec.x,0,dirVec.z);

        transform.rotation= Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(fixedDirVec),0.02f);//,transform.up);

       
    }

    // public void StartRoutine()
    // {

    //     StartCoroutine(StartAttack());
    // }

    public void Attack()
    {  
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        Vector3 dirVec = (playerPos - enemyPos).normalized;
        Vector3 fixedDirVec = new Vector3(dirVec.x,0,dirVec.z);
        transform.rotation= Quaternion.LookRotation(fixedDirVec);

        distance = (playerPos-enemyPos).magnitude;

        if(distance > 6f)
        {
            StartCoroutine(Fire(playerPos));
        }
        else
        {
            StartCoroutine(MoveAndHit(dirVec,distance));
        }
        


        Invoke("Attack",3f);

    }
    Vector3 playerPosSaved;
    IEnumerator Fire(Vector3 playerPos)
    {
        yield return new WaitForSeconds(1f);
        playerPosSaved = new Vector3(playerPos.x,playerPos.y+1f,playerPos.z);
        GameObject obj = Instantiate(fireExplosionEffect,playerPosSaved,Quaternion.identity);
        Destroy(obj,2f);
        yield return new WaitForSeconds(0.7f);
        OnExplodeEvent();
    }
    IEnumerator MoveAndHit(Vector3 dir , float distance)
    {
        float distanceTic = distance * 0.1f;
        for(int i=0; i<6; ++i)
        {
            yield return new WaitForEndOfFrame();
            transform.Translate(dir*distanceTic,Space.World);
        }
        anim.SetTrigger("Hit");
        
    }
    public void OnAttackEvent()
    {
        Collider[] colls = Physics.OverlapSphere(hitCollider.position,hitRadius,LayerMask.GetMask("Player"));
        if(colls.Length>0) colls[0]?.GetComponent<Player>().Damaged(damage);
       // GameObject obj = Instantiate(slashEffect,transform.position,Quaternion.identity);
       // obj.transform.rotation = Quaternion.LookRotation(transform.forward);
       // obj.transform.parent = transform.GetChild(1).GetChild(0) ;
       // Destroy(obj,2f);
        if(colls.Length>0)
        {
            colls[0]?.GetComponent<Player>().Stunned(transform.position,0.1f,5f);
        }
    }
    public void OnExplodeEvent()
    {
        Collider[] colls = Physics.OverlapSphere(playerPosSaved,hitRadius*1.8f,LayerMask.GetMask("Player"));
        if(colls.Length>0) colls[0]?.GetComponent<Player>().Damaged(damage);
        if(colls.Length>0)
        {
            colls[0]?.GetComponent<Player>().Stunned(transform.position,0.1f,0.5f);
        }        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        Gizmos.DrawWireSphere(hitCollider.position,hitRadius);
        //if(playerPosSaved !=null) Gizmos.DrawWireSphere(playerPosSaved,hitRadius*2f);
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
        GetComponent<CapsuleCollider>().enabled=false;
        eState = eEnemyState.DIE;
        anim.SetTrigger("Die");
        Destroy(gameObject,3f);
        CancelInvoke();
        MoneyManager.instance.MoneyDrop(transform,100);

        
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
    


        private void InvokeTrigger()
    {
        switch (paramType)
        {
            case AnimatorControllerParameterType.Trigger:
                anim.SetTrigger(nameString);
                break;

            case AnimatorControllerParameterType.Float:
                anim.SetFloat(nameString, _value);
                break;
        }
    }

    private void ResetTriggers()
    {
        foreach (var parameter in anim.parameters)
        {
            switch (parameter.type)
            {
                case AnimatorControllerParameterType.Trigger:
                    anim.ResetTrigger(parameter.name);
                    break;

                case AnimatorControllerParameterType.Float:
                    anim.SetFloat(parameter.name, 0f);
                    break;
            }
        }
    }
}
