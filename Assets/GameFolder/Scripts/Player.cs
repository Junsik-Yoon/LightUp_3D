using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public event UnityAction OnCoolTime;
    public PlayerLight playerLight;
    public float damage = 5f;
    public Transform hitCollider;
    public float hitRadius = 5f;
    public uint comboCounter = 0;
    public bool isCoolTimeA=false;
    public bool isCoolTimeB=false;
    public bool isCoolTimeDodge = false;
    private float _coolTimeA = 0;
    public float coolMaxA;
    public float coolTimeA
    {
        get
        {
            return _coolTimeA;
        }
        set
        {
              
            _coolTimeA = value;
            OnCoolTime?.Invoke();       
        }
    }
    private float _coolTimeB = 0;
    public float coolMaxB;
    public float coolTimeB
    {
        get
        {
            return _coolTimeB;
        }
        set
        {
              
            _coolTimeB = value;
            OnCoolTime?.Invoke();       
        }
    }
    private float _coolTimeDodge =0;
    public float coolMaxDodge;
    public float coolTimeDodge
    {
        get
        {
            return _coolTimeDodge;
        }
        set
        {
            _coolTimeDodge = value;
            OnCoolTime?.Invoke();
        }
    }
    public float moveSpeed = 5f;

    public bool isInvincible=false;
    public Animator anim;
    public CharacterController characterController;
    public NavMeshAgent navMeshAgent;
    public Camera playerCamera;
    private MoveCommand moveCommand;
    private BattleStyle battleStyle;
    public bool isOnSkill=false;

    Iinteractable colliderSaver;
    IEnumerator temp;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        battleStyle = new FistBattleStyle(this);
        if( SceneManager.GetActiveScene().name == "VillageScene")
        {
            anim.applyRootMotion = false;
            characterController.enabled = false;
            moveCommand = new MouseMoveCommand(this);
        }
        else
        {
            moveCommand = new NormalMoveCommand(this);//맨마지막
        }
    }
    private void Update()
    {
        Interact();
        Move();
        if(!isOnSkill)//스킬애니메이션 중 다른 업데이트 실행 안되도록 분리
        {   
            Dodge();
            Attack();
            Skill_A();
            Skill_B();
        }

    }
    public void Move()
    {
        if(comboCounter>0) return;
        moveCommand.Execute();
    }
    public void Interact()
    {       
        Collider[] colls = Physics.OverlapSphere(hitCollider.position,hitRadius,LayerMask.GetMask("Event"));
        if(colls.Length>0)
        {
            colliderSaver = colls[0].GetComponent<Iinteractable>();
            InventoryManager.instance.SetInformation(colls[0]);
            colliderSaver.Interact();//TODO: 여기서 에러 왜났는지 확인하기
        }
        else
        {
            if(colliderSaver!=null) colliderSaver.InteractFinished();
        }

        
 
    }
    public void Attack()
    {
        if(Input.GetButtonDown("Command1")) battleStyle.Attack();
    }
    public void Skill_A()
    {
        if(Input.GetButtonDown("Command2")) battleStyle.Skill_A();
    }
    public void Skill_B()
    {
        if(Input.GetButtonDown("Command3")) battleStyle.Skill_B();
    }
    public void Dodge()
    {
        if(Input.GetButtonDown("Dodge")) battleStyle.Dodge();
    }

    public IEnumerator ComboReset()
    {
        yield return new WaitForSeconds(1.1f);
        comboCounter = 0;
    }
    public IEnumerator CoolTimeResetA(float coolTime)
    {
        coolMaxA = coolTime;
        float _coolTime = coolTime;
        for (int i=0; i<100; ++i)
        {  
            yield return new WaitForSeconds(coolTime/100);
            _coolTime -= (coolTime/100);
            coolTimeA = _coolTime;
        }
        isCoolTimeA=false;
    }
    public IEnumerator CoolTimeResetB(float coolTime)
    {
        coolMaxB = coolTime;
        float _coolTime = coolTime;
        for (int i=0; i<100; ++i)
        {  
            yield return new WaitForSeconds(coolTime/100);
            _coolTime -= (coolTime/100);
            coolTimeB = _coolTime;
        }
        isCoolTimeB=false;
    }
    public IEnumerator CoolTimeResetDodge(float coolTime)
    {
        coolMaxDodge = coolTime;
        float _coolTime = coolTime;
        for (int i=0; i<100; ++i)
        {  
            yield return new WaitForSeconds(coolTime/100);
            _coolTime -= (coolTime/100);
            coolTimeDodge = _coolTime;
        }
        isCoolTimeDodge=false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitCollider.position,hitRadius);
    }
    public void OnHit()
    {
        battleStyle.OnHit();
    }
    public void OnSkillButton(string btnType)
    {
        switch(btnType)
        {
            case "ATTACK":
            {
                battleStyle.Attack();
            }break;
            case "SKILL_A":
            {
                battleStyle.Skill_A();
            }break;
            case "SKILL_B":
            {
                battleStyle.Skill_B();
            }break;
            case "Dodge":
            {
               battleStyle.Dodge();
            }break;
        }
    }
    public void Damaged(float damage)
    {
        if(!isInvincible)
        {
            StartCoroutine(DamageEffect());
            playerLight.playerLight.spotAngle -= damage;
        }
    }
    public void AnimMoveForward(float moveSpeed)
    {
        Outline outline = GetComponent<Outline>();
        outline.OutlineWidth = 5f;
        isInvincible=true;
        temp = MoveOnAnim(moveSpeed);
        StartCoroutine(temp);
    }
    public void AnimStop()
    {
        isInvincible=false;
        StopCoroutine(temp);
        Outline outline = GetComponent<Outline>();
        outline.OutlineWidth = 0.1f;
        anim.SetBool("isDodge",false);
        SkillEnded();
    }
    public void SkillEnded()
    {
        isOnSkill=false;
    }
    public IEnumerator MoveOnAnim(float moveSpeed)
    {
        while(true)
        {
            //transform.Translate(Vector3.forward*moveSpeed);
            characterController.Move(transform.forward*moveSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator DamageEffect()
    {
//        Debug.Log("Damage");
        Outline outline = GetComponent<Outline>();
        Color prevColor = outline.OutlineColor;
        outline.OutlineWidth = 1f;
        outline.OutlineColor = Color.red;
        //transform.Translate(Vector3.back*3f);
        yield return new WaitForSeconds(0.3f);
        outline.OutlineWidth = 0.1f;
        outline.OutlineColor = prevColor;
    }
}
