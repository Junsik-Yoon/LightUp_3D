using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject hiddenBoss;
    RoomChecker roomChecker;   
    public event UnityAction OnEnemyDead;
    public enum StageLevel
    {
        VERYEASY,
        EASY,
        NORMAL,
        HARD,
        VERYHARD,
        IMPOSSIBLE,
        MAX,
        BOSS,
        SPECIAL,
    }
    [Header("Level Setting")]
    public StageLevel stageLevel;//스테이지난이도
    private float genRate;//리젠속도
    private uint genQuantity;//리젠수량
    private float damageMulti;//데미지배율
    private float hpMultiple;//체력배율
    public GameObject[] prefEnemys;//현재 맵에서 나올 몬스터 종류 배열

    public GameObject prefPortal;//다음으로의 포탈


    private int enemyLeft;//남은 몹 수
    public GameObject mapGround;//맵 바닥
    MeshRenderer getMapSize;//맵 바닥 사이즈
    int randomRangeX;//맵 바닥 사이즈x
    int randomRangeZ;//맵 바닥 사이즈z
    private void Awake()
    {
        //Debug.Log("EnemyGeneratorAwake");
        roomChecker = GetComponentInParent<RoomChecker>();
        getMapSize = mapGround.GetComponent<MeshRenderer>();

        if(roomChecker.isStartRoom)
        {
            
        }
        else if(roomChecker.isShopRoom)
        {

        }
        else if(roomChecker.isItemRoom)
        {

        }
        // else if(roomChecker.isBossRoom)
        // {

        // }
        else if(roomChecker.isHiddenRoom)
        {

        }
        else
        {
            ResetLevelData();
           // if(roomChecker.isBossRoom){ ++stageLevel;Debug.Log("들어오는지");}
        }

    }
    private void Start()
    {

        
    }
    private void OnEnable()
    {
        if( //roomChecker.isBossRoom  || 
            //roomChecker.isHiddenRoom||
            roomChecker.isStartRoom ||
            roomChecker.isShopRoom  ||
            roomChecker.isItemRoom) 
        {
            roomChecker.SetDoor();
            if(roomChecker.isStartRoom)
            {
                roomChecker.roomLight.gameObject.SetActive(true);
            }
            else if(roomChecker.isShopRoom)
            {
                roomChecker.roomLight.gameObject.SetActive(true);
            }
            else if(roomChecker.isItemRoom)
            {
                //roomChecker.roomLight.gameObject.SetActive(true);
            }
            // else if(roomChecker.isBossRoom)
            // {
            //     //roomChecker.roomLight.enabled=true;
            // }
            // else if(roomChecker.isHiddenRoom)
            // {
            //     (roomChecker.hiddenBoss.GetComponent<EnemyHiddenBoss>()).Roar();
            // }
            return; //if문 안쪽 방들은 몬스터 리젠 x
        }

        if(roomChecker.isBossRoom)
        {
            ++stageLevel;ResetLevelData();
            BattleStageManager.instance.enemyCount = (int)genQuantity;
            BattleStageManager.instance.OnEnemyDead+=DeadCount;
            StartCoroutine(GenMonsters());
        }
        else if(roomChecker.isHiddenRoom)
        {
            stageLevel= StageLevel.BOSS;
            ResetLevelData();
            BattleStageManager.instance.enemyCount = (int)genQuantity;
            BattleStageManager.instance.OnEnemyDead+=DeadCount; 
           // Vector3 fixedPos = new Vector3(roomChecker.transform.position.x,roomChecker.transform.position.y+20f, roomChecker.transform.position.z);
            GameObject obj = Instantiate(hiddenBoss,roomChecker.transform.position,Quaternion.identity);
           // EnemyHiddenBoss boss = obj.transform.GetChild(0).GetComponent<EnemyHiddenBoss>();
           // boss.StartRoutine();


            //StartCoroutine(GenMonsters());
        }
        else
        {
            BattleStageManager.instance.enemyCount = (int)genQuantity;
            BattleStageManager.instance.OnEnemyDead+=DeadCount;
            StartCoroutine(GenMonsters());
        }
    }

    public void ResetLevelData()
    {
            switch(stageLevel)
            {
                case StageLevel.VERYEASY:
                {
                    genRate = 2f;
                    genQuantity = 5;
                    damageMulti = 0.5f;
                    hpMultiple = 0.5f;
                }break;
                case StageLevel.EASY:
                {
                    genRate = 1.7f;
                    genQuantity = 10;
                    damageMulti = 0.7f;
                    hpMultiple = 0.7f;
                }break;
                case StageLevel.NORMAL:
                {
                    genRate = 1.3f;
                    genQuantity = 15;
                    damageMulti = 1f;
                    hpMultiple = 1f;               
                }break;
                case StageLevel.HARD:
                {
                    genRate = 1f;
                    genQuantity = 20;
                    damageMulti = 1.3f;
                    hpMultiple = 1.3f;  
                }break;
                case StageLevel.VERYHARD:
                {
                    genRate = 0.7f;
                    genQuantity = 25;
                    damageMulti = 1.7f;
                    hpMultiple = 1.7f;  
                }break;
                case StageLevel.IMPOSSIBLE:
                {
                    genRate = 0.5f;
                    genQuantity = 30;
                    damageMulti = 2f;
                    hpMultiple = 2f;  
                }break;
                case StageLevel.BOSS:
                {
                    genQuantity = 1;
                }break;
                case StageLevel.SPECIAL:
                {

                }break;
            }
    }

    public void DeadCount()
    {
        enemyLeft=BattleStageManager.instance.enemyCount;
        if(enemyLeft<=0)
        {
            //Random.Range(1,3);
            // for(int i=0; i<Random.Range(1,3);++i)
            // {
            //     randomRangeX = Random.Range((int)((-getMapSize.bounds.size.x/2)+2),(int)((getMapSize.bounds.size.x/2)-1));
            //     randomRangeZ = Random.Range((int)((-getMapSize.bounds.size.z/2)+2),(int)((getMapSize.bounds.size.z/2)-1));
            //     Portal portal = Instantiate(prefPortal,new Vector3(randomRangeX,0.1f,randomRangeZ),Quaternion.identity).GetComponent<Portal>();
            //     portal.SetDestination("BattleStage1");
            // }
            roomChecker.SetDoor();
            roomChecker.roomLight.gameObject.SetActive(true);
            if(roomChecker.isBossRoom) roomChecker.SetPortal();
        }
    }
    IEnumerator GenMonsters()
    {
        int randomUnit;
    //    Debug.Log(getMapSize.bounds);


        for(int i=0; i<genQuantity; ++i)
        {
            randomUnit = Random.Range(0,prefEnemys.Length);
            randomRangeX = Random.Range((int)(-getMapSize.bounds.size.x/2+2),(int)(getMapSize.bounds.size.x/2-1));
            randomRangeZ = Random.Range((int)(-getMapSize.bounds.size.z/2+2),(int)(getMapSize.bounds.size.z/2-1));

            yield return new WaitForSeconds(genRate);
            GameObject obj = Instantiate(prefEnemys[randomUnit],new Vector3(transform.position.x+randomRangeX,0f,transform.position.z+randomRangeZ),Quaternion.identity);
            if(obj.name == "SkeletonWizard(Clone)")
            {
                obj.GetComponent<EnemySkeletonWizard>().minPos = 
                    new Vector3(transform.position.x+(int)(-getMapSize.bounds.size.x/2+2),0f,transform.position.z+(int)(-getMapSize.bounds.size.z/2+2));
                obj.GetComponent<EnemySkeletonWizard>().maxPos =
                    new Vector3(transform.position.x+(int)(getMapSize.bounds.size.x/2-1),0f,transform.position.z+(int)(getMapSize.bounds.size.z/2-1));
            }
        }




    }

}