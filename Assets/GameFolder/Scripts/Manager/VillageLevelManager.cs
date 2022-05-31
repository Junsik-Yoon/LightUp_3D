using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageLevelManager : MonoBehaviour
{
    public int villageLevel;
    public int expRequired;
    public int curExp;
    public static VillageLevelManager instance {get;private set;}

    public class VillageLevelInfo
    {
        public int villageLevel;
        public int expRequired;
        public int curExp;
    }
    private void Awake()
    {
        instance  = this;
    }

    public void BattleEndRewarded(int expReceived)
    {
        curExp+=expReceived;
    }

}
