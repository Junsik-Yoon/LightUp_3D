using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleStageManager : MonoBehaviour
{
    
    public event UnityAction OnEnemyDead;
    public static BattleStageManager instance {get;private set;}
    private void Awake()
    {
        instance =this;
    }
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
            OnEnemyDead?.Invoke();
            //Debug.Log(_enemyCount);
            
        }
    }
    
}
