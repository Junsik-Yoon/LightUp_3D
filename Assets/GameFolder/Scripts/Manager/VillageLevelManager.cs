using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

public class VillageLevelManager : MonoBehaviour
{
    public int villageLevel;
    public int expRequired;
    public int curExp;

    public float strMulti;
    public float agilMulti;
    public float hpMulti;
    public event UnityAction OnChangeLightCash;
    private int _lightCash;
    public int lightCash
    {
        get
        {
            return _lightCash;
        }
        set
        {
              
            _lightCash = value;
            OnChangeLightCash?.Invoke();
            
        }
    }
    public static VillageLevelManager instance {get;private set;}

    public class VillageLevelInfo
    {
        public int villageLevel;
        public int expRequired;
        public int curExp;
    }
    public class playerStatMultiplierInfo
    {
        public float strMulti;
        public float agilMulti;
        public float hpMulti;
        public int lightCash;
    }
    private void Awake()
    {
        instance  = this;
    }
    private void Start()
    {
        
        LoadVillageLevelData();
        LoadStatData();
    }

    public void BattleEndRewarded(int expReceived)
    {
        curExp+=expReceived;
        
        if(curExp>=expRequired)
        {
            ++villageLevel;
            curExp -= expRequired;
            expRequired += 100;
            _lightCash+=50;
        }

        VillageLevelInfo villageInfo = SetVillageLevelData();
        SetPlayerStatMultiplier();
        SaveVillageLevelData(villageInfo);
    }
    public VillageLevelInfo SetVillageLevelData()
    {
        VillageLevelInfo villageLevelInfo = new VillageLevelInfo();
        villageLevelInfo.villageLevel = villageLevel;
        villageLevelInfo.expRequired = expRequired;
        villageLevelInfo.curExp = curExp;
        return villageLevelInfo;
    }
    public void SaveVillageLevelData(VillageLevelInfo villageLevelInfo)
    {
        string jsonData = JsonUtility.ToJson(villageLevelInfo,true);
        string path = Path.Combine(Application.dataPath,"GameFolder/JSON/villageLevelData.json");
        File.WriteAllText(path,jsonData);
    }

    public void LoadVillageLevelData()
    {
        string path = Path.Combine(Application.dataPath,"GameFolder/JSON/villageLevelData.json");
        string jsonData = File.ReadAllText(path);
        VillageLevelInfo villageLevelInfo  = JsonUtility.FromJson<VillageLevelInfo>(jsonData);

        villageLevel    =   villageLevelInfo.villageLevel;
        expRequired     =   villageLevelInfo.expRequired;
        curExp          =   villageLevelInfo.curExp;
        
    }

    public void SetPlayerStatMultiplier()
    {
        playerStatMultiplierInfo statInfo = new playerStatMultiplierInfo();
        statInfo.strMulti = strMulti;
        statInfo.agilMulti = agilMulti;
        statInfo.hpMulti = hpMulti;
        statInfo.lightCash = _lightCash;
        SaveStatData(statInfo);
    }
    public void SaveStatData(playerStatMultiplierInfo statInfo)
    {
        string jsonData = JsonUtility.ToJson(statInfo,true);
        string path = Path.Combine(Application.dataPath,"GameFolder/JSON/statMultiplierData.json");
        File.WriteAllText(path,jsonData);
    }
    public void LoadStatData()
    {
        string path = Path.Combine(Application.dataPath,"GameFolder/JSON/statMultiplierData.json");
        string jsonData = File.ReadAllText(path);
        playerStatMultiplierInfo statInfo  = JsonUtility.FromJson<playerStatMultiplierInfo>(jsonData);

        strMulti    =   statInfo.strMulti;
        agilMulti   =   statInfo.agilMulti;
        hpMulti     =   statInfo.hpMulti;
        _lightCash   =   statInfo.lightCash;
        
    }

}
