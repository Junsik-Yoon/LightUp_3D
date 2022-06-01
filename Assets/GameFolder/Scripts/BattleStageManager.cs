using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
public class BattleStageManager : MonoBehaviour
{
    public static int dungeonStatus = 0; //보스클리어하거나 죽으면 다시 0으로 돌리기
    public static int enemyKilled = 0;
    public float playTime;

    public GameObject bloodFilledParticle;//bloodfilled임시저장
    public event UnityAction OnEnemyDead;
    public static BattleStageManager instance {get;private set;}


    Player player;
    private void Awake()
    {
        instance =this;
    }
    private void Start()
    {

        if(dungeonStatus == 0)
        {
            playTime = Time.time;
        }
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

    public void PlayerDeadAndReRoll()
    {
        //모습 아바타 리롤하기
        int randomBattleStyle = Random.Range(0,2);
        switch(randomBattleStyle)
        {
            case 0://fist style
            {
                player.setBattleStyle = "Fist";
            }break;
            case 1://magician style
            {
                player.setBattleStyle = "Dance";
            }break;
            case 2://dance style
            {
                player.setBattleStyle = "Magician";
            }break;
            //클래스 추가될 시 추가
        }
        SetNeededPlayerData(player);
    }
    [System.Serializable]
    public class SavePlayerDataAsClass
    {
        public float damage;
        public string playerBattleStyle;
        public float playerLightLeft;
        public float moveSpeed;
        public float hitRadius;
        public List<ItemData> equipItems = new List<ItemData>();
        public List<ItemData> inventoryItems = new List<ItemData>();
        public float playTime;
    }
    public void SetNeededPlayerData(Player player)
    {
        SavePlayerDataAsClass playerData = new SavePlayerDataAsClass();
        //저장이 필요한 플레이어 데이터만 저장
        //playerData.playerBattleStyle = player.setBattleStyle;
        playerData.damage = player.damage;
        playerData.playerLightLeft = player.playerLightLeft;
        playerData.equipItems = InventoryManager.instance.equipItems;
        playerData.inventoryItems = InventoryManager.instance.inventoryItems;
        playerData.moveSpeed = player.moveSpeed;
        playerData.hitRadius = player.hitRadius;
        
        //던전 1스테이지 진입시간
        playerData.playTime = playTime;
        
        SavePlayerData(playerData);
    }
    public void SavePlayerData(SavePlayerDataAsClass player)
    {
        string jsonData = JsonUtility.ToJson(player,true);
        string path = Path.Combine(Application.dataPath,"GameFolder/JSON/playerData.json");
        File.WriteAllText(path,jsonData);
    }
    public void LoadPlayerData(Player player)
    {
        string path = Path.Combine(Application.dataPath,"GameFolder/JSON/playerData.json");
        string jsonData = File.ReadAllText(path);
        SavePlayerDataAsClass playerData  = JsonUtility.FromJson<SavePlayerDataAsClass>(jsonData);

        //저장데이터 플레이어에 다시 넣기
        player.damage = playerData.damage;
        ((player.playerLight.gameObject)?.GetComponent<Light>()).spotAngle = playerData.playerLightLeft;
        InventoryManager.instance.equipItems = playerData.equipItems;
        foreach( var item in InventoryManager.instance.equipItems)
        {
            (item.prefab?.GetComponent<Equipable>()).Equip();
        }
        InventoryManager.instance.inventoryItems = playerData.inventoryItems; 
        player.moveSpeed = playerData.moveSpeed; 
        player.hitRadius = playerData.hitRadius; 
        playTime = playerData.playTime;
    }
    
}
