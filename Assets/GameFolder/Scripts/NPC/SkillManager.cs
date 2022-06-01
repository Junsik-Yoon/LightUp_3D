using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SkillManager : MonoBehaviour
{
    [System.Serializable]
    public class ListofSkills
    {
        public List<SkillData> uSkillList = new List<SkillData>();
        public List<SkillData> unUSkillList = new List<SkillData>();
    }
   
    public static SkillManager instance {get;private set;}
    public GameObject skillUI;
    Animator skillUIAnim;
    //=========================================
    public List<SkillData> wholeSkillList;
    public List<SkillData> usingSkillList;
    public List<SkillData> unUsingSkillList;
    //=========================================
    public UsingSkillUnit[] usingSkills;
    public UnUsingSkillUnit[] notUsingSkills;
    //=========================================
    public int maxUsingSkills = 2;
    public int maxUnUsingSkills = 5;
    Player player;
    private void Awake()
    {
        instance = this;
        
        player = (GameObject.FindGameObjectWithTag("Player"))?.GetComponent<Player>();
        if(skillUI!=null) skillUIAnim = skillUI.GetComponent<Animator>();
        //Debug.Log(skillUIAnim);
        //wholeSkillList = new List<SkillData>();
        //usingSkillList = new List<SkillData>();
        //unUsingSkillList = new List<SkillData>();

    //    LoadSkillData();
    //    UpdateSkillUI();

        //InitSkillUI();
    }
    private void Start()
    {
        LoadSkillData();
        UpdateSkillUI();
        
        //총 스킬 리스트에서 불러와야해. 무엇-> 지금 사용중인 스킬과 사용중이지 않은 스킬을
        //임시로 세이브
        //SaveSkillData();
      //  AddCurrentSkills();

    }
    // public void AddCurrentSkills()
    // {

    // }

    public void LoadSkillData()
    {
        if(player.setBattleStyle == "Fist")
        {
            string path = Path.Combine(Application.dataPath,"GameFolder/JSON/savedSkillDataFist.json");
            string jsonData = File.ReadAllText(path);
            ListofSkills listofSkills  = JsonUtility.FromJson<ListofSkills>(jsonData);
            usingSkillList = listofSkills.uSkillList;
            unUsingSkillList = listofSkills.unUSkillList;
        }
        else if(player.setBattleStyle == "Dance")
        {
            string path = Path.Combine(Application.dataPath,"GameFolder/JSON/savedSkillDataDance.json");
            string jsonData = File.ReadAllText(path);
            ListofSkills listofSkills  = JsonUtility.FromJson<ListofSkills>(jsonData);
            usingSkillList = listofSkills.uSkillList;
            unUsingSkillList = listofSkills.unUSkillList;
        }
        else if(player.setBattleStyle == "Magician")
        {
            string path = Path.Combine(Application.dataPath,"GameFolder/JSON/savedSkillDataMagician.json");
            string jsonData = File.ReadAllText(path);
            ListofSkills listofSkills  = JsonUtility.FromJson<ListofSkills>(jsonData);
            usingSkillList = listofSkills.uSkillList;
            unUsingSkillList = listofSkills.unUSkillList;
        }

    }
    public void SaveSkillData()
    {
        if(player.setBattleStyle == "Fist")
        {
            ListofSkills listofSkills = new ListofSkills();
            listofSkills.uSkillList = usingSkillList;
            listofSkills.unUSkillList = unUsingSkillList;
            string jsonData = JsonUtility.ToJson(listofSkills,true);
            string path = Path.Combine(Application.dataPath,"GameFolder/JSON/savedSkillDataFist.json");
            File.WriteAllText(path,jsonData);
        }
        else if(player.setBattleStyle == "Dance")
        {
            ListofSkills listofSkills = new ListofSkills();
            listofSkills.uSkillList = usingSkillList;
            listofSkills.unUSkillList = unUsingSkillList;
            string jsonData = JsonUtility.ToJson(listofSkills,true);
            string path = Path.Combine(Application.dataPath,"GameFolder/JSON/savedSkillDataDance.json");
            File.WriteAllText(path,jsonData);
        }
        else if(player.setBattleStyle == "Magician")
        {
            ListofSkills listofSkills = new ListofSkills();
            listofSkills.uSkillList = usingSkillList;
            listofSkills.unUSkillList = unUsingSkillList;
            string jsonData = JsonUtility.ToJson(listofSkills,true);
            string path = Path.Combine(Application.dataPath,"GameFolder/JSON/savedSkillDataMagician.json");
            File.WriteAllText(path,jsonData);           
        }

    }

    public bool UsingSkillAdd(SkillData skill)
    {
        if(usingSkillList.Count >=maxUsingSkills)
        {
            return false;
        }
        usingSkillList.Add(skill);
        unUsingSkillList.Remove(skill);
        UpdateSkillUI();
        return true;
    }
    public void UsingSkillRemove(SkillData skill)
    {
        usingSkillList.Remove(skill);
        unUsingSkillList.Add(skill);
        UpdateSkillUI();
        
    }
    public void MoveSkillUIUp()
    {
        skillUIAnim.SetTrigger("UIUp");
    }
    public void MoveSkillUIDown()
    {
        skillUIAnim.SetTrigger("UIDown");
    }
    public void OnConfirmButton()
    {
        if(usingSkillList.Count!=2)return;

        //만약 스킬이 2개가 다 안채워져있으면 컨펌안되도록 예외처리
        SaveSkillData();
        UpdatePlayerSkillData();
        skillUIAnim.SetTrigger("UIUp");
    }
    public void OnCancelButton()
    {
        skillUIAnim.SetTrigger("UIUp");
       // LoadSkillData();복사버그수정하기
    }

    public void UpdatePlayerSkillData()
    {
        player.battleStyle.curSkillA = usingSkillList[0];
        player.battleStyle.curSkillB = usingSkillList[1];
        if(usingSkillList[0].isPassive)
        {
            usingSkillList[0].skillUse.UseSkill();
        }
        if(usingSkillList[1].isPassive)
        {
            usingSkillList[1].skillUse.UseSkill();
        }
    }
    // public void InitSkillUI()
    // {
    //     for(int i=0; i< usingSkills.Length;++i)
    //     {
    //         if(i<usingSkillList.Count)
    //         {
    //             usingSkills[i].AddItem(usingSkillList[i]);
    //         }
    //         else
    //         {
    //             usingSkills[i].RemoveItem();
    //         }
    //     }
    //     for(int i=0; i< notUsingSkills.Length;++i)
    //     {
    //         if(i<unUsingSkillList.Count)
    //         {
    //             notUsingSkills[i].AddItem(unUsingSkillList[i]);
    //         }
    //         else
    //         {
    //             notUsingSkills[i].RemoveItem();
    //         }
    //     }
    // }
     public void UpdateSkillUI()
    {
        usingSkills = GetComponentsInChildren<UsingSkillUnit>();
        for(int i=0; i< usingSkills.Length;++i)
        {
            if(i<usingSkillList.Count)
            {
                usingSkills[i].AddItem(usingSkillList[i]);
            }
            else
            {
                usingSkills[i].RemoveItem();
            }
        }
        notUsingSkills = GetComponentsInChildren<UnUsingSkillUnit>();
        for(int i=0; i< notUsingSkills.Length;++i)
        {
            if(i<unUsingSkillList.Count)
            {
                notUsingSkills[i].AddItem(unUsingSkillList[i]);
            }
            else
            {
                notUsingSkills[i].RemoveItem();
            }
        }

    }



    //    public void SetNeededPlayerData(Player player)
    // {
    //     SavePlayerDataAsClass playerData = new SavePlayerDataAsClass();
    //     //저장이 필요한 플레이어 데이터만 저장
    //     playerData.damage = player.damage;
    //     playerData.playerLightLeft = player.playerLightLeft;
    //     playerData.equipItems = InventoryManager.instance.equipItems;
    //     playerData.inventoryItems = InventoryManager.instance.inventoryItems;
    //     playerData.moveSpeed = player.moveSpeed;
    //     playerData.hitRadius = player.hitRadius;
        
    //     //던전 1스테이지 진입시간
    //     playerData.playTime = playTime;
        
    //     SavePlayerData(playerData);
    // }
    // public void SavePlayerData(SavePlayerDataAsClass player)
    // {
    //     string jsonData = JsonUtility.ToJson(player,true);
    //     string path = Path.Combine(Application.dataPath,"GameFolder/JSON/playerData.json");
    //     File.WriteAllText(path,jsonData);
    // }
    // public void LoadPlayerData(Player player)
    // {
    //     string path = Path.Combine(Application.dataPath,"GameFolder/JSON/playerData.json");
    //     string jsonData = File.ReadAllText(path);
    //     SavePlayerDataAsClass playerData  = JsonUtility.FromJson<SavePlayerDataAsClass>(jsonData);

    //     //저장데이터 플레이어에 다시 넣기
    //     player.damage = playerData.damage;
    //     ((player.playerLight.gameObject)?.GetComponent<Light>()).spotAngle = playerData.playerLightLeft;
    //     InventoryManager.instance.equipItems = playerData.equipItems;
    //     foreach( var item in InventoryManager.instance.equipItems)
    //     {
    //         (item.prefab?.GetComponent<Equipable>()).Equip();
    //     }
    //     InventoryManager.instance.inventoryItems = playerData.inventoryItems; 
    //     player.moveSpeed = playerData.moveSpeed; 
    //     player.hitRadius = playerData.hitRadius; 
    //     playTime = playerData.playTime;
    // }

    public IEnumerator ClubLight(PlayerLight playerLight)
    {
        while(true)
        {
            playerLight.playerLight.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            playerLight.playerLight.color = Color.green;
            yield return new WaitForSeconds(0.2f);
            playerLight.playerLight.color = Color.blue;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
