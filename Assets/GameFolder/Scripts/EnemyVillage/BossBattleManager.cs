using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossBattleManager : MonoBehaviour
{
    public event UnityAction OnChangePhase;
    public event UnityAction OnGuardDead;
    public static BossBattleManager instance {get;private set;}
    public GameObject prefGuard;
    public Transform[] regenSpots;
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
}
