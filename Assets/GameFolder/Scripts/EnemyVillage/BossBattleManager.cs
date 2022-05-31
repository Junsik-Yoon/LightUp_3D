using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossBattleManager : MonoBehaviour
{
    public event UnityAction OnChangePhase;
    public event UnityAction OnGuardDead;
    public static BossBattleManager instance {get;private set;}
    public GameObject prefGuard;
    public Transform[] regenSpots;

    public GameObject resultUI;
    public Text clearTimeText;
    public Text enemiesKilledText;
    public Text rewardText;
    public Text rankText;
    private int _enemyCount;
    public int enemyCount
    {
        get
        {
            return _enemyCount;
        }
        set
        {
            _enemyCount = value;
           // OnGuardDead?.Invoke();
            //Debug.Log(_enemyCount);
            if(_enemyCount<=0)
            {
                phase=3;
            }
        }
    }
    private int _phase=0;
    public int phase
    {
        get
        {
            return _phase;
        }
        set
        {
            _phase = value;
            OnChangePhase?.Invoke();
        }
    }
    private void Awake()
    {
        instance=this;
        OnChangePhase+=Phase;
        enemyCount = 10;
        //OnGuardDead+=DeadCount;
    }
    public void Phase()
    {
        if(phase==2)
        {
            StartCoroutine(GenGuards());
        }
        else if(phase ==3)
        {

        }
    }
    IEnumerator GenGuards()
    {

        for(int i=0; i<10; ++i)
        {
            yield return new WaitForSeconds(2f);
            int index = Random.Range(0,regenSpots.Length);
            Instantiate(prefGuard, regenSpots[index].position, Quaternion.identity);
        }
    }
    public IEnumerator ShowBattleResult()
    {
        yield return new WaitForSeconds(5f);
        //결과 다 세팅하고
        int clearTime = (int)(Time.time - BattleStageManager.instance.playTime);
        string clearTimeString = "00:" + (clearTime/60).ToString("00")+":"+ (clearTime%60).ToString("00"); 
        string enemyKilledString = BattleStageManager.enemyKilled.ToString();

        //등급 정하기
        //몬스터 몇마리 잡고 속도가 얼마나 빨랐는가 등으로 계산해서 돈으로 환산

        string rank;
        string rewardString;
        if(clearTime < 1200)// && BattleStageManager.enemyKilled > 200)
        {
            rank = "SSS";
            rewardString = 100.ToString();
        }
        else if(clearTime <1500)// && BattleStageManager.enemyKilled > 200)  ||(clearTime <1200 && BattleStageManager.enemyKilled > 160) )
        {
            rank = "SS";
            rewardString = 80.ToString();
        }
        else if(clearTime < 1800)// && BattleStageManager.enemyKilled > 160)
        {
            rank = "S";
            rewardString = 60.ToString();
        }
        else if(clearTime < 2100)
        {
            rank = "A";
            rewardString = 40.ToString();
        }
        else
        {
            rank = "B";
            rewardString = 20.ToString();
        }

        //결과 창 띄우고
        resultUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        clearTimeText.text = clearTimeString;
        yield return new WaitForSeconds(1f);
        enemiesKilledText.text = enemyKilledString;
        yield return new WaitForSeconds(1f);
        rewardText.text = rewardString;
        yield return new WaitForSeconds(1f);
        rankText.text = rank;
        

        yield return new WaitForSeconds(5f);
        //몇 초 후에 혹은 입력을 받아서 마을로 이동하도록
        LoadingHelper.LoadScene("VillageScene");
    }
}
